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

        /// <summary>
        /// Tests that Process correctly mirrors an image horizontally. 
        /// </summary>
        [TestMethod]
        public void Process_ValidInput_CreatesMirroredImage()
        {
            // Act
            string resultPath = MirrorImage.Process(TestUtilityClass.InputImagePath("test_image.jpg"), outputImagePath);

            // Assert
            Assert.IsTrue(File.Exists(resultPath), "Mirrored image was not created.");
        }

        /// <summary>
        /// Tests that Process throws an exception for an invalid file path.
        /// </summary>
        [TestMethod]
        public void Process_InvalidInput_ThrowsException()
        {
            // Arrange
            MirrorImage mirrorImage = new();

            // Act
            Assert.ThrowsExactly<ArgumentException>(() => MirrorImage.Process("invalid_path.jpg", outputImagePath));
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
