using System;
using System.Collections.Generic;
using Oatc.OpenMI.Sdk.Backbone;
using OpenMI.Standard;

namespace Oatc.OpenMI.Sdk.Spatial
{
    /// <summary>
    /// Sparse matrix.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class DoubleSparseMatrix : IMatrix<double>
    {
        [Serializable]
        public struct Index
        {
            public Index(int row, int column)
            {
                Row = row;
                Column = column;
            }

            public int Row;
            
            public int Column;
        }

        public readonly Dictionary<Index, double> Values;

        public int RowCount { get; private set; }
        public int ColumnCount { get; private set; }

        public DoubleSparseMatrix(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;

            Values = new Dictionary<Index, double>();
        }

        public DoubleSparseMatrix(int rowCount, int columnCount, double[,] values)
            : this(rowCount, columnCount)
        {
            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < columnCount; j++)
                {
                    if(values[i, j] != 0)
                    {
                        this[i, j] = values[i, j];
                    }
                }
            }
        }

        public bool IsCellEmpty(int row, int column)
        {
            var index = new Index(row, column);
            return !Values.ContainsKey(index);
        }

        public double this[int row, int column]
        {
            get
            {
                var index = new Index(row, column);
                double result;
                Values.TryGetValue(index, out result);
                return result;
            }
            set
            {
                var index = new Index(row, column);
                
                if (value.Equals(0))
                {
                    if(Values.ContainsKey(index))
                    {
                        Values.Remove(index);
                    }

                    return;
                }

                Values[index] = value;
            }
        }

        public IScalarSet Product(IScalarSet vector)
        {
            var outputValues = new double[RowCount];

            foreach (var entry in Values)
            {
                outputValues[entry.Key.Row] += entry.Value * vector.GetScalar(entry.Key.Column);
            }

            return new ScalarSet(outputValues);
        }
    }
}