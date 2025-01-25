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
                        inputImagePath = UtilityClass.ImagePath("mirror_image1.png");
                        // Mirroring the input image
                        image = new MirrorImage(inputImagePath);
                        preprocessedImagePath = image.Process();
                        break;

                    case "3":
                        inputImagePath = UtilityClass.ImagePath("image3.jpg");
                        // Inverting the input image
                        image = new InvertImage(inputImagePath);
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
