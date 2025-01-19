namespace OCRApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to input image directory
            string inputImageDirectory = @"path_to_image\";

            if (!Directory.Exists(inputImageDirectory))
            {
                Directory.CreateDirectory(inputImageDirectory);
            }

            // Path to your image file
            string inputImagePath = Path.Combine(inputImageDirectory, "image1.jpg");
            

            try
            {
                Console.WriteLine("Select type of preprocessing image?\n 1. Rotate \n 2. Mirror ");
                var option = Console.ReadLine();
                string preprocessedImagePath;

                switch (option)
                {
                    case "1":
                        inputImagePath = Path.Combine(inputImageDirectory, "rotated_image.jpg");
                        //Rotating input image with given angle
                        IPreprocessing image = new RotateImage(inputImagePath, 45);
                        preprocessedImagePath = image.Process();
                        break;

                    case "2":
                        preprocessedImagePath = inputImagePath;
                        break;

                    default:
                        preprocessedImagePath = inputImagePath;
                        break;
                }

                //Extracting text from image
                TextExtraction tx = new TextExtraction();
                tx.extractText(preprocessedImagePath, "eng");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
