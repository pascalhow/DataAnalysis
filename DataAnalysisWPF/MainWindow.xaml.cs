using System;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using ChartDirector;

namespace DataAnalysisWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double[] valuesArray;
        private int boundarySize = 10;
        private int minDataValue = 0;
        private int maxDataValue = 100;
        private int chartWidth = 600;
        private int chartHeight = 350;
        private int chartXLocation = 70;
        private int chartYLocation = 60;
        private int plotWidth = 500;
        private int plotHeight = 200;
        private Data data;
        WinChartViewer chartViewer;

        public enum stdDeviationType
        {
            POPULATION,
            SAMPLE
        }

        public MainWindow()
        {
            InitializeComponent();
            data = new Data();

            //  We choose desktop as default directory as all Windows platforms should have one
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Data Sample";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            directory_txtBox.Text = directory;

            //  This is necessary as Chart Director does not fully support WPF yet.... :(
            chartViewer = new WinChartViewer();
            chartViewer.ChartSizeMode = WinChartSizeMode.StretchImage;
            this.windowsFormsHost.Child = chartViewer;
        }

        /// <summary>
        /// This method is called when the open button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_openFile_Click(object sender, RoutedEventArgs e)
        {
            //  Open file dialog to select file adn filter by csv          
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            // Show open file dialog box
            Nullable<bool> result = openFileDialog.ShowDialog();

            //  If user clicked OK
            if (result == true)
            {
                Stream stream = openFileDialog.OpenFile();

                //  Display file directory and filename in text box
                directory_txtBox.Text = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// This method reads all the values from the csv file as strings and then converts them to doubles
        /// </summary>
        /// <param name="path"></param>
        /// <returns name="allDataConverted"></returns>
        private bool saveDataSet(string path)
        {
            bool allDataConverted = false;

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    //  Read all the contents of the csv file and convert to array of doubles
                    String[] stringValues = File.ReadAllText(path).Split(new Char[] { ',', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    List<double> valueList = new List<double>();

                    foreach (string s in stringValues)
                    {
                        try
                        {
                            double value = Convert.ToDouble(s);

                            //  Only accept values within the specified range
                            if ((value > minDataValue) && (value < maxDataValue))
                            {
                                valueList.Add(value);
                            }
                        }
                        catch
                        {
                            //  Some data has not been converted because they were not numbers
                            allDataConverted = false;
                            break;
                        }
                    }

                    if (valueList.Count == stringValues.Length)
                    {
                        //  Only convert the list to array if all elements have been converted
                        //  Set allDataConverted to true
                        this.valuesArray = valueList.ToArray();
                        allDataConverted = true;
                    }
                }
            }
            return allDataConverted;
        }

        /// <summary>
        /// This method is called when the analyse button is clicked
        /// It implements the Mean, Standard Deviation and getHistogramArray method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_analyse_Click(object sender, RoutedEventArgs e)
        {
            //  Load excel spreadsheet from the chosen file directory
            bool allDataConverted = saveDataSet(directory_txtBox.Text);

            if ((!string.IsNullOrEmpty(directory_txtBox.Text)) && (allDataConverted == true))
            {
                //  Calculate the mean from dataset
                double mean = data.Mean(this.valuesArray);
                lbl_mean.Content = "Mean: " + string.Format("{0:N3}", mean);

                //  Calculate the population standard deviation for the dataset
                double popStdDev = data.StandardDeviation(this.valuesArray, stdDeviationType.POPULATION);
                lbl_popStdDev.Content = "Population Standard Deviation: " + string.Format("{0:N3}", popStdDev);

                //  Calculate the sample standard deviation for the dataset
                double sampleStdDev = data.StandardDeviation(this.valuesArray, stdDeviationType.SAMPLE);
                lbl_sampleStdDev.Content = "Sample Standard Deviation: " + string.Format("{0:N3}", sampleStdDev);

                //  Subdivide the value range based on size of boundary
                int steps = maxDataValue / boundarySize;
                double[] histogramArray = new double[steps];

                //  Get the histogram array for the given dataset in steps defined by boundary size
                histogramArray = data.getHistogramArray(this.valuesArray, maxDataValue, boundarySize);

                // The labels for the bar chart
                string[] labels = new string[steps];
                int lowerBoundary = 0;
                int upperBoundary = boundarySize;

                for (int i = 0; i < steps; i++)
                {
                    labels[i] = lowerBoundary.ToString() + " < " + upperBoundary.ToString();
                    lowerBoundary = upperBoundary;
                    upperBoundary += boundarySize;
                }

                //  Plot the histogram
                plot(chartWidth, chartHeight, histogramArray, labels, "Histogram", "Sample", "Frequency");
            }
            else if (string.IsNullOrEmpty(directory_txtBox.Text))
            {
                //  If the directory is empty
                //  Send an error message otherwise there is no data to process 
                MessageBox.Show("Directory cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!File.Exists(directory_txtBox.Text))
            {
                //  If the file does not exist
                //  Send an error message otherwise processing will result in errors 
                MessageBox.Show("File directory does not exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //  If the dataset contains invalid values
                //  Send an error message otherwise processing will result in errors 
                MessageBox.Show("Data sample contains invalid numbers!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// This method uses the Chart Director API to plot the histogram
        /// </summary>
        /// <param name="chartWidth"></param>
        /// <param name="chartHeight"></param>
        /// <param name="data"></param>
        /// <param name="labels"></param>
        /// <param name="title"></param>
        /// <param name="xAxisLabel"></param>
        /// <param name="yAxisLabel"></param>
        private void plot(int chartWidth, int chartHeight, double[] data, string[] labels, string title, string xAxisLabel, string yAxisLabel)
        {
            // Create a XYChart object of size chartWidth x chartHeight pixels
            XYChart chart = new XYChart(chartWidth, chartHeight);

            // Set default text color to dark grey (0x333333)
            chart.setColor(Chart.TextColor, 0x333333);

            // Add a title box using grey (0x555555) 24pt Arial Bold font
            chart.addTitle(title, "Arial Bold", 24, 0x555555);

            // Set the plotarea at (70, 60) and of size 500 x 200 pixels, with transparent
            // background and border and light grey (0xcccccc) horizontal grid lines
            chart.setPlotArea(chartXLocation, chartYLocation, plotWidth, plotHeight, Chart.Transparent, -1, Chart.Transparent, 0xcccccc);

            // Set the x and y axis stems to transparent and the label font to 10pt Arial
            chart.xAxis().setColors(Chart.Transparent);
            chart.yAxis().setColors(Chart.Transparent);

            // Use 10 points Arial rotated by 45 degrees as the x-axis label font
            chart.xAxis().setLabelStyle("Arial", 10, Chart.TextColor, 45);
            chart.yAxis().setLabelStyle("Arial", 10);

            // Add a blue (0x6699bb) bar chart layer using the given data
            BarLayer layer = chart.addBarLayer(data, 0x6699bb);

            // Use bar gradient lighting with the light intensity from 0.8 to 1.3
            layer.setBorderColor(Chart.Transparent, Chart.barLighting(0.8, 1.3));

            // Set rounded corners for bars
            layer.setRoundedCorners();

            // Display labela on top of bars using 10pt Arial font
            layer.setAggregateLabelStyle("Arial", 10);

            // Set the labels on the x axis.
            chart.xAxis().setLabels(labels);

            // For the automatic y-axis labels, set the minimum spacing to 40 pixelcharts.
            chart.yAxis().setTickDensity(40);

            // Add a title to the y axis using dark grey (0x555555) 12pt Arial Bold font
            chart.yAxis().setTitle(yAxisLabel, "Arial Bold", 12, 0x555555);

            // Add a title to the x axis using dark grey (0x555555) 12pt Arial Bold font
            chart.xAxis().setTitle(xAxisLabel, "Arial Bold", 12, 0x555555);

            //output the chart
            chartViewer.Chart = chart;
        }
    }
}

