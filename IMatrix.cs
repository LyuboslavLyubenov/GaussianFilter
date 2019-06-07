namespace GaussianFilter
{
    public interface IMatrix
    {
        int Width { get; }
        int Height { get; }

        float GetValue(int column, int row);
        void SetValue(int column, int row, float value);
    }
}