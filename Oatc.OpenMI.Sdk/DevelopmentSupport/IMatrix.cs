<<<<<<< HEAD
﻿using System;
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
=======
﻿using System;
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
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
}