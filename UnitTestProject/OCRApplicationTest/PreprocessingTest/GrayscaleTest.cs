using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCRApplication.Preprocesssing;
using OCRApplicationTest.Helpers;

namespace OCRApplicationTest.PreprocessingTest
{
    /// <summary>
    /// Test class for Grayscale, verifying grayscale conversion functionality.
    /// </summary>
    [TestClass]
    public class GrayscaleTest
    {
        private string testImagePath;
        private string outputImagePath;

        // Sets up test environment input and output image paths.
        [TestInitialize]
        public void Setup()
        {
            testImagePath = TestUtilityClass.InputImagePath("test_image.jpg");
            outputImagePath = TestUtilityClass.OutputImagePath("grayscale.jpg");
        }

        // Tests that ConvertToGrayscale correctly processes an image.
        [TestMethod]
        public void ConvertToGrayscale_ValidInput_CreatesGrayscaleImage()
        {
            // Arrange
            Grayscale grayscaleConverter = new Grayscale();

            // Act
            string resultPath = grayscaleConverter.ConvertToGrayscale(testImagePath, outputImagePath);

            // Assert
            Assert.IsTrue(File.Exists(resultPath), "Grayscale image was not created.");
        }

        // Tests that ConvertToGrayscale throws an exception for an invalid file path.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConvertToGrayscale_InvalidInput_ThrowsException()
        {
            // Arrange
            Grayscale grayscaleConverter = new Grayscale();

            // Act
            grayscaleConverter.ConvertToGrayscale("invalid_path.jpg", outputImagePath);
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
