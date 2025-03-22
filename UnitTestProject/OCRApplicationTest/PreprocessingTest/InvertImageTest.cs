using OCRApplicationTest.Helpers;
using OCRApplication.Preprocesssing;

namespace OCRApplicationTest.PreprocessingTest;

/// <summary>
/// Test class for InvertImage, verifying image invert functionality.
/// </summary>
[TestClass]
public class InvertImageTest
{
    private readonly string outputImagePath = TestUtilityClass.OutputImagePath("invert.jpg");

    // Sets up test environment input and output image path.
    [TestInitialize]
    public void Setup()
    {
    }

    /// <summary>
    /// Tests that InvertingImage correctly creates an inverted image.
    /// </summary>
    [TestMethod]
    public void InvertingImage_ValidInput_CreatesInvertedImage()
    {
        // Arrange
        InvertImage invertImage = new();

        // Act
        string resultPath = InvertImage.InvertingImage(TestUtilityClass.InputImagePath("test_image.jpg"), outputImagePath);

        // Assert
        Assert.IsTrue(File.Exists(resultPath), "Inverted image was not created.");
    }

    // Tests that InvertingImage throws an exception when given an invalid file path.
    [TestMethod]
    public void InvertingImage_InvalidInput_ThrowsException()
    {
        // Arrange
        InvertImage invertImage = new();

        // Act
        Assert.ThrowsExactly<ArgumentException>(() => InvertImage.InvertingImage("invalid_path.jpg", outputImagePath));
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
