using OCRApplication.Preprocesssing;
using OCRApplicationTest.Helpers;
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OCRApplicationTest
{
    /// <summary>
    /// Test class for MirrorImage, verifying horizontal mirroring functionality.
    /// </summary>
    [TestClass]
    public class MirrorImageTest
    {
        private string testImagePath;
        private string outputImagePath;

        // Sets up test environment input and output image paths.
        [TestInitialize]
        public void Setup()
        {
            testImagePath = TestUtilityClass.InputImagePath("test_image.jpg");
            outputImagePath = TestUtilityClass.OutputImagePath("horizontal_mirrored_image.png");
        }

        // Tests that Process correctly mirrors an image horizontally.
        [TestMethod]
        public void Process_ValidInput_CreatesMirroredImage()
        {
            // Arrange
            MirrorImage mirrorImage = new MirrorImage(testImagePath);

            // Act
            string resultPath = mirrorImage.Process();

            // Assert
            Assert.IsTrue(File.Exists(resultPath), "Mirrored image was not created.");
        }

        // Tests that Process throws an exception for an invalid file path.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Process_InvalidInput_ThrowsException()
        {
            // Arrange
            MirrorImage mirrorImage = new MirrorImage("invalid_path.jpg");

            // Act
            mirrorImage.Process();
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
