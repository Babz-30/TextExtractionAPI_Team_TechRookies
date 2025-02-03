namespace OCRApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                // Path to your image file
                string inputImagePath = UtilityClass.ImagePath("image1.jpg");

                Console.WriteLine("Select type of preprocessing image?\n 1. Rotate \n 2. Mirror \n 3. Invert ");
                var option = Console.ReadLine();
                string preprocessedImagePath;
                IPreprocessing image;

                switch (option)
                {
                    case "1":
                        inputImagePath = UtilityClass.ImagePath("rotated_image.jpg");
                        // Rotating input image with given angle
                        image = new RotateImage(inputImagePath, 45);
                        preprocessedImagePath = image.Process();
                        break;

                    case "2":
                        inputImagePath = UtilityClass.ImagePath("mirrored_image_verticle3_cb.png");

                        // Check if the input image exists
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
                        inputImagePath = UtilityClass.ImagePath("image3.jpg");
                        // Inverting the input image
                        image = new InvertImage(inputImagePath);
                        preprocessedImagePath = image.Process();
                        break;

                    case "4":
                        inputImagePath = UtilityClass.ImagePath("image1.jpg");
                        // Resize the input image
                        image = new ResizeImage(inputImagePath);
                        preprocessedImagePath = image.Process();
                        break;

                    case "5":
                        inputImagePath = UtilityClass.ImagePath("image4.jpg");
                        // Denoise the input image
                        image = new DenoiseImage(inputImagePath);
                        preprocessedImagePath = image.Process();
                        break;

                    default:
                        preprocessedImagePath = inputImagePath;
                        Console.WriteLine("Default option selected. Using the original image without preprocessing.");
                        break;
                }

                //Extracting text from image
                TextExtraction tx = new();
                tx.ExtractText(preprocessedImagePath, "eng");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}