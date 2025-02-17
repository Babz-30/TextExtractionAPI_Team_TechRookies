﻿using System.Drawing;

namespace OCRApplication.Preprocesssing
{
    internal class InvertImage
    {
        public string InvertingImage(string inputImagePath, string outputImagePath)
        {
            // Load the image
            using Bitmap bitmap = new(inputImagePath);

            // Process each pixel
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    // Get the pixel color
                    Color originalColor = bitmap.GetPixel(x, y);

                    // Invert the color
                    Color invertedColor = Color.FromArgb(255 - originalColor.R, 255 - originalColor.G, 255 - originalColor.B);

                    // Set the new color
                    bitmap.SetPixel(x, y, invertedColor);
                }
            }

            // Save the modified image
            bitmap.Save(outputImagePath);

            return outputImagePath;
        }
    }
}
