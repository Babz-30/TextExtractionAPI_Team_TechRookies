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
        private readonly string outputImagePath = TestUtilityClass.OutputImagePath("grayscale.jpg");

        // Sets up test environment input and output image paths.
        [TestInitialize]
        public void Setup()
        {
        }

        // Tests that ConvertToGrayscale correctly processes an image.
        [TestMethod]
        public void ConvertToGrayscale_ValidInput_CreatesGrayscaleImage()
        {
            // Act
            string resultPath = Grayscale.ConvertToGrayscale(TestUtilityClass.InputImagePath("test_image.jpg"), outputImagePath);

            // Assert
            Assert.IsTrue(File.Exists(resultPath), "Grayscale image was not created.");
        }

        // Tests that ConvertToGrayscale throws an exception for an invalid file path.
        [TestMethod]
        public void ConvertToGrayscale_InvalidInput_ThrowsException()
        {
            // Act
            Assert.ThrowsExactly<ArgumentException>(() => Grayscale.ConvertToGrayscale("invalid_path.jpg", outputImagePath));
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
