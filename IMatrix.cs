namespace GaussianFilter
{
    public interface IMatrix
    {
        int Width { get; }
        int Height { get; }

        IMatrixData Sum { get; }

        IMatrixData GetValue(int column, int row);

        void SetValue(int column, int row, IMatrixData value);

        IMatrix Convolute(IMatrix kernel);
    }
}