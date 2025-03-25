using OCRApplicationTest.Helpers;
using OCRApplication.Preprocesssing;

namespace OCRApplicationTest.PreprocessingTest;

/// <summary>
/// Test class for ResizeImage, verifying image resize functionality.
/// </summary>
[TestClass]
public class ResizeImageTest 
{
    private readonly string outputImagePath = TestUtilityClass.OutputImagePath("resize.jpg");

    /// <summary>
    /// Tests that ResizingImage throws an exception when given an invalid file path.
    /// </summary>
    [TestMethod]
    public void ResizingImage_InvalidInput_ThrowsException()
    {
        // Arrange
        ResizeImage resizeImage = new();
        int targetDPI = 300;

        // Act
        Assert.ThrowsExactly<ArgumentException>(() => ResizeImage.ResizingImage("invalid_path.jpg", outputImagePath, targetDPI));
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
