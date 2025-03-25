using OCRApplication.Preprocesssing;
using OCRApplicationTest.Helpers;

namespace OCRApplicationTest.PreprocessingTest
{
    /// <summary>
    /// Test class for RotateImage, verifying image rotation functionality.
    /// </summary>
    [TestClass]
    public class RotateImageTest
    {
        private readonly string outputImagePath = TestUtilityClass.OutputImagePath("rotation.jpg");

        /// <summary>
        /// Tests that ApplyRotation correctly creates a rotated image. 
        /// </summary>
        [TestMethod]
        public void ApplyRotation_ValidInput_CreatesRotatedImage()
        {
            float angle = 45f;

            // Act
            string resultPath = RotateImage.ApplyRotation(TestUtilityClass.InputImagePath("test_image.jpg"), outputImagePath, angle);

            // Assert
            Assert.IsTrue(File.Exists(resultPath), "Output image was not created.");
        }

        /// <summary>
        /// Tests that ApplyRotation throws an exception when given an invalid file path. 
        /// </summary>
        [TestMethod]
        public void ApplyRotation_InvalidInput_ThrowsException()
        {
            // Arrange
            RotateImage rotateImage = new();

            // Act
            Assert.ThrowsExactly<FileNotFoundException>(() => RotateImage.ApplyRotation("invalid_path.jpg", outputImagePath, 45f));
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