using System.Drawing;

namespace GaussianFilter
{
    public class MatrixUtils
    {
        MatrixUtils()
        {
        }

        /// <summary>
        /// Creates RGBMatrix matrix from image
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns>Matrix containing image data</returns>
        public static IMatrix CreateMatrixFromImage(string imagePath)
        {
            var bitmapImage = new Bitmap(imagePath);
            var width = bitmapImage.Width;
            var height = bitmapImage.Height;
            var matrix = new RGBMatrix(width, height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var pixel = bitmapImage.GetPixel(j, i);
                    matrix.SetValue(j, i, new RGBMatrixData(pixel.R, pixel.G, pixel.B));
                }
            }

            return matrix;
        }
    }
}