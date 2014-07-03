using System;
using Oatc.OpenMI.Sdk.Backbone;
using OpenMI.Standard;

namespace Oatc.OpenMI.Sdk.Spatial
{
    /// <summary>
    /// Default matrix implementation, dense.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class DoubleMatrix : IMatrix<double>
    {
        private double[,] values;

        public DoubleMatrix(int rowCount, int columnCount)
        {
            values = new double[rowCount, columnCount];
        }

        public double this[int rowIndex, int columnIndex]
        {
            get { return values[rowIndex, columnIndex]; }
            set { values[rowIndex, columnIndex] = value; }
        }

        public int RowCount
        {
            get { return values.GetLength(0); }
        }

        public int ColumnCount
        {
            get { return values.GetLength(1); }
        }

        public IScalarSet Product(IScalarSet vector)
        {
            var outputValues = new double[RowCount];

            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    outputValues[i] += this[i, j] * vector.GetScalar(j);
                }
            }

            return new ScalarSet(outputValues);
        }
    }
}