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

    // Sets up test environment input and output image path.
    [TestInitialize]
    public void Setup()
    {
    }

    // Tests that ResizingImage correctly creates a resized image with the specified DPI.
    [TestMethod]
    public void ResizingImage_ValidInput_CreatesResizedImage()
    {
        // Arrange
        ResizeImage resizeImage = new();
        int targetDPI = 300;

        // Act
        string resultPath = ResizeImage.ResizingImage(TestUtilityClass.InputImagePath("test_image.jpg"), outputImagePath, targetDPI);

        // Assert
        Assert.IsTrue(File.Exists(resultPath), "Resized image was not created.");
    }

    // Tests that ResizingImage throws an exception when given an invalid file path.
    [TestMethod]
    public void ResizingImage_InvalidInput_ThrowsException()
    {
        // Arrange
        ResizeImage resizeImage = new();
        int targetDPI = 300;

        // Act
        Assert.ThrowsExactly<ArgumentException>(() => ResizeImage.ResizingImage("invalid_path.jpg", outputImagePath, targetDPI));
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
