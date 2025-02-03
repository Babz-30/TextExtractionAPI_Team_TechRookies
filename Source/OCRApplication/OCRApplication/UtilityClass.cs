namespace OCRApplication
{
    public static class UtilityClass
    {

        // Path to input image directory
        static readonly string inputImageDirectory = @"path_to_image";

        // Path to the trained tessdata folder
        static readonly string tessDataPath = @"path_to_tessdata";
        public static string SolutionDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            DirectoryInfo? parentDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent;

            string parentFullName = parentDirectory?.FullName ?? "Parent directory does not exist";

            return parentFullName;

        }

        public static string ImageDirectory()
        {
            return Path.Combine(SolutionDirectory(), inputImageDirectory);
        }

        public static string ImagePath(string filename)
        {
            string imagePath = Path.Combine(ImageDirectory(), filename);

            if (!File.Exists(imagePath))
            {
                Console.WriteLine("Image not found.");
            }

            return imagePath;
        }

        public static string OutputImagePath(string filename)
        {
            string imagePath = Path.Combine(ImageDirectory(), filename);

            if (!File.Exists(imagePath))
            {
                File.Create(imagePath);
            }

            return imagePath;
        }

        public static string TessDataPath()
        {
            return Path.Combine(SolutionDirectory(), tessDataPath);

        }

    }
}