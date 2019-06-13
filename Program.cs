using System;

namespace GaussianFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            var image = MatrixUtils.CreateMatrixFromImage(
                "/home/dead4y/Desktop/programming/BGReceiptScanner/GaussianFilter/photo.jpeg");
            var kernel = GenerateGaussianKernel();
            var convolutedImageMatrix = MatrixUtils.ConvertMatrixToRGBMatrix(image.Convolute(kernel));
            MatrixUtils.CreateImageFromMatrix(convolutedImageMatrix,
                "/home/dead4y/Desktop/programming/BGReceiptScanner/GaussianFilter/photo-blurred.jpeg");
        }

        static IMatrix GenerateGaussianKernel()
        {
            var kernel = new Matrix(5, 5, typeof(FloatNumberMatrixData));

            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    kernel.SetValue(i + 2, j + 2, new FloatNumberMatrixData((float) Gaussian2D(j, i, 1)));
                }
            }

            return kernel;
        }

        static double Gaussian2D(int x, int y, float standardDeviation)
        {
            return
            (
                Math.Exp(-(((Math.Pow(x, 2) + Math.Pow(y, 2)) / (2 * Math.Pow(standardDeviation, 2)))))
                /
                (2 * Math.PI * Math.Pow(standardDeviation, 2))
            );
        }
    }
}