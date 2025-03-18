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

        // Sets up test environment input and output image path.
        [TestInitialize]
        public void Setup()
        {
        }

        // Tests that ApplyRotation correctly creates a rotated image.
        [TestMethod]
        public void ApplyRotation_ValidInput_CreatesRotatedImage()
        {
            // Arrange
            RotateImage rotateImage = new();
            float angle = 45f;

            // Act
            string resultPath = rotateImage.ApplyRotation(TestUtilityClass.InputImagePath("test_image.jpg"), outputImagePath, angle);

            // Assert
            Assert.IsTrue(File.Exists(resultPath), "Output image was not created.");
        }

        // Tests that ApplyRotation throws an exception when given an invalid file path.
        [TestMethod]
        public void ApplyRotation_InvalidInput_ThrowsException()
        {
            // Arrange
            RotateImage rotateImage = new();

            // Act
            Assert.ThrowsExactly<FileNotFoundException>(() => rotateImage.ApplyRotation("invalid_path.jpg", outputImagePath, 45f));
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