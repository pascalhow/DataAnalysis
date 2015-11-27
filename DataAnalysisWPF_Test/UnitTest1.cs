using DataAnalysisWPF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAnalysisWPF_Test
{
    [TestClass]
    public class UnitTest
    {
        Data data = new Data();

        /// <summary>
        /// This test verifies if the sum of all the array elements are added properly
        /// </summary>
        [TestMethod]
        public void Sum_TestMethod1()
        {
            //  Arrange
            double[] Array = new double[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            double expectedSum = 55;

            //  Act
            double actualSum = data.Sum(Array);

            //  Assert
            Assert.AreEqual(actualSum, expectedSum);
        }

        /// <summary>
        /// This test verifies if the sum of all the array elements are added properly when it contains negative numbers
        /// </summary>
        [TestMethod]
        public void Sum_TestMethod2()
        {
            //  Arrange
            double[] Array = new double[10] { 1, 2, -3, 4, 5, -6, 7, 8, -9, 10 };
            double expectedSum = 19;

            //  Act
            double actualSum = data.Sum(Array);

            //  Assert
            Assert.AreEqual(actualSum, expectedSum);
        }


        /// <summary>
        /// This test verifies if the Mean method calculates the mean of the dataset correctly
        /// </summary>
        [TestMethod]
        public void Mean_TestMethod1()
        {
            //  Arrange
            double[] Array = new double[10] { 1, 2, -3, 4, 5, -6, 7, 8, -9, 10 };
            double expectedMean = 1.9;

            //  Act
            double actualMean = data.Mean(Array);

            //  Assert
            Assert.AreEqual(actualMean, expectedMean);
        }


        /// <summary>
        /// This test verifies if the population standard deviation is calculated correctly from the dataset
        /// </summary>
        [TestMethod]
        public void PopulationStandardDeviation_TestMethod1()
        {
            //  Arrange
            double[] Array = new double[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            double expectedPopulationStdDev = 2.872;

            //  Act
            double actualPopulationStdDev = data.StandardDeviation(Array, MainWindow.stdDeviationType.POPULATION);

            //  Assert
            Assert.AreEqual(actualPopulationStdDev, expectedPopulationStdDev, 0.001);
        }

        /// <summary>
        /// This test verifies if the sample standard deviation is calculated correctly from the dataset
        /// </summary>
        [TestMethod]
        public void SampleStandardDeviation_TestMethod1()
        {
            //  Arrange
            double[] Array = new double[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            double expectedSampleStdDev = 3.027;

            //  Act
            double actualSampleStdDev = data.StandardDeviation(Array, MainWindow.stdDeviationType.SAMPLE);

            //  Assert
            Assert.AreEqual(actualSampleStdDev, expectedSampleStdDev, 0.001);
        }

        /// <summary>
        /// This test verifies if the sortAscending method can sort an initial array in descending order
        /// </summary>
        [TestMethod]
        public void sortAscending_TestMethod1()
        {
            //  Arrange
            double[] startingArray = new double[10] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            double[] expectedArray = new double[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //  Act
            data.sortAscending(startingArray);

            //  Assert
            CollectionAssert.AreEqual(expectedArray, startingArray);
        }

        /// <summary>
        /// This test verifies if the sortAscending method can sort an initial array in random order
        /// </summary>
        [TestMethod]
        public void sortAscending_TestMethod2()
        {
            //  Arrange
            double[] startingArray = new double[10] { 4, 3, 6, 10, 8, 9, 1, 7, 2, 5 };
            double[] expectedArray = new double[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //  Act
            data.sortAscending(startingArray);

            //  Assert
            CollectionAssert.AreEqual(expectedArray, startingArray);
        }

        /// <summary>
        /// This test verifies if the sortAscending method can sort an initial array in ascending order
        /// </summary>
        [TestMethod]
        public void sortAscending_TestMethod3()
        {
            //  Arrange
            double[] startingArray = new double[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            double[] expectedArray = new double[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //  Act
            data.sortAscending(startingArray);

            //  Assert
            CollectionAssert.AreEqual(expectedArray, startingArray);
        }

        /// <summary>
        /// This test verifies if the sortAscending method can sort an initial array with negative numbers
        /// </summary>
        [TestMethod]
        public void sortAscending_TestMethod4()
        {
            //  Arrange
            double[] startingArray = new double[10] { 1, 2, -3, 4, 5, 6, 7, -8, 9, 10 };
            double[] expectedArray = new double[10] { -8, -3, 1, 2, 4, 5, 6, 7, 9, 10 };

            //  Act
            data.sortAscending(startingArray);

            //  Assert
            CollectionAssert.AreEqual(expectedArray, startingArray);
        }

        /// <summary>
        /// This test verifies if the getHistogramArray method returns the frequency of numbers for a given dataset in ascending order
        /// This is based on the boundary size and maximum value specified
        /// </summary>
        [TestMethod]
        public void getHistogramArray_TestMethod1()
        {
            int maxDataValue = 100;
            int boundarySize = 10;
            int step = maxDataValue / boundarySize;

            //  Arrange
            double[] sampleArray = new double[15] { 3, 4, 5, 10, 11, 12, 24, 35, 36, 40, 42, 49, 51, 53, 60 };
            double[] expectedArray = new double[10] { 3, 3, 1, 2, 3, 2, 1, 0, 0, 0 };

            double[] actualArray = new double[step];

            //  Act
            actualArray = data.getHistogramArray(sampleArray, maxDataValue, boundarySize);

            //  Assert
            CollectionAssert.AreEqual(expectedArray, actualArray);
        }

        /// <summary>
        /// This test verifies if the getHistogramArray method returns the frequency of numbers for a given dataset in random order
        /// This is based on the boundary size and maximum value specified
        /// </summary>
        [TestMethod]
        public void getHistogramArray_TestMethod2()
        {
            int maxDataValue = 100;
            int boundarySize = 10;
            int step = maxDataValue / boundarySize;

            //  Arrange
            double[] sampleArray = new double[15] { 3, 24, 51, 10, 60, 12, 4, 35, 36, 40, 42, 49, 5, 53, 11 };
            double[] expectedArray = new double[10] { 3, 3, 1, 2, 3, 2, 1, 0, 0, 0 };

            double[] actualArray = new double[step];

            //  Act
            actualArray = data.getHistogramArray(sampleArray, maxDataValue, boundarySize);

            //  Assert
            CollectionAssert.AreEqual(expectedArray, actualArray);
        }
    }
}
