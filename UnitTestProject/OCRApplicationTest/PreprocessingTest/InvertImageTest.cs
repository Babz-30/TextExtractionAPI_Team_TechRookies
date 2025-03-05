using OCRApplicationTest.Helpers;
using OCRApplication.Preprocesssing;

namespace OCRApplicationTest;

/// <summary>
/// Test class for InvertImage, verifying image invert functionality.
/// </summary>
[TestClass]
public class InvertImageTest
{
    private string testImagePath;
    private string outputImagePath;

    // Sets up test environment input and output image path.
    [TestInitialize]
    public void Setup()
    {
        testImagePath = TestUtilityClass.InputImagePath("test_image.jpg");
        outputImagePath = TestUtilityClass.OutputImagePath("invert.jpg");
    }

    /// <summary>
    /// Tests that InvertingImage correctly creates an inverted image.
    /// </summary>
    [TestMethod]
    public void InvertingImage_ValidInput_CreatesInvertedImage()
    {
        // Arrange
        InvertImage invertImage = new InvertImage();

        // Act
        string resultPath = invertImage.InvertingImage(testImagePath, outputImagePath);

        // Assert
        Assert.IsTrue(File.Exists(resultPath), "Inverted image was not created.");
    }

    // Tests that InvertingImage throws an exception when given an invalid file path.
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void InvertingImage_InvalidInput_ThrowsException()
    {
        // Arrange
        InvertImage invertImage = new InvertImage();

        // Act
        invertImage.InvertingImage("invalid_path.jpg", outputImagePath);
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
