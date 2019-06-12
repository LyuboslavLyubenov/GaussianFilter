using System;
using System.Collections.Generic;
using System.Linq;

namespace GaussianFilter
{
    /// <summary>
    /// Matrix object
    /// </summary>
    public class Matrix : IMatrix
    {
        /// <summary>
        /// Internal matrix structure
        /// </summary>
        private readonly IList<IList<IMatrixData>> matrix;

        /// <summary>
        /// Creates matrix object with data from nested lists
        /// </summary>
        /// <param name="matrix"></param>
        /// <exception cref="ArgumentException"></exception>
        public Matrix(IList<IList<IMatrixData>> matrix) : this(matrix[0].Count, matrix.Count)
        {
            if (matrix.Count == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(matrix));
            }


            this.matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
        }

        /// <summary>
        /// Creates empty matrix with given size
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <exception cref="ArgumentOutOfRangeException">When width or height is not positive number</exception>
        public Matrix(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Width of the matrix
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Height of the matrix
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Calculates sum of the matrix
        /// </summary>
        public IMatrixData Sum => this.matrix.SelectMany(number => number).ToList().Sum();

        /// <summary>
        /// Gets value from certain position
        /// </summary>
        /// <param name="column">column position</param>
        /// <param name="row">row position</param>
        /// <returns>value c</returns>
        /// <exception cref="ArgumentOutOfRangeException">when x or y are pointing to elements outside of the matrix</exception>
        public IMatrixData GetValue(int column, int row)
        {
            if (column >= Height || row >= Width || column < 0 || row < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.matrix[row][column];
        }

        /// <summary>
        /// Sets pixel value
        /// </summary>
        /// <param name="column">column position</param>
        /// <param name="row">row position</param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentOutOfRangeException">when x or y are pointing to elements outside of the matrix</exception>
        public void SetValue(int column, int row, IMatrixData value)
        {
            if (column >= Height || row >= Width || column < 0 || row < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.matrix[row][column] = value;
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
            var resultMatrix = new Matrix(resultMatrixSize[0], resultMatrixSize[1]);

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