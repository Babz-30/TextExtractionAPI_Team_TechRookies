using OCRApplication.Preprocesssing;
using OCRApplicationTest.Helpers;

namespace OCRApplicationTest.PreprocessingTest;

/// <summary>
/// Test class for CannyFilter, verifying image edge detection functionality.
/// </summary>
[TestClass]
public class CannyFilterTest
{
    private readonly string outputImagePath = TestUtilityClass.OutputImagePath("canny.jpg");


    /// <summary>
    /// Tests that ApplyCannyEdgeDetection correctly processes an image.
    /// </summary>
    [TestMethod]
    public void ApplyCannyEdgeDetection_ValidInput_CreatesEdgeDetectedImage()
    {
        // Arrange
        int threshold1 = 100;
        int threshold2 = 200;

        // Act
        string resultPath = CannyFilter.ApplyCannyEdgeDetection(TestUtilityClass.InputImagePath("test_image.jpg"), outputImagePath, threshold1, threshold2);

        // Assert
        Assert.IsTrue(File.Exists(resultPath), "Edge detected image was not created.");
    }

    /// <summary>
    /// Tests that ApplyCannyEdgeDetection throws an exception for an invalid file path.
    /// </summary>
    [TestMethod]
    public void ApplyCannyEdgeDetection_InvalidInput_ThrowsException()
    {
        // Act
        Assert.ThrowsExactly<ArgumentException>(() => CannyFilter.ApplyCannyEdgeDetection("invalid_path.jpg", outputImagePath, 100, 200));
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
