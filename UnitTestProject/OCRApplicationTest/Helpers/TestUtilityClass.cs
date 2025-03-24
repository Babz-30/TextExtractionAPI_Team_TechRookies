using Microsoft.CodeCoverage.Core.Reports.Coverage;

namespace OCRApplicationTest.Helpers
{
    /// <summary>
    /// Provides utility methods for handling file paths related to input and output images.
    /// </summary>
    public static class TestUtilityClass
    {
        // Path to input image directory
        static readonly string inputImageDirectory = @"Input";

        // Path to preprocessed image directory
        static readonly string outputImageDirectory = @"Output\preprocessed\";


        /// <summary>
        /// Gets the solution directory by navigating three levels up from the current directory.
        /// </summary>
        /// <returns>Path to the solution directory.</returns>
        public static string SolutionDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            DirectoryInfo? parentDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent;

            string parentFullName = parentDirectory?.FullName ?? "Parent directory does not exist";

            return parentFullName;

        }

        /// <summary>
        /// Gets the full path to the input image directory within the solution.
        /// </summary>
        /// <returns>Path to the input image directory.</returns>
        public static string InputImageDirectory()
        {
            return Path.Combine(SolutionDirectory(), inputImageDirectory);
        }

        /// <summary>
        /// Gets the full path to an input image file.
        /// </summary>
        /// <param name="filename">Name of the image file.</param>
        /// <returns>Full path to the input image file.</returns>
        public static string InputImagePath(string filename)
        {
            string imagePath = Path.Combine(InputImageDirectory(), filename);

            if (!File.Exists(imagePath))
            {
                Console.WriteLine("Image not found.");
            }

            return imagePath;
        }

        /// <summary>
        /// Gets the full path to the output image directory, creating it if it does not exist.
        /// </summary>
        /// <returns>Path to the output image directory.</returns>
        public static string OutputImageDirectory()
        {
            string directoryPath = Path.Combine(SolutionDirectory(), outputImageDirectory);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            return directoryPath; 
        }

        /// <summary>
        /// Gets the full path to an output image file.
        /// </summary>
        /// <param name="filename">Name of the output image file.</param>
        /// <returns>Full path to the output image file.</returns>
        public static string OutputImagePath(string filename)
        {
            string imagePath = Path.Combine(OutputImageDirectory(), filename);

            return imagePath;
        }
    }
}
