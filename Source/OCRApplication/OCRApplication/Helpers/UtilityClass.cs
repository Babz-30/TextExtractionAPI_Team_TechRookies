namespace OCRApplication.Helpers
{
    public static class UtilityClass
    {

        // Path to input image directory
        static readonly string InputImageFolder = @"Input/Input_Images";

        // Path to preprocessed image directory
        static readonly string OutputImageFolder = @"Output/Preprocessed_Image_Output/";

        // Path to the trained tessdata folder
        static readonly string TrainedDataDirectory = @"Input/Trained_Data_Input";

        // Path to extracted text from chatgpt
        static readonly string ChatgptOutputFolder = @"Chatgpt_Output";

        // Path to extracted text after preprocessing
        static readonly string TesseractOutputFolder = @"Output/Tesseract_Output";

        static readonly string CosineSimilarityFolder = @"Output/Cosine_Similarity_Output";


        public static string SolutionDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            DirectoryInfo? parentDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent;

            string parentFullName = parentDirectory?.FullName ?? "Parent directory does not exist";

            return parentFullName;

        }

        public static string InputImageDirectory()
        {
            string directoryPath = Path.Combine(SolutionDirectory(), InputImageFolder);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            return directoryPath;
        }

        public static string OutputImageDirectory()
        {
            string directoryPath = Path.Combine(SolutionDirectory(), OutputImageFolder);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            return directoryPath;
        }

        public static string TesseractOutputDirectory()
        {
            string directoryPath = Path.Combine(SolutionDirectory(), TesseractOutputFolder);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            return directoryPath;
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
            string textFileDirectory = Path.Combine(SolutionDirectory(), ChatgptOutputFolder);

            string textFilePath = Path.Combine(textFileDirectory, filename);

            if (!File.Exists(textFilePath))
            {
                File.Create(textFilePath);
            }

            return textFilePath;
        }

        public static string TesseractOutputPath(string filename)
        {

            string textFilePath = Path.Combine(TesseractOutputDirectory(), filename);

            return textFilePath;
        }
        public static string TrainedDataPath()
        {
            return Path.Combine(SolutionDirectory(), TrainedDataDirectory);

        }

        public static string DictionaryPath()
        {
            return Path.Combine(TrainedDataPath(), "dictionary.txt");
        }

        public static string CosineSimilarityDirectory(string filename)
        {
            string cosineSimilarityDirectory = Path.Combine(SolutionDirectory(), CosineSimilarityFolder);
            string filePath = Path.Combine(cosineSimilarityDirectory, filename);
            return filePath;
        }

        public static void SaveToFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
            Console.WriteLine("Extracted Text saved to file.\n");
        }

        public static void DeleteAllFiles()
        {
            // List of folders to empty (change these paths)
            string[] folders = [
                TesseractOutputDirectory(),
                OutputImageDirectory()
            ];

            try
            {
                foreach (string folderPath in folders)
                {

                    // Check if the folder exists
                    if (Directory.Exists(folderPath))
                    {
                        // Delete all files in the folder
                        foreach (string file in Directory.GetFiles(folderPath))
                        {
                            File.Delete(file);
                        }

                        Console.WriteLine("Folder emptied successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to delete files in Folder: {ex.Message}");
            }
        }
    }
}