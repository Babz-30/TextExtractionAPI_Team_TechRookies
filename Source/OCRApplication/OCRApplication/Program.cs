using System;
using System.IO;

namespace OCRApplication
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Path to your image file
                string inputImagePath = UtilityClass.InputImagePath("image1.jpg");

                Console.WriteLine("Select type of preprocessing image?\n 1. Rotate \n 2. Mirror \n 3. Invert \n 4. Resize \n 5. Denoise");
                var option = Console.ReadLine();
                string preprocessedImagePath;
                IPreprocessing image;

                switch (option)
                {
                    case "1":
                        inputImagePath = UtilityClass.InputImagePath("rotated_image1.jpg"); 
                        image = new RotateImage(inputImagePath, 45);
                        preprocessedImagePath = image.Process();
                        break;

                    case "2":
                        inputImagePath = UtilityClass.InputImagePath("vertical_flipped_image.jpg");

                        if (!File.Exists(inputImagePath))
                        {
                            Console.WriteLine($"Input image not found: {inputImagePath}");
                            return;
                        }

                        Console.WriteLine("Select mirroring type:\n 1. Horizontal \n 2. Vertical");
                        string mirrorType = Console.ReadLine()?.Trim() == "1" ? "Horizontal" : "Vertical";

                        try
                        {
                            image = new MirrorImage(inputImagePath, mirrorType);
                            preprocessedImagePath = image.Process();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error during mirroring: {ex.Message}");
                            return;
                        }
                        break;

                    case "3":
                        inputImagePath = UtilityClass.InputImagePath("image3.jpg");
                        image = new InvertImage(inputImagePath);
                        preprocessedImagePath = image.Process();
                        break;

                    case "4":
                        inputImagePath = UtilityClass.InputImagePath("image1.jpg");
                        image = new ResizeImage(inputImagePath);
                        preprocessedImagePath = image.Process();
                        break;

                    case "5":
                        inputImagePath = UtilityClass.InputImagePath("image4.jpg");
                        image = new DenoiseImage(inputImagePath);
                        preprocessedImagePath = image.Process();
                        break;

                    default:
                        preprocessedImagePath = inputImagePath;
                        Console.WriteLine("Default option selected. Using the original image without preprocessing.");
                        break;
                }

                // Call ChatGPT
                var chatGPT = new ChatGPT();
                chatGPT.Task(inputImagePath);

                // Extracting text from image
                TextExtraction tx = new();
                tx.ExtractText(preprocessedImagePath, "eng");

                // Compute and display cosine similarity
                string tesseractOutputPath = UtilityClass.TesseractOutputPath("tesseract_output.txt");
                string chatgptOutputPath = UtilityClass.ChatgptOutputPath("chatgpt_output.txt");

                if (File.Exists(tesseractOutputPath) && File.Exists(chatgptOutputPath))
                {
                    // Read text content from both files
                    string tesseractText = File.ReadAllText(tesseractOutputPath);
                    string chatgptText = File.ReadAllText(chatgptOutputPath);

                    //Calculate embeddings
                    List<double> t1 = await TextEmbedding.ComputeEmbedding(tesseractText);
                    List<double> t2 = await TextEmbedding.ComputeEmbedding(chatgptText);

                    // Compute similarity using the correct method
                    double similarityScore = TextSimilarity.ComputeCosineSimilarity(t1, t2);
                    Console.WriteLine($"Cosine Similarity between Tesseract and ChatGPT output: {similarityScore}");
                }
                else
                {
                    Console.WriteLine("Error: One or both text output files not found.");
                }

                // Before closing the terminal window, await user input.
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
