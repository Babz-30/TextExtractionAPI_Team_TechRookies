using OCRApplication.Preprocesssing;
using OCRApplicationTest.Helpers;

namespace OCRApplicationTest.PreprocessingTest
{
    /// <summary>
    /// Test class for Binarization, verifying image thresholding functionality.
    /// </summary>
    [TestClass]
    public class BinarizationTest
    {
        private readonly string outputImagePath = TestUtilityClass.OutputImagePath("binarized.jpg");

        // Sets up test environment input and output image paths.
        [TestInitialize]
        public void Setup()
        {
        }

        /// <summary>
        /// Tests that ApplyOtsuBinarization correctly processes an image. 
        /// </summary>
        [TestMethod]
        public void ApplyOtsuBinarization_ValidInput_CreatesBinarizedImage()
        {
            // Act
            string? resultPath = Binarization.ApplyOtsuBinarization(TestUtilityClass.InputImagePath("test_image.jpg"), outputImagePath: outputImagePath);

            // Assert
            Assert.IsTrue(File.Exists(resultPath), "Binarized image was not created.");
        }

        /// <summary>
        /// Tests that ApplyOtsuBinarization throws an exception for an invalid file path.
        /// </summary>
        [TestMethod]
        public void ApplyOtsuBinarization_InvalidInput_ThrowsException()
        {
            // Arrange
            Binarization binarization = new();

            // Act
            Assert.ThrowsExactly<ArgumentException>(() => Binarization.ApplyOtsuBinarization("invalid_path.jpg", outputImagePath));
        }

        /// <summary>
        /// Cleans up test environment by deleting created images. 
        /// </summary>
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