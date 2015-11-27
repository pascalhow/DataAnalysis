using System;

namespace DataAnalysisWPF
{
    public class Data
    {
        /// <summary>
        /// This method calculates the sum of the given dataset
        /// </summary>
        /// <param name="valuesArray"></param>
        /// <returns></returns>
        public double Sum(double[] valuesArray)
        {
            double sum = 0.0;

            //  Add all the elements in the dataset
            foreach (double value in valuesArray)
            {
                sum += value;
            }

            return sum;
        }

        /// <summary>
        /// This method calculates the mean based on a given dataset
        /// </summary>
        /// <param name="valuesArray"></param>
        /// <returns></returns>
        public double Mean(double[] valuesArray)
        {
            double mean = 0.0;
            double sum = this.Sum(valuesArray);

            //  Divide the sum the number of elements in the dataset
            mean = sum / (valuesArray.Length);

            return mean;
        }

        /// <summary>
        /// This method calculates the standard deviation based on a given dataset (divide by n-1 instead of n for sample standard deviation)
        /// Variance is given by (∑(xi - x̄)^2)/n-1
        /// Derive ∑(xi - x̄)^2 = ∑(xi^2) - n·x̄^2 = ∑(xi^2) - (∑xi)^2 / n
        /// Then take square root of variance to obtain standard deviation
        /// </summary>
        /// <param name="valuesArray"></param>
        /// <returns></returns>
        public double StandardDeviation(double[] valuesArray, MainWindow.stdDeviationType stdDeviationType)
        {
            double mean = this.Mean(valuesArray);
            double stdDev = 0.0;
            double sumOfDerivation = 0.0;
            double sumOfDerivationAverage = 0.0;
            double sum = this.Sum(valuesArray);

            //  Add the square of all the elements from the dataset as per formula
            foreach (double value in valuesArray)
            {
                sumOfDerivation += (value) * (value);
            }

            switch(stdDeviationType)
            {            
                case MainWindow.stdDeviationType.POPULATION:
                    //  Divide by n for the population standard deviation
                    sumOfDerivationAverage = sumOfDerivation / (valuesArray.Length);

                    //  After derivation the denominator is n^2 so we can subtract mean * mean
                    stdDev = Math.Sqrt(sumOfDerivationAverage - (mean * mean));
                    break;

                case MainWindow.stdDeviationType.SAMPLE:
                    //  Divide by the Bressel correction factor n - 1 for sample standard deviation
                    sumOfDerivationAverage = sumOfDerivation / (valuesArray.Length - 1);

                    //  After derivation the denominator is n(n - 1) so we subtract mean * sum/(n - 1)
                    stdDev = Math.Sqrt(sumOfDerivationAverage - (mean * sum / (valuesArray.Length - 1)));
                    break;

                default:
                    break;
            }
            return stdDev;
        }

        /// <summary>
        /// This method sorts a given array in ascending order
        /// </summary>
        /// <param name="array"></param>
        public void sortAscending(double[] array)
        {
            //  This is gives similar result to Array.Sort() method
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        double temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }

        public double[] getHistogramArray(double[] array, int maxDataValue, int boundarySize)
        {
            //  Sort valuesArray in ascending order first so that sample is easier to loop through  
            this.sortAscending(array);
            int boundary = boundarySize;

            //  Subdivide the value range based on size of boundary
            int steps = maxDataValue / boundarySize;

            double[] histogramArray = new double[steps];

            int count = 0;
            int index = 0;
            
            for (int i = 0; i < steps; i++)
            {
                //  We use index so we remember where we got to in the valuesArray after every count
                for (int j = index; j < array.Length; j++)
                {
                    //  As long as the value is less than the boundary value, keep incrementing
                    if (array[j] < boundary)
                    {
                        count++;
                        index++;
                    }
                    else
                    {
                        //  Value exceeded boundary so save the count
                        histogramArray[i] = count;
                        break;
                    }
                }

                //  This is to save the last count in our histogramArray
                //  We then break out of the loop as any other value in the histogramArray will be 0 anyway
                if (index == array.Length)
                {
                    histogramArray[i] = count;
                    break;
                }

                //  Increase the boundary and reset count
                boundary += boundarySize;
                count = 0;
            }

            return histogramArray;
        }
    }
}
