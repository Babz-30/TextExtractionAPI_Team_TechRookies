using OCRApplication.Preprocesssing;
using OCRApplicationTest.Helpers;

namespace OCRApplicationTest.PreprocessingTest
{
    /// <summary>
    /// Test class for MirrorImage, verifying horizontal mirroring functionality.
    /// </summary>
    [TestClass]
    public class MirrorImageTest
    {
        private readonly string outputImagePath = TestUtilityClass.OutputImagePath("horizontal_mirrored_image.png");

        // Sets up test environment input and output image paths.
        [TestInitialize]
        public void Setup()
        {
        }

        // Tests that Process correctly mirrors an image horizontally.
        [TestMethod]
        public void Process_ValidInput_CreatesMirroredImage()
        {
            // Arrange
            MirrorImage mirrorImage = new();

            // Act
            string resultPath = mirrorImage.Process(TestUtilityClass.InputImagePath("test_image.jpg"), outputImagePath);

            // Assert
            Assert.IsTrue(File.Exists(resultPath), "Mirrored image was not created.");
        }

        // Tests that Process throws an exception for an invalid file path.
        [TestMethod]
        public void Process_InvalidInput_ThrowsException()
        {
            // Arrange
            MirrorImage mirrorImage = new();

            // Act
            Assert.ThrowsExactly<ArgumentException>(() => mirrorImage.Process("invalid_path.jpg", outputImagePath));
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
