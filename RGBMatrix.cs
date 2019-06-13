using System;
using System.Collections.Generic;

namespace GaussianFilter
{
    public class RGBMatrix : Matrix
    {
        public RGBMatrix(IList<IList<IMatrixData>> matrix) : base(matrix)
        {
        }

        public RGBMatrix(int width, int height) : base(width, height, typeof(RGBMatrixData))
        {
        }

        /// <summary>
        /// Convolutes by kernel
        /// </summary>
        /// <param name="kernel"></param>
        /// <returns>result from multiplication</returns>
        public IMatrix Convolute(IMatrix kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException(nameof(kernel));
            }

            var resultMatrixSize = this.CalculateConvolutedImageDimensions(kernel);
            var resultMatrix = new Matrix(resultMatrixSize[0], resultMatrixSize[1], typeof(RGBMatrixData));

            for (var rowIndex = 0; rowIndex < resultMatrix.Height; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < resultMatrix.Width; columnIndex++)
                {
                    var newValue = CalculateValueForPosition(rowIndex, columnIndex, this, kernel);
                    resultMatrix.SetValue(columnIndex, rowIndex, newValue);
                }
            }

            return resultMatrix;
        }

        /// <summary>
        /// Calculate value for position in result matrix (made from multiplication of image and kernel)
        /// </summary>
        /// <param name="image">image</param>
        /// <param name="row">current result matrix row</param>
        /// <param name="column">current result matrix column</param>
        /// <param name="kernel">kernel</param>
        /// <param name="kernelSum">Optional. Sum of kernel matrix. If not specified every time it will calculate (bad for performance i guess)</param>
        /// <returns>calculated value for specified position</returns>
        private IMatrixData CalculateValueForPosition(int row, int column, IMatrix image, IMatrix kernel,
            float kernelSum = -1)
        {
            IMatrixData endValue = new FloatNumberMatrixData(0);

            for (var i = 0; i < kernel.Height; i++)
            {
                IMatrixData innerCycleCalculationResult = null;

                for (var j = 0; j < kernel.Width; j++)
                {
                    var imageColumn = column + j - 1;
                    var imageRow = row + i - 1;

                    //allows bluring edges of the picture
                    if (imageColumn < 0 || imageRow < 0 || imageColumn >= image.Width || imageRow >= image.Height)
                    {
                        continue;
                    }

                    innerCycleCalculationResult =
                        innerCycleCalculationResult.Add(image.GetValue(imageColumn, imageRow)
                            .MultiplyBy(kernel.GetValue(j, i)));
                }

                endValue = endValue.Add(innerCycleCalculationResult);
            }

            if (Math.Abs(kernelSum - (-1)) > 0.01)
            {
                return endValue.Divide(new FloatNumberMatrixData(kernelSum));
            }
            else
            {
                return endValue.Divide(kernel.Sum);
            }
        }

        /// <summary>
        /// Calculates convoluted image size
        /// </summary>
        /// <param name="kernel">kernel matrix</param>
        /// <returns>array of integers. 0 position is convoluted image width, 1 is convoluted image height</returns>
        private int[] CalculateConvolutedImageDimensions(IMatrix kernel)
        {
            return
                new[]
                {
                    this.Width - kernel.Width - 1,
                    this.Height - kernel.Height + 1
                };
        }
    }
}