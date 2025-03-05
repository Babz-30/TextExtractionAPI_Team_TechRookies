using OCRApplication.Preprocesssing;
using OCRApplicationTest.Helpers;

namespace OCRApplicationTest;

/// <summary>
/// Test class for CannyFilter, verifying image edge detection functionality.
/// </summary>
[TestClass]
public class CannyFilterTest
{
    private string testImagePath;
    private string outputImagePath;

    // Sets up test environment input and output image path.
    [TestInitialize]
    public void Setup()
    {
        testImagePath = TestUtilityClass.InputImagePath("test_image.jpg");
        outputImagePath = TestUtilityClass.OutputImagePath("canny.jpg");
    }

    // Tests that ApplyCannyEdgeDetection correctly processes an image.
    [TestMethod]
    public void ApplyCannyEdgeDetection_ValidInput_CreatesEdgeDetectedImage()
    {
        // Arrange
        CannyFilter cannyFilter = new CannyFilter();
        int threshold1 = 100;
        int threshold2 = 200;

        // Act
        string resultPath = cannyFilter.ApplyCannyEdgeDetection(testImagePath, outputImagePath, threshold1, threshold2);

        // Assert
        Assert.IsTrue(File.Exists(resultPath), "Edge detected image was not created.");
    }

    // Tests that ApplyCannyEdgeDetection throws an exception for an invalid file path.
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ApplyCannyEdgeDetection_InvalidInput_ThrowsException()
    {
        // Arrange
        CannyFilter cannyFilter = new CannyFilter();

        // Act
        cannyFilter.ApplyCannyEdgeDetection("invalid_path.jpg", outputImagePath, 100, 200);
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
