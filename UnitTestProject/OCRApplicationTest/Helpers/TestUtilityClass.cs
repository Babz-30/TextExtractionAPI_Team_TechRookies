﻿namespace OCRApplicationTest.Helpers
{
    public static class TestUtilityClass
    {
        // Path to input image directory
        static readonly string inputImageDirectory = @"path_to_image";

        // Path to preprocessed image directory
        static readonly string outputImageDirectory = @"path_to_image/preprocessed/";

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
            return Path.Combine(SolutionDirectory(), outputImageDirectory);
        }
        public static string OutputImagePath(string filename)
        {
            string imagePath = Path.Combine(OutputImageDirectory(), filename);

            return imagePath;
        }
    }
}
