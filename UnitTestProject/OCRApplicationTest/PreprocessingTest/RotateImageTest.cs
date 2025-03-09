using OCRApplication.Preprocesssing;
using OCRApplicationTest.Helpers;

namespace OCRApplicationTest
{
    /// <summary>
    /// Test class for RotateImage, verifying image rotation functionality.
    /// </summary>
    [TestClass]
    public class RotateImageTest
    {
        private string testImagePath;
        private string outputImagePath;

        // Sets up test environment input and output image path.
        [TestInitialize]
        public void Setup()
        {
            testImagePath = TestUtilityClass.InputImagePath("test_image.jpg");
            outputImagePath = TestUtilityClass.OutputImagePath("rotation.jpg");
        }

        // Tests that ApplyRotation correctly creates a rotated image.
        [TestMethod]
        public void ApplyRotation_ValidInput_CreatesRotatedImage()
        {
            // Arrange
            RotateImage rotateImage = new RotateImage();
            float angle = 45f;

            // Act
            string resultPath = rotateImage.ApplyRotation(testImagePath, outputImagePath, angle);

            // Assert
            Assert.IsTrue(File.Exists(resultPath), "Output image was not created.");
        }

        // Tests that ApplyRotation throws an exception when given an invalid file path.
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ApplyRotation_InvalidInput_ThrowsException()
        {
            // Arrange
            RotateImage rotateImage = new RotateImage();

            // Act
            rotateImage.ApplyRotation("invalid_path.jpg", outputImagePath, 45f);
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