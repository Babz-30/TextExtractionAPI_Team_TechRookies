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

        /// <summary>
        /// Tests that ConvertToGrayscale correctly processes an image.
        /// </summary>
        [TestMethod]
        public void ConvertToGrayscale_ValidInput_CreatesGrayscaleImage()
        {
            // Act
            string resultPath = Grayscale.ConvertToGrayscale(TestUtilityClass.InputImagePath("test_image.jpg"), outputImagePath);

            // Assert
            Assert.IsTrue(File.Exists(resultPath), "Grayscale image was not created.");
        }

        /// <summary>
        /// Tests that ConvertToGrayscale throws an exception for an invalid file path.
        /// </summary>
        [TestMethod]
        public void ConvertToGrayscale_InvalidInput_ThrowsException()
        {
            // Act
            Assert.ThrowsExactly<ArgumentException>(() => Grayscale.ConvertToGrayscale("invalid_path.jpg", outputImagePath));
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
