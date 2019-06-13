using System;

namespace GaussianFilter
{
    public class FloatNumberMatrixData : IMatrixData
    {
        private float internalValue;

        public FloatNumberMatrixData()
        {
        }

        public FloatNumberMatrixData(float internalValue)
        {
            this.internalValue = internalValue;
        }

        public object RawValue => this.internalValue;

        public IMatrixData ZeroRepresentation => new FloatNumberMatrixData(0);

        public IMatrixData MultiplyBy(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                this.internalValue *= (float) floatNumberData.internalValue;
            }

            throw new NotImplementedException();
        }

        public IMatrixData Add(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                this.internalValue += (float) floatNumberData.internalValue;
            }

            throw new System.NotImplementedException();
        }

        public IMatrixData Divide(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                this.internalValue *= (float) floatNumberData.internalValue;
            }

            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return this.internalValue.ToString();
        }
    }
}