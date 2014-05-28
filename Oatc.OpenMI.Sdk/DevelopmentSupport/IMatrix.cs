using System;
using OpenMI.Standard;

namespace Oatc.OpenMI.Sdk.Spatial
{
    /// <summary>
    /// Base matrix interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMatrix<T> where T : IComparable<T>
    {
        T this[int rowIndex, int columnIndex] { get; set; }
        int RowCount { get; }
        int ColumnCount { get; }

        IScalarSet Product(IScalarSet vector);
    }
}