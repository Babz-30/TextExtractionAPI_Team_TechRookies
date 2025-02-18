using NUnit.Framework;
using OCRApplication.Preprocesssing;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.Versioning;

namespace OCRApplication.Tests
{
    [TestFixture]
    public class BinarizationTest
    {
        private string _testInputPath;
        private string _testOutputPath;

        public BinarizationTest()
        {
            // Provide valid test image paths
            _testInputPath = "test_input.jpg";   // Replace with an actual test image path
            _testOutputPath = "test_output.jpg"; // Output path
        }

        [Test]
        [SupportedOSPlatform("windows")] // Fix for platform warnings
        public void ApplyBinarization_ShouldCreateBinarizedImage()
        {
            // Ensure test input image exists
            if (!File.Exists(_testInputPath))
            {
                Assert.Fail("Test input image not found!");
            }

            // Apply binarization
            string resultPath = Binarization.ApplyBinarization(_testInputPath, _testOutputPath);

            // Check if output file is created
            Assert.That(File.Exists(resultPath), "Binarized image file was not created.");

            // Verify output image is not empty
            using (Bitmap resultImage = new Bitmap(resultPath))
            {
                Assert.That(resultImage.Width > 0 && resultImage.Height > 0, "Output image is empty.");
            }
        }

        //
    }
}
