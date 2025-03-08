using OCRApplicationTest.Helpers;
using OCRApplication.Preprocesssing;

namespace OCRApplicationTest;

/// <summary>
/// Test class for ResizeImage, verifying image resize functionality.
/// </summary>
[TestClass]
public class ResizeImageTest
{
    private string testImagePath;
    private string outputImagePath;

    // Sets up test environment input and output image path.
    [TestInitialize]
    public void Setup()
    {
        testImagePath = TestUtilityClass.InputImagePath("test_image.jpg");
        outputImagePath = TestUtilityClass.OutputImagePath("resize.jpg");
    }

    // Tests that ResizingImage correctly creates a resized image with the specified DPI.
    [TestMethod]
    public void ResizingImage_ValidInput_CreatesResizedImage()
    {
        // Arrange
        ResizeImage resizeImage = new ResizeImage();
        int targetDPI = 300;

        // Act
        string resultPath = resizeImage.ResizingImage(testImagePath, outputImagePath, targetDPI);

        // Assert
        Assert.IsTrue(File.Exists(resultPath), "Resized image was not created.");
    }

    // Tests that ResizingImage throws an exception when given an invalid file path.
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ResizingImage_InvalidInput_ThrowsException()
    {
        // Arrange
        ResizeImage resizeImage = new ResizeImage();
        int targetDPI = 300;

        // Act
        resizeImage.ResizingImage("invalid_path.jpg", outputImagePath, targetDPI);
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
