namespace OCRApplication
{
    public static class UtilityClass
    {

        // Path to input image directory
        static readonly string inputImageDirectory = @"path_to_image";

        // Path to the trained tessdata folder
        static readonly string tessDataPath = @"path_to_tessdata";

        // Path to extracted text from chatgpt
        static readonly string chatgptOutputFolder = @"chatgpt_output";

        // Path to extracted text after preprocessing
        static readonly string tesseractOutputFolder = @"tesseract_output";


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

        public static string OutputImagePath(string filename)
        {
            string imagePath = Path.Combine(InputImageDirectory(), filename);

            if (!File.Exists(imagePath))
            {
                File.Create(imagePath);
            }

            return imagePath;
        }

        public static string ChatgptOutputPath(string filename)
        {
            string textFileDirectory = Path.Combine(SolutionDirectory(), chatgptOutputFolder);

            string textFilePath = Path.Combine(textFileDirectory, filename);

            if (!File.Exists(textFilePath))
            {
                File.Create(textFilePath);
            }

            return textFilePath;
        }

        public static string TesseractOutputPath(string filename)
        {
            string textFileDirectory = Path.Combine(SolutionDirectory(), tesseractOutputFolder);

            string textFilePath = Path.Combine(textFileDirectory, filename);

            if (!File.Exists(textFilePath))
            {
                File.Create(textFilePath);
            }

            return textFilePath;
        }
        public static string TessDataPath()
        {
            return Path.Combine(SolutionDirectory(), tessDataPath);

        }

        public static void RemoveBlankLines(string inputFile)
        {
            var lines = File.ReadAllLines(inputFile)
                            .Where(line => !string.IsNullOrWhiteSpace(line))
                            .ToArray();
            File.WriteAllLines(inputFile, lines);
        }

        public static void SaveToFile(string filePath, string content)
        {
            Console.WriteLine("Extracted Text:");
            Console.WriteLine(content);
            File.WriteAllText(filePath, content);
            Console.WriteLine("Extracted content saved to " + filePath);
        }
    }
}