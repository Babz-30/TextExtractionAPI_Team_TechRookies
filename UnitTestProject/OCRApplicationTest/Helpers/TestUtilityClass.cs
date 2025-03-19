using Microsoft.CodeCoverage.Core.Reports.Coverage;

namespace OCRApplicationTest.Helpers
{
    public static class TestUtilityClass
    {
        // Path to input image directory
        static readonly string inputImageDirectory = @"Input";

        // Path to preprocessed image directory
        static readonly string outputImageDirectory = @"Output\preprocessed\";

        public static string SolutionDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            DirectoryInfo? parentDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent;

            string parentFullName = parentDirectory?.FullName ?? "Parent directory does not exist";

            return parentFullName;

        }
        public static string InputImageDirectory()
        {
            return Path.Combine(SolutionDirectory(), inputImageDirectory);
        }

        public static string InputImagePath(string filename)
        {
            string imagePath = Path.Combine(InputImageDirectory(), filename);

            if (!File.Exists(imagePath))
            {
                Console.WriteLine("Image not found.");
            }

            return imagePath;
        }

        public static string OutputImageDirectory()
        {
            string directoryPath = Path.Combine(SolutionDirectory(), outputImageDirectory);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            return directoryPath; 
        }
        public static string OutputImagePath(string filename)
        {
            string imagePath = Path.Combine(OutputImageDirectory(), filename);

            return imagePath;
        }
    }
}
