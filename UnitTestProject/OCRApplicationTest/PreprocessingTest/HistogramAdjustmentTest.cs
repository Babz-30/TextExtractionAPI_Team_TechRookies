using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCRApplication.Preprocesssing;
using OCRApplicationTest.Helpers;

namespace OCRApplicationTest
{
    /// <summary>
    /// Test class for HistogramAdjustment, verifying image enhancement functionality.
    /// </summary>
    [TestClass]
    public class HistogramAdjustmentTest
    {
        private string testImagePath;
        private string outputImagePath;

        // Sets up test environment input and output image paths.
        [TestInitialize]
        public void Setup()
        {
            testImagePath = TestUtilityClass.InputImagePath("test_image.jpg");
            outputImagePath = TestUtilityClass.OutputImagePath("histogram_adjusted.jpg");
        }

        // Tests that ApplyHistogramAdjustment correctly processes an image.
        [TestMethod]
        public void ApplyHistogramAdjustment_ValidInput_CreatesAdjustedImage()
        {
            // Arrange
            HistogramAdjustment histogramAdjustment = new HistogramAdjustment();
            double saturationFactor = 1.2;
            double intensityFactor = 1.1;

            // Act
            string resultPath = histogramAdjustment.ApplyHistogramAdjustment(testImagePath, outputImagePath, saturationFactor, intensityFactor);

            // Assert
            Assert.IsTrue(File.Exists(resultPath), "Histogram adjusted image was not created.");
        }

        // Tests that ApplyHistogramAdjustment throws an exception for an invalid file path.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ApplyHistogramAdjustment_InvalidInput_ThrowsException()
        {
            // Arrange
            HistogramAdjustment histogramAdjustment = new HistogramAdjustment();

            // Act
            histogramAdjustment.ApplyHistogramAdjustment("invalid_path.jpg", outputImagePath, 1.2, 1.1);
        }

        // Cleans up test environment by deleting created images.
        [TestCleanup]
        public void Cleanup()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            // Delete test images
            if (File.Exists(outputImagePath)) File.Delete(outputImagePath);
        }
    }
}
