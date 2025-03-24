using OCRApplication.Preprocesssing;
using OCRApplicationTest.Helpers;

namespace OCRApplicationTest.PreprocessingTest
{
    /// <summary>
    /// Test class for HistogramAdjustment, verifying image enhancement functionality.
    /// </summary>
    [TestClass]
    public class HistogramAdjustmentTest 
    {
        private readonly string outputImagePath = TestUtilityClass.OutputImagePath("histogram_adjusted.jpg");

        /// <summary>
        /// Tests that ApplyHistogramAdjustment throws an exception for an invalid file path.
        /// </summary>
        [TestMethod]
        public void ApplyHistogramAdjustment_InvalidInput_ThrowsException()
        {
            // Arrange
            HistogramAdjustment histogramAdjustment = new();

            // Act
            Assert.ThrowsExactly<ArgumentException>(() => HistogramAdjustment.ApplyHistogramAdjustment("invalid_path.jpg", outputImagePath, 1.2, 1.1));
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
