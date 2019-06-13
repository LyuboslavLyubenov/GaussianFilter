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
        public Matrix(IList<IList<IMatrixData>> matrix)
        {
            if (matrix.Count == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(matrix));
            }


            this.Width = matrix[0].Count;
            this.Height = matrix.Count;
            this.matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
        }

        /// <summary>
        /// Creates empty matrix with given size
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <exception cref="ArgumentOutOfRangeException">When width or height is not positive number</exception>
        public Matrix(int width, int height, Type matrixType)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            if (!matrixType.GetInterfaces().Contains(typeof(IMatrixData)))
            {
                throw new ArgumentException(nameof(matrixType));
            }

            this.Width = width;
            this.Height = height;

            var matrixEmptyObject = (IMatrixData) Activator.CreateInstance(matrixType);
            this.matrix = new List<IList<IMatrixData>>();

            for (int i = 0; i < Height; i++)
            {
                this.matrix.Add(new List<IMatrixData>());

                for (int j = 0; j < Width; j++)
                {
                    this.matrix[i].Add(matrixEmptyObject);
                }
            }
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
    }
}