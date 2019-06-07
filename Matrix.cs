using System;
using System.Collections.Generic;
using System.Linq;

namespace GaussianFilter
{
    /// <summary>
    /// Matrix object
    /// </summary>
    /// <typeparam name="TCellValue">Type of values containing inside it</typeparam>
    public class Matrix<TCellValue> : IMatrix<TCellValue>
        where TCellValue : struct, IConvertible, IComparable<TCellValue>, IComparable, IEquatable<TCellValue>
    {
        /// <summary>
        /// Supported types as Type object
        /// </summary>
        public static readonly IReadOnlyCollection<Type> SupportedValuesForTCellValue = new[]
        {
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(decimal)
        };

        /// <summary>
        /// Internal matrix structure
        /// </summary>
        private readonly IList<IList<TCellValue>> matrix;

        /// <summary>
        /// Creates matrix object with data from nested lists
        /// </summary>
        /// <param name="matrix"></param>
        /// <exception cref="ArgumentException"></exception>
        public Matrix(IList<IList<TCellValue>> matrix) : this(matrix.Count, matrix[0].Count)
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
            var tCellValueType = typeof(TCellValue);

            if (!SupportedValuesForTCellValue.Contains(tCellValueType))
            {
                throw new ArgumentException(
                    "Invalid cell value. Supported types are: " + string.Join(", ", SupportedTypeValuesNames),
                    nameof(TCellValue));
            }

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
        /// Supported types as string
        /// </summary>
        public static string[] SupportedTypeValuesNames =>
            SupportedValuesForTCellValue.Select(type => type.Name).ToArray();

        /// <summary>
        /// Width of the matrix
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Height of the matrix
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets value from certain position
        /// </summary>
        /// <param name="x">column position</param>
        /// <param name="y">row position</param>
        /// <returns>value c</returns>
        /// <exception cref="ArgumentOutOfRangeException">when x or y are pointing to elements outside of the matrix</exception>
        public TCellValue GetPixelValue(int x, int y)
        {
            if (x >= Width || y >= Height || x < 0 || y < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.matrix[y][x];
        }

        /// <summary>
        /// Sets pixel value
        /// </summary>
        /// <param name="x">column position</param>
        /// <param name="y">row position</param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentOutOfRangeException">when x or y are pointing to elements outside of the matrix</exception>
        public void SetPixelValue(int x, int y, TCellValue value)
        {
            if (x >= Width || y >= Height || x < 0 || y < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.matrix[y][x] = value;
        }
    }
}