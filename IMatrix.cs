namespace GaussianFilter
{
    public interface IMatrix<T>
    {
        int Width { get; }
        int Height { get; }

        T GetValue(int column, int row);
        void SetValue(int column, int row, T value);
    }
}