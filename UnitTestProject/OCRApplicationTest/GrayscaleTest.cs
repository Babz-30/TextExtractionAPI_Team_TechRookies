using NUnit.Framework;
using OCRApplication.Preprocesssing;
using System;
using System.IO;

namespace OCRApplication.Tests
{
    [TestFixture]
    public class GrayscaleTests
    {
        private string _testInputPath;
        private string _testOutputPath;

        [SetUp]
        public void Setup()
        {
            // Set up test image paths
            _testInputPath = "test_input.jpg";
            _testOutputPath = "test_output.jpg";

            // Create a dummy input image
            using (var bitmap = new System.Drawing.Bitmap(10, 10))
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(x * 10, y * 10, 100));
                    }
                }
                bitmap.Save(_testInputPath);
            }
        }

        
    }
}
