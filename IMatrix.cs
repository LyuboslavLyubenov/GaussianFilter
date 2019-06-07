namespace GaussianFilter
{
    public interface IMatrix<T>
    {
        int Width { get; }
        int Height { get; }

        T GetPixelValue(int x, int y);
        void SetPixelValue(int x, int y, T value);
    }
}