using System;
using System.Collections.Generic;

namespace GaussianFilter.Utils
{
    public class Utils<TCellValue>
        where TCellValue : struct, IConvertible, IComparable<TCellValue>, IComparable, IEquatable<TCellValue>
    {
        /// <summary>
        /// Multiplies two matrixes
        /// </summary>
        /// <param name="image"></param>
        /// <param name="kernel"></param>
        /// <returns>result from multiplication</returns>
        public static IMatrix<TCellValue> Convolute(IMatrix<TCellValue> image, IMatrix<TCellValue> kernel)
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
            var resultMatrix = new Matrix<TCellValue>(resultMatrixSize[0], resultMatrixSize[1]);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates convoluted image size
        /// </summary>
        /// <param name="image">source matrix (image)</param>
        /// <param name="kernel">kernel matrix</param>
        /// <returns>array of integers. 0 position is convoluted image width, 1 is convoluted image height</returns>
        private static int[] CalculateConvolutedImageDimensions(IMatrix<TCellValue> image, IMatrix<TCellValue> kernel)
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