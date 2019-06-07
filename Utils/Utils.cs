using System;
using System.Collections.Generic;

namespace GaussianFilter.Utils
{
    public class Utils
    {
        /// <summary>
        /// Multiplies two matrixes
        /// </summary>
        /// <param name="image"></param>
        /// <param name="kernel"></param>
        /// <returns>result from multiplication</returns>
        public static IMatrix Convolute(IMatrix image, IMatrix kernel)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (kernel == null)
            {
                throw new ArgumentNullException(nameof(kernel));
            }

            var resultMatrixSize = CalculateConvolutedImageDimensions(image, kernel);
            var resultMatrix = new Matrix(resultMatrixSize[0], resultMatrixSize[1]);

            for (var rowIndex = 0; rowIndex < resultMatrix.Height; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < resultMatrix.Width; columnIndex++)
                {
                    var newValue = CalculateValueForPosition(rowIndex, columnIndex, image, kernel);
                    resultMatrix.SetValue(columnIndex, rowIndex, newValue);
                }
            }

            return resultMatrix;
        }

        /// <summary>
        /// Calculate value for position in result matrix (made from multiplication of image and kernel)
        /// </summary>
        /// <param name="row">current result matrix row</param>
        /// <param name="column">current result matrix column</param>
        /// <param name="image">image</param>
        /// <param name="kernel">kernel</param>
        /// <returns>calculated value for specified position</returns>
        private static float CalculateValueForPosition(int row, int column, IMatrix image,
            IMatrix kernel)
        {
            float endValue = 0;

            for (var i = 0; i < kernel.Height; i++)
            {
                float innerCycleCalculationResult = 0;

                for (var j = 0; j < kernel.Width; j++)
                {
                    innerCycleCalculationResult += image.GetValue(column + j - 1, row + i - 1) * kernel.GetValue(j, i);
                }

                endValue += innerCycleCalculationResult;
            }

            return endValue;
        }

        /// <summary>
        /// Calculates convoluted image size
        /// </summary>
        /// <param name="image">source matrix (image)</param>
        /// <param name="kernel">kernel matrix</param>
        /// <returns>array of integers. 0 position is convoluted image width, 1 is convoluted image height</returns>
        private static int[] CalculateConvolutedImageDimensions(IMatrix image, IMatrix kernel)
        {
            return
                new[]
                {
                    image.Width - kernel.Width - 1,
                    image.Height - kernel.Height + 1
                };
        }
    }
}