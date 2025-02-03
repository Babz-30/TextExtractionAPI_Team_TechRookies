using System.Drawing;

namespace OCRApplication
{
    public static class Binarization
    {
        public static Bitmap ApplyBinarization(Bitmap inputImage, int threshold = 128)
        {
            Bitmap binarized = new Bitmap(inputImage.Width, inputImage.Height);
            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    Color pixel = inputImage.GetPixel(x, y);
                    int binaryColor = pixel.R > threshold ? 255 : 0;
                    binarized.SetPixel(x, y, Color.FromArgb(binaryColor, binaryColor, binaryColor));
                }
            }
            return binarized;
        }
    }
}
