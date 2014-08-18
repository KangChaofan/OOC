<<<<<<< HEAD
#region Copyright

/*
* Copyright (c) 2005,2006,2007, OpenMI Association
* All rights reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions are met:
*     * Redistributions of source code must retain the above copyright
*       notice, this list of conditions and the following disclaimer.
*     * Redistributions in binary form must reproduce the above copyright
*       notice, this list of conditions and the following disclaimer in the
*       documentation and/or other materials provided with the distribution.
*     * Neither the name of the OpenMI Association nor the
*       names of its contributors may be used to endorse or promote products
*       derived from this software without specific prior written permission.
*
* THIS SOFTWARE IS PROVIDED BY "OpenMI Association" ``AS IS'' AND ANY
* EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
* DISCLAIMED. IN NO EVENT SHALL "OpenMI Association" BE LIABLE FOR ANY
* DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
* SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Oatc.OpenMI.Sdk.Backbone;
using OpenMI.Standard;

namespace Oatc.OpenMI.Sdk.Spatial
{
    /// <summary>
    /// The ElementMapper converts one ValueSet (inputValues) associated one ElementSet (fromElements)
    /// to a new ValuesSet (return value of MapValue) that corresponds to another ElementSet 
    /// (toElements). The conversion is a two step procedure where the first step (Initialize) is 
    /// executed at initialisation time only, whereas the MapValues is executed during time stepping.
    /// 
    /// <p>The Initialize method will create a conversion matrix with the same number of rows as the
    /// number of elements in the ElementSet associated to the accepting component (i.e. the toElements) 
    /// and the same number of columns as the number of elements in the ElementSet associated to the 
    /// providing component (i.e. the fromElements).</p>
    /// 
    /// <p>Mapping is possible for any zero-, one- and two-dimensional elemets. Zero dimensional 
    /// elements will always be points, one-dimensional elements will allways be polylines and two-
    /// dimensional elements will allways be polygons.</p>
    /// 
    /// <p>The ElementMapper contains a number of methods for mapping between the different element types.
    /// As an example polyline to polygon mapping may be done either as Weighted Mean or as Weighted Sum.
    /// Typically the method choice will depend on the quantity mapped. Such that state variables such as 
    /// water level will be mapped using Weighted Mean whereas flux variables such as seepage from river 
    /// to groundwater will be mapped using Weighted Sum. The list of available methods for a given 
    /// combination of from and to element types is obtained using the GetAvailableMethods method.</p>
    /// </summary>
    public class ElementMapper
    {
        private static readonly IList<ElementMappingMethod> methods;
        
        private ElementMappingMethod currentMethod;

        private bool isInitialised;

        private IMatrix<double> mappingMatrix;

        // these 2 fields are added for debugging purposes
        private IMatrix<double> intersectedLengthMatrix;

        private int rowsCount;
        private int columnsCount;

        static ElementMapper()
        {
            methods = new List<ElementMappingMethod>();

            AddNewMethod(ElementType.XYPoint, ElementType.XYPoint, "Nearest", (int)DefaultMethodType.PointToPoint.Nearest);
            AddNewMethod(ElementType.XYPoint, ElementType.XYPoint, "Inverse", (int)DefaultMethodType.PointToPoint.Inverse);
            AddNewMethod(ElementType.XYPoint, ElementType.XYPolyLine, "Nearest", (int)DefaultMethodType.PointToPolyline.Nearest);
            AddNewMethod(ElementType.XYPoint, ElementType.XYPolyLine, "Inverse", (int)DefaultMethodType.PointToPolyline.Inverse);
            AddNewMethod(ElementType.XYPoint, ElementType.XYPolygon, "Mean", (int)DefaultMethodType.PointToPolygon.Mean);
            AddNewMethod(ElementType.XYPoint, ElementType.XYPolygon, "Sum", (int)DefaultMethodType.PointToPolygon.Sum);
            AddNewMethod(ElementType.XYPolyLine, ElementType.XYPoint, "Nearest", (int)DefaultMethodType.PolylineToPoint.Nearest);
            AddNewMethod(ElementType.XYPolyLine, ElementType.XYPoint, "Inverse", (int)DefaultMethodType.PolylineToPoint.Inverse);
            AddNewMethod(ElementType.XYPolyLine, ElementType.XYPolygon, "Weighted Mean", (int)DefaultMethodType.PolylineToPolygon.WeightedMean);
            AddNewMethod(ElementType.XYPolyLine, ElementType.XYPolygon, "Weighted Sum", (int)DefaultMethodType.PolylineToPolygon.WeightedSum);
            AddNewMethod(ElementType.XYPolygon, ElementType.XYPoint, "Value", (int)DefaultMethodType.PolygonToPoint.Value);
            AddNewMethod(ElementType.XYPolygon, ElementType.XYPolyLine, "Weighted Mean", (int)DefaultMethodType.PolygonToPolyline.WeightedMean);
            AddNewMethod(ElementType.XYPolygon, ElementType.XYPolyLine, "Weighted Sum", (int)DefaultMethodType.PolygonToPolyline.WeightedSum);
            AddNewMethod(ElementType.XYPolygon, ElementType.XYPolygon, "Weighted Mean", (int)DefaultMethodType.PolygonToPolygon.WeightedMean);
            AddNewMethod(ElementType.XYPolygon, ElementType.XYPolygon, "Weighted Sum", (int)DefaultMethodType.PolygonToPolygon.WeightedSum);
            AddNewMethod(ElementType.XYPoint, ElementType.XYLine, "Nearest", (int)DefaultMethodType.PointToLine.Nearest);
            AddNewMethod(ElementType.XYPoint, ElementType.XYLine, "Inverse", (int)DefaultMethodType.PointToLine.Inverse);
            AddNewMethod(ElementType.XYLine, ElementType.XYPoint, "Nearest", (int)DefaultMethodType.LineToPoint.Nearest);
            AddNewMethod(ElementType.XYLine, ElementType.XYPoint, "Inverse", (int)DefaultMethodType.LineToPoint.Inverse);
            AddNewMethod(ElementType.XYLine, ElementType.XYPolygon, "Weighted Mean", (int)DefaultMethodType.LineToPolygon.WeightedMean);
            AddNewMethod(ElementType.XYLine, ElementType.XYPolygon, "Weighted Sum", (int)DefaultMethodType.LineToPolygon.WeightedSum);
            AddNewMethod(ElementType.XYPolygon, ElementType.XYLine, "Weighted Mean", (int)DefaultMethodType.PolygonToLine.WeightedMean);
            AddNewMethod(ElementType.XYPolygon, ElementType.XYLine, "Weighted Sum", (int)DefaultMethodType.PolygonToLine.WeightedSum);
            AddNewMethod(ElementType.XYPolyLine, ElementType.XYPolyLine, "Value", (int)DefaultMethodType.PolylineToPolyline.Value); // TODO: do we need this one? remove it.
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ElementMapper()
        {
            isInitialised = false;
        }

        public static void AddNewMethod(ElementType from, ElementType to, string name, int id)
        {
            methods.Add(new ElementMappingMethod
                (id, name, from, to));
        }

        public virtual IMatrix<double> MappingMatrix
        {
            get { return mappingMatrix; } 
            set { mappingMatrix = value; }
        }

        public virtual IMatrix<double> IntersectedLengthMatrix
        {
            get { return intersectedLengthMatrix; }
            set { intersectedLengthMatrix = value; }
        }

        public virtual int RowsCount
        {
            get { return rowsCount; }
            set { rowsCount = value; }
        }

        public virtual int ColumnsCount
        {
            get { return columnsCount; }
            set { columnsCount = value; }
        }

        public virtual bool IsInitialised { get { return isInitialised; } }

        public virtual ElementMappingMethod CurrentMethod
        {
            get { return currentMethod; }
            set { currentMethod = value; }
        }

        public virtual IList<ElementMappingMethod> Methods { get { return methods; } }

        public static bool SupportsCaching { get; set; }

        public static string CacheFileDirectory { get; set; }

        /// <summary>
        /// Initialises the ElementMapper. The initialisation includes setting the isInitialised
        /// flag and calls UpdateMappingMatrix for claculation of the mapping matrix.
        /// </summary>
        ///
        /// <param name="methodDescription">String description of mapping method</param> 
        /// <param name="fromElements">The IElementSet to map from.</param>
        /// <param name="toElements">The IElementSet to map to</param>
        /// 
        /// <returns>
        /// The method has no return value.
        /// </returns>
        public virtual void Initialise(string methodDescription, IElementSet fromElements, IElementSet toElements)
        {
            this.fromElements = fromElements;
            this.toElements = toElements;

            RowsCount = toElements.ElementCount;
            ColumnsCount = fromElements.ElementCount;

            if (SupportsCaching && CacheFileDirectory != null && LoadMappingMatrixFromCache(methodDescription, fromElements, toElements))
            {
                isInitialised = true;
                return; // no need to calculate mapping matrix
            }

            fromXYPolygons.Clear();
            toXYPolygons.Clear();

            CreateGeometries();

            UpdateMappingMatrix(methodDescription, fromElements, toElements);

            if (SupportsCaching && CacheFileDirectory != null)
            {
                SaveMappingMatrixToCache(methodDescription, fromElements, toElements);
            }

            isInitialised = true;
        }

        private bool LoadMappingMatrixFromCache(string method, IElementSet fromElementSet, IElementSet toElementSet)
        {
            var cacheFilePath = GetCacheFilePath(method, fromElementSet, toElementSet);

            FileStream file = null;
            try
            {
                if (File.Exists(cacheFilePath))
                {
                    file = File.Open(cacheFilePath, FileMode.Open);

                    var bf = new BinaryFormatter();

                    var cachedMethod = bf.Deserialize(file) as string;
                    var cachedFromElementSetID = bf.Deserialize(file) as string;
                    var cachedFromElementSetElementCount = bf.Deserialize(file) as int[];
                    var cachedToElementSetID = bf.Deserialize(file) as string;
                    var cachedToElementSetElementCount = bf.Deserialize(file) as int[];

                    if (cachedMethod != method
                        || !fromElementSet.ID.Equals(cachedFromElementSetID)
                        || !fromElementSet.ElementCount.Equals(cachedFromElementSetElementCount[0])
                        || !toElementSet.ID.Equals(cachedToElementSetID)
                        || !toElementSet.ElementCount.Equals(cachedToElementSetElementCount[0]))
                    {
                        return false;
                    }

                    MappingMatrix = bf.Deserialize(file) as IMatrix<double>;

                    // when file format changes - no exception is thrown on the above line so we have to make additional check here
                    if(MappingMatrix == null || (MappingMatrix is DoubleSparseMatrix && ((DoubleSparseMatrix)MappingMatrix).Values == null))
                    {
                        throw new IOException("File format is incorrect");
                    }

                    file.Close();

                    return true;
                }
            }
            catch(Exception e)
            {
                if(file != null)
                {
                    file.Close();
                }
                if(File.Exists(cacheFilePath))
                {
                    File.Delete(cacheFilePath);
                }
                return false;
            }


            return false;
        }

        private void SaveMappingMatrixToCache(string method, IElementSet fromElementSet, IElementSet toElementSet)
        {
            var cacheFilePath = GetCacheFilePath(method, fromElementSet, toElementSet);
            var file = File.Open(cacheFilePath, FileMode.Create);
            var bf = new BinaryFormatter();
            bf.Serialize(file, method);
            bf.Serialize(file, fromElementSet.ID);
            bf.Serialize(file, new [] {fromElementSet.ElementCount});
            bf.Serialize(file, toElementSet.ID);
            bf.Serialize(file, new [] {toElementSet.ElementCount});
            bf.Serialize(file, MappingMatrix);
            file.Close();
        }

        public static string GetCacheFilePath(string method, IElementSet fromElementSet, IElementSet toElementSet)
        {
            var cacheFileName = string.Format("ElementMapperCache-{0}-{1}.{2}-{3}.{4}", method, fromElementSet.ID,
                                              fromElementSet.ElementCount, toElementSet.ID, toElementSet.ElementCount);
            return Path.Combine(CacheFileDirectory, cacheFileName);
        }

        protected IList<XYPolygon> fromXYPolygons = new List<XYPolygon>();
        protected IList<XYPolygon> toXYPolygons = new List<XYPolygon>();

        private void CreateGeometries()
        {
            if(fromElements.ElementType == ElementType.XYPolygon)
            {
                for (int i = 0; i < fromElements.ElementCount; i++)
                {
                    fromXYPolygons.Add(CreateFromXYPolygon(fromElements, i));
                }
            }

            if(toElements.ElementType == ElementType.XYPolygon)
            {
                for (int i = 0; i < toElements.ElementCount; i++)
                {
                    toXYPolygons.Add(CreateFromXYPolygon(toElements, i));
                }
            }
        }

        private IElementSet fromElements;
        private IElementSet toElements;

        /// <summary>
        /// MapValues calculates a IValueSet through multiplication of an inputValues IValueSet
        /// vector or matrix (ScalarSet or VectorSet) on to the mapping maprix. IScalarSets maps
        /// to IScalarSets and IVectorSets maps to IVectorSets.
        /// </summary>
        /// 
        /// <remarks>
        /// Mapvalues is called every time a georeferenced link is evaluated.
        /// </remarks>
        /// 
        /// <param name="inputValues">IValueSet of values to be mapped.</param>
        /// 
        /// <returns>
        /// A IValueSet found by mapping of the inputValues on to the toElementSet.
        /// </returns>
        public virtual IValueSet MapValues(IValueSet inputValues)
        {
            if (!isInitialised)
            {
                throw new Exception(
                    "ElementMapper objects needs to be initialised before the MapValue method can be used");
            }

            if (!inputValues.Count.Equals(ColumnsCount))
            {
                throw new Exception("Dimension mismatch between inputValues and mapping matrix");
            }
            
            if (inputValues is IScalarSet)
            {
                return MappingMatrix.Product((IScalarSet)inputValues);
            }
            else if (inputValues is IVectorSet)
            {
                Vector[] outValues = new Vector[RowsCount];
                //--- Multiply the Values vector with the MappingMatrix ---
                for (int i = 0; i < RowsCount; i++)
                {
                    for (int n = 0; n < ColumnsCount; n++)
                    {
                        outValues[i] = new Vector(); // Need this as Initialisation only valid for value types!

                        outValues[i].XComponent += MappingMatrix[i, n]*
                                                   ((IVectorSet) inputValues).GetVector(n).XComponent;
                        outValues[i].YComponent += MappingMatrix[i, n]*
                                                   ((IVectorSet) inputValues).GetVector(n).YComponent;
                        outValues[i].ZComponent += MappingMatrix[i, n]*
                                                   ((IVectorSet) inputValues).GetVector(n).ZComponent;
                    }
                }
                VectorSet outputValues = new VectorSet(outValues);
                return outputValues;
            }
            else
            {
                throw new Exception("Invalid datatype used for inputValues parameter. MapValues failed");
            }
        }

        /// <summary>
        /// Calculates the mapping matrix between fromElements and toElements. The mapping method 
        /// is decided from the combination of methodDescription, fromElements.ElementType and 
        /// toElements.ElementType. 
        /// The valid values for methodDescription is obtained through use of the 
        /// GetAvailableMethods method.
        /// </summary>
        /// 
        /// <remarks>
        /// UpdateMappingMatrix is called during initialisation. UpdateMappingMatrix must be called prior
        /// to Mapvalues.
        /// </remarks>
        /// 
        /// <param name="methodDescription">String description of mapping method</param> 
        /// <param name="fromElements">The IElementset to map from.</param>
        /// <param name="toElements">The IElementset to map to</param>
        ///
        /// <returns>
        /// The method has no return value.
        /// </returns>
        public virtual void UpdateMappingMatrix(string methodDescription, IElementSet fromElements,
                                                IElementSet toElements)
        {
            var useSparseMatrix = true;
            UpdateMappingMatrix(methodDescription, fromElements, toElements, useSparseMatrix);

            // check ratio of non-zero elements
            var nonZeroCellCount = 0;
            for (var row = 0; row < mappingMatrix.RowCount; row++)
            {
                for (var column = 0; column < mappingMatrix.ColumnCount; column++)
                {
                    if(mappingMatrix[row, column] != 0)
                    {
                        nonZeroCellCount++;
                    }
                }
            }

            var totalCellCount = fromElements.ElementCount * toElements.ElementCount;
            if (nonZeroCellCount/(double) totalCellCount <= 0.5) // TODO: expose as a user argument
            {
                return;
            }

            // switch to dense matrix
            var newMappingMatrix = new DoubleMatrix(RowsCount, ColumnsCount);
            for (var row = 0; row < mappingMatrix.RowCount; row++)
            {
                for (var column = 0; column < mappingMatrix.ColumnCount; column++)
                {
                    if (mappingMatrix[row, column] != 0)
                    {
                        newMappingMatrix[row, column] = mappingMatrix[row, column];
                    }
                }
            }

            mappingMatrix = newMappingMatrix;
        }

        public virtual void UpdateMappingMatrix(string methodDescription, IElementSet fromElements,
                                                IElementSet toElements, bool useSparseMatrix)
        {
            try
            {
                ElementSetChecker.CheckElementSet(fromElements);
                ElementSetChecker.CheckElementSet(toElements);

                CurrentMethod = GetMethod(methodDescription, fromElements.ElementType, toElements.ElementType);

                if(useSparseMatrix)
                {
                    MappingMatrix = new DoubleSparseMatrix(RowsCount, ColumnsCount);
                    intersectedLengthMatrix = new DoubleSparseMatrix(RowsCount, ColumnsCount);
                }
                else
                {
                    MappingMatrix = new DoubleMatrix(RowsCount, ColumnsCount);
                    intersectedLengthMatrix = new DoubleMatrix(RowsCount, ColumnsCount);
                }

                

                if (fromElements.ElementType == ElementType.XYPoint && toElements.ElementType == ElementType.XYPoint)
                    // Point to Point
                {
                    #region

                    try
                    {
                        for (int i = 0; i < RowsCount; i++)
                        {
                            XYPoint ToPoint = CreateXYPoint(toElements, i);
                            for (int j = 0; j < ColumnsCount; j++)
                            {
                                XYPoint FromPoint = CreateXYPoint(fromElements, j);
                                MappingMatrix[i, j] = XYGeometryTools.CalculatePointToPointDistance(ToPoint, FromPoint);
                            }
                        }

                        if (CurrentMethod.ID.Equals((int) DefaultMethodType.PointToPoint.Nearest))
                        {
                            for (int i = 0; i < RowsCount; i++)
                            {
                                double MinDist = MappingMatrix[i, 0];
                                for (int j = 1; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] < MinDist)
                                    {
                                        MinDist = MappingMatrix[i, j];
                                    }
                                }
                                int Denominator = 0;
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] == MinDist)
                                    {
                                        MappingMatrix[i, j] = 1;
                                        Denominator++;
                                    }
                                    else
                                    {
                                        MappingMatrix[i, j] = 0;
                                    }
                                }
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    MappingMatrix[i, j] = MappingMatrix[i, j]/Denominator;
                                }
                            }
                        } // if (currentMethodId.Equals((int) DefaultMethodType.PointToPoint.Nearest))   
                        else if (CurrentMethod.ID.Equals((int) DefaultMethodType.PointToPoint.Inverse))
                        {
                            for (int i = 0; i < RowsCount; i++)
                            {
                                double MinDist = MappingMatrix[i, 0];
                                for (int j = 1; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] < MinDist)
                                    {
                                        MinDist = MappingMatrix[i, j];
                                    }
                                }
                                if (MinDist == 0)
                                {
                                    int Denominator = 0;
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        if (MappingMatrix[i, j] == MinDist)
                                        {
                                            MappingMatrix[i, j] = 1;
                                            Denominator++;
                                        }
                                        else
                                        {
                                            MappingMatrix[i, j] = 0;
                                        }
                                    }
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = MappingMatrix[i, j]/Denominator;
                                    }
                                }
                                else
                                {
                                    double Denominator = 0;
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = 1/MappingMatrix[i, j];
                                        Denominator = Denominator + MappingMatrix[i, j];
                                    }
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = MappingMatrix[i, j]/Denominator;
                                    }
                                }
                            }
                        } // else if (currentMethodId.Equals((int) DefaultMethodType.PointToPoint.Inverse))
                        else
                        {
                            throw new Exception("methodDescription unknown for point point mapping");
                        }
                        // else if (currentMethodId.Equals((int) DefaultMethodType.PointToPoint.Nearest)) and else if (currentMethodId.Equals((int) DefaultMethodType.PointToPoint.Inverse))
                    }
                    catch (Exception e) // Catch for all of the Point to Point part
                    {
                        throw new Exception("Point to point mapping failed", e);
                    }

                    #endregion
                }
                else if (fromElements.ElementType == ElementType.XYPoint &&
                         ((toElements.ElementType == ElementType.XYPolyLine) ||
                          (toElements.ElementType == ElementType.XYLine)))
                    // Point to PolyLine/Line
                {
                    #region

                    try
                    {
                        for (int i = 0; i < RowsCount; i++)
                        {
                            XYPolyline toPolyLine = CreateXYPolyline(toElements, i);
                            for (int j = 0; j < ColumnsCount; j++)
                            {
                                XYPoint fromPoint = CreateXYPoint(fromElements, j);
                                MappingMatrix[i, j] = XYGeometryTools.CalculatePolylineToPointDistance(toPolyLine,
                                                                                                       fromPoint);
                            }
                        }

                        if (CurrentMethod.ID.Equals((int) DefaultMethodType.PointToPolyline.Nearest) ||
                            CurrentMethod.ID.Equals((int) DefaultMethodType.PointToLine.Nearest))
                        {
                            for (int i = 0; i < RowsCount; i++)
                            {
                                double MinDist = MappingMatrix[i, 0];
                                for (int j = 1; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] < MinDist)
                                    {
                                        MinDist = MappingMatrix[i, j];
                                    }
                                }
                                int denominator = 0;
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] == MinDist)
                                    {
                                        MappingMatrix[i, j] = 1;
                                        denominator++;
                                    }
                                    else
                                    {
                                        MappingMatrix[i, j] = 0;
                                    }
                                }
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    MappingMatrix[i, j] = MappingMatrix[i, j]/denominator;
                                }
                            }
                        } // if (currentMethodId.Equals((int) DefaultMethodType.PointToPolyline.Nearest))
                        else if ((CurrentMethod.ID.Equals((int) DefaultMethodType.PointToPolyline.Inverse)) ||
                                 (CurrentMethod.ID.Equals((int) DefaultMethodType.PointToLine.Inverse)))
                        {
                            for (int i = 0; i < RowsCount; i++)
                            {
                                double minDist = MappingMatrix[i, 0];
                                for (int j = 1; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] < minDist)
                                    {
                                        minDist = MappingMatrix[i, j];
                                    }
                                }
                                if (minDist == 0)
                                {
                                    int denominator = 0;
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        if (MappingMatrix[i, j] == minDist)
                                        {
                                            MappingMatrix[i, j] = 1;
                                            denominator++;
                                        }
                                        else
                                        {
                                            MappingMatrix[i, j] = 0;
                                        }
                                    }
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = MappingMatrix[i, j]/denominator;
                                    }
                                }
                                else
                                {
                                    double denominator = 0;
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = 1/MappingMatrix[i, j];
                                        denominator = denominator + MappingMatrix[i, j];
                                    }
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = MappingMatrix[i, j]/denominator;
                                    }
                                }
                            }
                        } // else if (currentMethodId.Equals((int) DefaultMethodType.PointToPolyline.Inverse))
                        else // if currentMethodId != Nearest and Inverse
                        {
                            throw new Exception("methodDescription unknown for point to polyline mapping");
                        }
                    }
                    catch (Exception e) // Catch for all of the Point to Polyline part 
                    {
                        throw new Exception("Point to polyline mapping failed", e);
                    }

                    #endregion
                }
                else if (fromElements.ElementType == ElementType.XYPoint &&
                         toElements.ElementType == ElementType.XYPolygon)
                    // Point to Polygon
                {
                    #region

                    try
                    {
                        XYPolygon polygon;
                        XYPoint point;
                        int count;
                        for (int i = 0; i < RowsCount; i++)
                        {
                            polygon = toXYPolygons[i];
                            count = 0;
                            for (int n = 0; n < ColumnsCount; n++)
                            {
                                point = CreateXYPoint(fromElements, n);
                                if (XYGeometryTools.IsPointInPolygon(point, polygon))
                                {
                                    if (CurrentMethod.ID.Equals((int) DefaultMethodType.PointToPolygon.Mean))
                                    {
                                        count = count + 1;
                                    }
                                    else if (CurrentMethod.ID.Equals((int) DefaultMethodType.PointToPolygon.Sum))
                                    {
                                        count = 1;
                                    }
                                    else
                                    {
                                        throw new Exception(
                                            "methodDescription unknown for point to polygon mapping");
                                    }
                                }
                            }
                            for (int n = 0; n < ColumnsCount; n++)
                            {
                                point = CreateXYPoint(fromElements, n);

                                if (XYGeometryTools.IsPointInPolygon(point, polygon))
                                {
                                    MappingMatrix[i, n] = 1.0/count;
                                }
                            }
                        }
                    }
                    catch (Exception e) // Catch for all of the Point to Polyline part 
                    {
                        throw new Exception("Point to polygon mapping failed", e);
                    }

                    #endregion
                }
                else if (((fromElements.ElementType == ElementType.XYPolyLine) ||
                          (fromElements.ElementType == ElementType.XYLine)) &&
                         toElements.ElementType == ElementType.XYPoint)
                    // Polyline/Line to Point
                {
                    #region

                    try
                    {
                        for (int i = 0; i < RowsCount; i++)
                        {
                            XYPoint toPoint = CreateXYPoint(toElements, i);
                            for (int j = 0; j < ColumnsCount; j++)
                            {
                                XYPolyline fromPolyLine = CreateXYPolyline(fromElements, j);
                                MappingMatrix[i, j] =
                                    XYGeometryTools.CalculatePolylineToPointDistance(fromPolyLine, toPoint);
                            }
                        }

                        if (CurrentMethod.ID.Equals((int) DefaultMethodType.PolylineToPoint.Nearest) ||
                            CurrentMethod.ID.Equals((int) DefaultMethodType.LineToPoint.Nearest))
                        {
                            for (int i = 0; i < RowsCount; i++)
                            {
                                double minDist = MappingMatrix[i, 0];
                                for (int j = 1; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] < minDist)
                                    {
                                        minDist = MappingMatrix[i, j];
                                    }
                                }
                                int denominator = 0;
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] == minDist)
                                    {
                                        MappingMatrix[i, j] = 1;
                                        denominator++;
                                    }
                                    else
                                    {
                                        MappingMatrix[i, j] = 0;
                                    }
                                }
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    MappingMatrix[i, j] = MappingMatrix[i, j]/denominator;
                                }
                            }
                        } // if (currentMethodId.Equals((int) DefaultMethodType.PolylineToPoint.Nearest))
                        else if (CurrentMethod.ID.Equals((int) DefaultMethodType.PolylineToPoint.Inverse) ||
                                 CurrentMethod.ID.Equals((int) DefaultMethodType.LineToPoint.Inverse))
                        {
                            for (int i = 0; i < RowsCount; i++)
                            {
                                double minDist = MappingMatrix[i, 0];
                                for (int j = 1; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] < minDist)
                                    {
                                        minDist = MappingMatrix[i, j];
                                    }
                                }
                                if (minDist == 0)
                                {
                                    int denominator = 0;
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        if (MappingMatrix[i, j] == minDist)
                                        {
                                            MappingMatrix[i, j] = 1;
                                            denominator++;
                                        }
                                        else
                                        {
                                            MappingMatrix[i, j] = 0;
                                        }
                                    }
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = MappingMatrix[i, j]/denominator;
                                    }
                                }
                                else
                                {
                                    double denominator = 0;
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = 1/MappingMatrix[i, j];
                                        denominator = denominator + MappingMatrix[i, j];
                                    }
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = MappingMatrix[i, j]/denominator;
                                    }
                                }
                            }
                        } // if (currentMethodId.Equals((int) DefaultMethodType.PolylineToPoint.Inverse))
                        else // MethodID != Nearest and Inverse
                        {
                            throw new Exception(
                                "methodDescription unknown for polyline to point mapping");
                        }
                    }
                    catch (Exception e) // Catch for all of the Point to Polyline part 
                    {
                        throw new Exception("Polyline to point mapping failed", e);
                    }

                    #endregion
                }
                else if (((fromElements.ElementType == ElementType.XYPolyLine) ||
                          (fromElements.ElementType == ElementType.XYLine)) &&
                         toElements.ElementType == ElementType.XYPolygon)
                    // PolyLine to Polygon
                {
                    #region

                    try
                    {
                        for (int i = 0; i < RowsCount; i++)
                        {
                            XYPolygon polygon = toXYPolygons[i];

                            if (
                                CurrentMethod.ID.Equals(
                                    (int) DefaultMethodType.PolylineToPolygon.WeightedMean) ||
                                CurrentMethod.ID.Equals((int) DefaultMethodType.LineToPolygon.WeightedMean))
                            {
                                double totalLineLengthInPolygon = 0;
                                for (int n = 0; n < ColumnsCount; n++)
                                {
                                    XYPolyline polyline = CreateXYPolyline(fromElements, n);
                                    MappingMatrix[i, n] =
                                        XYGeometryTools.CalculateLengthOfPolylineInsidePolygon(
                                            polyline, polygon);

                                    IntersectedLengthMatrix[i, n] = MappingMatrix[i, n];

                                    totalLineLengthInPolygon += MappingMatrix[i, n];
                                }
                                if (totalLineLengthInPolygon > 0)
                                {
                                    for (int n = 0; n < ColumnsCount; n++)
                                    {
                                        MappingMatrix[i, n] = MappingMatrix[i, n]/
                                                              totalLineLengthInPolygon;
                                    }
                                }
                            }
                                // if (currentMethodId.Equals((int) DefaultMethodType.PolylineToPolygon.WeightedMean))
                            else if (
                                CurrentMethod.ID.Equals(
                                    (int) DefaultMethodType.PolylineToPolygon.WeightedSum) ||
                                CurrentMethod.ID.Equals(
                                    (int) DefaultMethodType.LineToPolygon.WeightedSum))
                            {
                                for (int n = 0; n < ColumnsCount; n++)
                                {
                                    XYPolyline polyline = CreateXYPolyline(fromElements, n);
                                    MappingMatrix[i, n] =
                                        XYGeometryTools.CalculateLengthOfPolylineInsidePolygon(
                                            polyline, polygon)/polyline.GetLength();

                                    IntersectedLengthMatrix[i, n] = MappingMatrix[i, n];
                                } // for (int n = 0; n < ColumnsCount; n++)
                            }
                                // else if (currentMethodId.Equals((int) DefaultMethodType.PolylineToPolygon.WeightedSum))
                            else // if MethodID != WeightedMean and WeigthedSum
                            {
                                throw new Exception(
                                    "methodDescription unknown for polyline to polygon mapping");
                            }
                        } // for (int i = 0; i < RowsCount; i++)
                    }
                    catch (Exception e) // Catch for all of polyLine to polygon
                    {
                        throw new Exception("Polyline to polygon mapping failed", e);
                    }

                    #endregion
                }
                else if (fromElements.ElementType == ElementType.XYPolygon &&
                         toElements.ElementType == ElementType.XYPoint)
                    // Polygon to Point
                {
                    #region

                    try
                    {
                        for (int n = 0; n < RowsCount; n++)
                        {
                            XYPoint point = CreateXYPoint(toElements, n);
                            for (int i = 0; i < ColumnsCount; i++)
                            {
                                XYPolygon polygon = fromXYPolygons[i];
                                if (XYGeometryTools.IsPointInPolygon(point, polygon))
                                {
                                    if (
                                        CurrentMethod.ID.Equals(
                                            (int) DefaultMethodType.PolygonToPoint.Value))
                                    {
                                        MappingMatrix[n, i] = 1.0;
                                    }
                                    else // if currentMethodId != Value
                                    {
                                        throw new Exception(
                                            "methodDescription unknown for polygon to point mapping");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e) // catch for all of Polygon to Point
                    {
                        throw new Exception("Polygon to point mapping failed", e);
                    }

                    #endregion
                }
                else if (fromElements.ElementType == ElementType.XYPolygon &&
                         ((toElements.ElementType == ElementType.XYPolyLine) ||
                          (toElements.ElementType == ElementType.XYLine)))
                    // Polygon to PolyLine
                {
                    #region

                    try
                    {
                        for (int i = 0; i < RowsCount; i++)
                        {
                            XYPolyline polyline = CreateXYPolyline(toElements, i);
                            if (
                                CurrentMethod.ID.Equals(
                                    (int) DefaultMethodType.PolygonToPolyline.WeightedMean) ||
                                CurrentMethod.ID.Equals(
                                    (int) DefaultMethodType.PolygonToLine.WeightedMean))
                            {
                                for (int n = 0; n < ColumnsCount; n++)
                                {
                                    XYPolygon polygon = fromXYPolygons[n];
                                    var intersectedLength = XYGeometryTools.CalculateLengthOfPolylineInsidePolygon(polyline, polygon);
                                    MappingMatrix[i, n] = intersectedLength/polyline.GetLength();

                                    IntersectedLengthMatrix[i, n] = intersectedLength;
                                }
                                double sum = 0;
                                for (int n = 0; n < ColumnsCount; n++)
                                {
                                    sum += MappingMatrix[i, n];
                                }
                                for (int n = 0; n < ColumnsCount; n++)
                                {
                                    MappingMatrix[i, n] = MappingMatrix[i, n]/sum;
                                }
                            }
                                // if (currentMethodId.Equals((int) DefaultMethodType.PolygonToPolyline.WeightedMean))
                            else if (
                                CurrentMethod.ID.Equals((int) DefaultMethodType.PolygonToPolyline.WeightedSum) ||
                                CurrentMethod.ID.Equals((int) DefaultMethodType.PolygonToLine.WeightedSum))
                            {
                                for (int n = 0; n < ColumnsCount; n++)
                                {
                                    XYPolygon polygon = fromXYPolygons[n];
                                    var intersectedLength = XYGeometryTools.CalculateLengthOfPolylineInsidePolygon(polyline, polygon);
                                    MappingMatrix[i, n] = intersectedLength/polyline.GetLength();
                                    IntersectedLengthMatrix[i, n] = intersectedLength;
                                }
                            }
                                // else if (currentMethodId.Equals((int) DefaultMethodType.PolygonToPolyline.WeightedSum))
                            else // currentMethodId != WeightedMean and WeightedSum
                            {
                                throw new Exception(
                                    "methodDescription unknown for polygon to polyline mapping");
                            }
                        }
                    }
                    catch (Exception e) // catch for all of Polygon to PolyLine
                    {
                        throw new Exception("Polygon to polyline mapping failed", e);
                    }

                    #endregion
                }
                else if (fromElements.ElementType == ElementType.XYPolygon &&
                         toElements.ElementType == ElementType.XYPolygon)
                    // Polygon to Polygon
                {
                    #region

                    try
                    {
                        for (int i = 0; i < RowsCount; i++)
                        {
                            XYPolygon toPolygon = toXYPolygons[i];
                            for (int j = 0; j < ColumnsCount; j++)
                            {
                                XYPolygon fromPolygon = fromXYPolygons[j];
                                MappingMatrix[i, j] =
                                    XYGeometryTools.CalculateSharedArea(toPolygon,
                                                                        fromPolygon);
                            }
                            if (
                                CurrentMethod.ID.Equals(
                                    (int) DefaultMethodType.PolygonToPolygon.WeightedMean))
                            {
                                double denominator = 0;
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    denominator = denominator + MappingMatrix[i, j];
                                }
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    if (denominator != 0)
                                    {
                                        MappingMatrix[i, j] = MappingMatrix[i, j]/
                                                              denominator;
                                    }
                                }
                            }
                                // if (currentMethodId.Equals((int) DefaultMethodType.PolygonToPolygon.WeightedMean)) 
                            else if (
                                CurrentMethod.ID.Equals(
                                    (int)
                                    DefaultMethodType.PolygonToPolygon.WeightedSum))
                            {
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    MappingMatrix[i, j] = MappingMatrix[i, j]/
                                                          toPolygon.GetArea();
                                }
                            }
                                // else if (currentMethodId.Equals((int) DefaultMethodType.PolygonToPolygon.WeightedSum))
                            else // currentMethodId != WeightedMean and WeightedSum
                            {
                                throw new Exception(
                                    "methodDescription unknown for polygon to polygon mapping");
                            }
                        }
                    }
                    catch (Exception e) // catch for all of Polygon to Polygon
                    {
                        throw new Exception("Polygon to polygon mapping failed", e);
                    }

                    #endregion
                }
                else if (fromElements.ElementType == ElementType.XYPolyLine &&
                    toElements.ElementType == ElementType.XYPolyLine)
                // PolyLine to PolyLine
                {
                    #region


                    if (toElements.Equals(fromElements) && CurrentMethod.ID.Equals((int)DefaultMethodType.PolylineToPolyline.Value)) 
                    {
                        int nElements = fromElements.ElementCount;


                        if (useSparseMatrix)
                        {
                            MappingMatrix = new DoubleSparseMatrix(nElements, nElements);
                        }
                        else
                        {
                            MappingMatrix = new DoubleMatrix(nElements, nElements);
                        }

                        for (int i = 0; i < nElements; i++)
                        {
                            MappingMatrix[i, i] = 1;
                        }
                    }
                    else
                    {
                        throw new Exception("Polyline to Polyline mapping failed");
                    }
                    #endregion
                }
                else // if the fromElementType, toElementType combination is no implemented
                {
                    throw new Exception(
                        "Mapping of specified ElementTypes not included in ElementMapper");
                }
            }
            catch (Exception e)
            {
                throw new Exception("UpdateMappingMatrix failed to update mapping matrix", e);
            }
        }

        /// <summary>
        /// Extracts the (row, column) element from the MappingMatrix.
        /// </summary>
        /// 
        /// <param name="row">Zero based row index</param>
        /// <param name="column">Zero based column index</param>
        /// <returns>
        /// Element(row, column) from the mapping matrix.
        /// </returns>
        public virtual double GetValueFromMappingMatrix(int row, int column)
        {
            try
            {
                ValidateIndicies(row, column);
            }
            catch (Exception e)
            {
                throw new Exception("GetValueFromMappingMatrix failed.", e);
            }
            return MappingMatrix[row, column];
        }

        /// <summary>
        /// Sets individual the (row, column) element in the MappingMatrix.
        /// </summary>
        /// 
        /// <param name="value">Element value to set</param>
        /// <param name="row">Zero based row index</param>
        /// <param name="column">Zero based column index</param>
        /// <returns>
        /// No value is returned.
        /// </returns>
        public virtual void SetValueInMappingMatrix(double value, int row, int column)
        {
            try
            {
                ValidateIndicies(row, column);
            }
            catch (Exception e)
            {
                throw new Exception("SetValueInMappingMatrix failed.", e);
            }
            MappingMatrix[row, column] = value;
        }

        protected void ValidateIndicies(int row, int column)
        {
            if (row < 0)
            {
                throw new Exception("Negative row index not allowed. GetValueFromMappingMatrix failed.");
            }
            else if (row >= RowsCount)
            {
                throw new Exception("Row index exceeds mapping matrix dimension. GetValueFromMappingMatrix failed.");
            }
            else if (column < 0)
            {
                throw new Exception("Negative column index not allowed. GetValueFromMappingMatrix failed.");
            }
            else if (column >= ColumnsCount)
            {
                throw new Exception("Column index exceeds mapping matrix dimension. GetValueFromMappingMatrix failed.");
            }
        }

        /// <summary>
        /// Gives a list of descriptions (strings) for available mapping methods 
        /// given the combination of fromElementType and toElementType
        /// </summary>
        /// 
        /// <param name="fromElementsElementType">Element type of the elements in
        /// the fromElementset</param>
        /// <param name="toElementsElementType">Element type of the elements in
        /// the toElementset</param>
        /// 
        /// <returns>
        ///	<p>ArrayList of method descriptions</p>
        /// </returns>
        public virtual ArrayList GetAvailableMethods(ElementType fromElementsElementType,
                                                     ElementType toElementsElementType)
        {
            ArrayList methodDescriptions = new ArrayList();

            for (int i = 0; i < methods.Count; i++)
            {
                if (fromElementsElementType == methods[i].FromElementsShapeType)
                {
                    if (toElementsElementType == methods[i].ToElementsShapeType)
                    {
                        methodDescriptions.Add(methods[i].Description);
                    }
                }
            }
            return methodDescriptions;
        }

        /// <summary>
        /// Gives a list of ID's (strings) for available mapping methods 
        /// given the combination of fromElementType and toElementType
        /// </summary>
        /// 
        /// <param name="fromElementsElementType">Element type of the elements in
        /// the fromElementset</param>
        /// <param name="toElementsElementType">Element type of the elements in
        /// the toElementset</param>
        /// 
        /// <returns>
        ///	<p>ArrayList of method ID's</p>
        /// </returns>
        public virtual ArrayList GetIDsForAvailableDataOperations(ElementType fromElementsElementType,
                                                                  ElementType toElementsElementType)
        {
            ArrayList methodIDs = new ArrayList();

            for (int i = 0; i < methods.Count; i++)
            {
                if (fromElementsElementType == methods[i].FromElementsShapeType)
                {
                    if (toElementsElementType == methods[i].ToElementsShapeType)
                    {
                        methodIDs.Add("ElementMapper" + methods[i].ID);
                    }
                }
            }
            return methodIDs;
        }

        /// <summary>
        /// This method will return an ArrayList of IDataOperations that the ElementMapper provides when
        /// mapping from the ElementType specified in the method argument. 
        /// </summary>
        /// <remarks>
        ///  Each IDataOperation object will contain 3 IArguments:
        ///  <p> [Key]              [Value]                      [ReadOnly]    [Description]----------------- </p>
        ///  <p> ["Type"]           ["SpatialMapping"]           [true]        ["Using the ElementMapper"] </p>
        ///  <p> ["ID"]             [The Operation ID]           [true]        ["Internal ElementMapper dataoperation ID"] </p>
        ///  <p> ["Description"]    [The Operation Description]  [true]        ["Using the ElementMapper"] </p>
        ///  <p> ["ToElementType"]  [ElementType]                [true]        ["Valid To-Element Types"]  </p>
        /// </remarks>
        /// <param name="fromElementsElementType"></param>
        /// <returns>
        ///  ArrayList which contains the available dataOperations (IDataOperation).
        /// </returns>
        public static IList<DataOperation> GetAvailableDataOperations(ElementType fromElementsElementType)
        {
            if(dataOperations.ContainsKey(fromElementsElementType))
            {
                return dataOperations[fromElementsElementType];
            }
            
            var op = new List<DataOperation>();

            for (var i = 0; i < methods.Count; i++)
            {
                if (fromElementsElementType != methods[i].FromElementsShapeType)
                {
                    continue;
                }

                var dataOperation = new DataOperation("ElementMapper" + methods[i].ID);
                dataOperation.AddArgument(new Argument("ID", methods[i].ID.ToString(), true,
                                                       "Internal ElementMapper dataoperation ID"));
                dataOperation.AddArgument(new Argument("Description", methods[i].Description, true,
                                                       "Operation description"));
                dataOperation.AddArgument(new Argument("Type", "SpatialMapping", true, "Using the ElementMapper"));
                dataOperation.AddArgument(new Argument("FromElementType",
                                                       methods[i].FromElementsShapeType.ToString(), true,
                                                       "Valid From-Element Types"));
                dataOperation.AddArgument(new Argument("ToElementType",
                                                       methods[i].ToElementsShapeType.ToString(), true,
                                                       "Valid To-Element Types"));

                op.Add(dataOperation);
            }

            dataOperations[fromElementsElementType] = op;

            return op;
        }

        /// <summary>
        /// Cache data operations (performance)
        /// </summary>
        private static IDictionary<ElementType, IList<DataOperation>> dataOperations = new Dictionary<ElementType, IList<DataOperation>>();

        protected virtual ElementMappingMethod GetMethod(string methodDescription, ElementType fromElementsElementType, ElementType toElementsElementType)
        {
            for (int i = 0; i < methods.Count; i++)
            {
                if (fromElementsElementType == methods[i].FromElementsShapeType)
                {
                    if (toElementsElementType == methods[i].ToElementsShapeType)
                    {
                        if (methodDescription == methods[i].Description)
                            return methods[i];
                    }
                }
            }
            throw new Exception("methodDescription: " + methodDescription +
                                " not known for fromElementType: " + fromElementsElementType +
                                " and to ElementType: " + toElementsElementType);
        }

        protected virtual XYPoint CreateXYPoint(IElementSet elementSet, int index)
        {
            if (elementSet.ElementType != ElementType.XYPoint)
            {
                throw new Exception("Cannot create XYPoint");
            }

            XYPoint xyPoint = new XYPoint();
            xyPoint.X = elementSet.GetXCoordinate(index, 0);
            xyPoint.Y = elementSet.GetYCoordinate(index, 0);
            return xyPoint;
        }

        public static XYPolyline CreateXYPolyline(IElementSet elementSet, int index)
        {
            if (!(elementSet.ElementType == ElementType.XYPolyLine || elementSet.ElementType == ElementType.XYLine))
            {
                throw new Exception("Cannot create XYPolyline");
            }

            XYPolyline xyPolyline = new XYPolyline();
            for (int i = 0; i < elementSet.GetVertexCount(index); i++)
            {
                xyPolyline.Points.Add(new XYPoint(elementSet.GetXCoordinate(index, i),
                                                  elementSet.GetYCoordinate(index, i)));
            }

            return xyPolyline;
        }

        protected virtual XYPolygon CreateFromXYPolygon(IElementSet elementSet, int index)
        {
            if (elementSet.ElementType != ElementType.XYPolygon)
            {
                throw new Exception("Cannot create XYPolyline");
            }

            XYPolygon xyPolygon = new XYPolygon();

            for (int i = 0; i < elementSet.GetVertexCount(index); i++)
            {
                xyPolygon.Points.Add(new XYPoint(elementSet.GetXCoordinate(index, i),
                                                 elementSet.GetYCoordinate(index, i)));
            }

            return xyPolygon;
        }

        #region Nested type: DefaultMethodType

        public struct DefaultMethodType
        {
            #region LineToPoint enum

            public enum LineToPoint
            {
                Nearest = 1000,
                Inverse = 1001,
            }

            #endregion

            #region LineToPolygon enum

            public enum LineToPolygon
            {
                WeightedMean = 1100,
                WeightedSum = 1101,
            }

            #endregion

            #region PointToLine enum

            public enum PointToLine
            {
                Nearest = 900,
                Inverse = 901,
            }

            #endregion

            #region PointToPoint enum

            public enum PointToPoint
            {
                Nearest = 100,
                Inverse = 101
            }

            #endregion

            #region PointToPolygon enum

            public enum PointToPolygon
            {
                Mean = 300,
                Sum = 301,
            }

            #endregion

            #region PointToPolyline enum

            public enum PointToPolyline
            {
                Nearest = 200,
                Inverse = 201,
            }

            #endregion

            #region PolygonToLine enum

            public enum PolygonToLine
            {
                WeightedMean = 1200,
                WeightedSum = 1201,
            }

            #endregion

            #region PolygonToPoint enum

            public enum PolygonToPoint
            {
                Value = 600,
            }

            #endregion

            #region PolygonToPolygon enum

            public enum PolygonToPolygon
            {
                WeightedMean = 800,
                WeightedSum = 801,
            }

            #endregion

            #region PolygonToPolyline enum

            public enum PolygonToPolyline
            {
                WeightedMean = 700,
                WeightedSum = 701,
            }

            #endregion

            #region PolylineToPoint enum

            public enum PolylineToPoint
            {
                Nearest = 400,
                Inverse = 401,
            }

            #endregion

            #region PolylineToPolygon enum

            public enum PolylineToPolygon
            {
                WeightedMean = 500,
                WeightedSum = 501,
            }

            #endregion

            #region PolylineToPolyline enum

            public enum PolylineToPolyline
            {
                Value = 1300
            }

            #endregion
        }

        #endregion
    }
=======
#region Copyright

/*
* Copyright (c) 2005,2006,2007, OpenMI Association
* All rights reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions are met:
*     * Redistributions of source code must retain the above copyright
*       notice, this list of conditions and the following disclaimer.
*     * Redistributions in binary form must reproduce the above copyright
*       notice, this list of conditions and the following disclaimer in the
*       documentation and/or other materials provided with the distribution.
*     * Neither the name of the OpenMI Association nor the
*       names of its contributors may be used to endorse or promote products
*       derived from this software without specific prior written permission.
*
* THIS SOFTWARE IS PROVIDED BY "OpenMI Association" ``AS IS'' AND ANY
* EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
* DISCLAIMED. IN NO EVENT SHALL "OpenMI Association" BE LIABLE FOR ANY
* DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
* SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Oatc.OpenMI.Sdk.Backbone;
using OpenMI.Standard;

namespace Oatc.OpenMI.Sdk.Spatial
{
    /// <summary>
    /// The ElementMapper converts one ValueSet (inputValues) associated one ElementSet (fromElements)
    /// to a new ValuesSet (return value of MapValue) that corresponds to another ElementSet 
    /// (toElements). The conversion is a two step procedure where the first step (Initialize) is 
    /// executed at initialisation time only, whereas the MapValues is executed during time stepping.
    /// 
    /// <p>The Initialize method will create a conversion matrix with the same number of rows as the
    /// number of elements in the ElementSet associated to the accepting component (i.e. the toElements) 
    /// and the same number of columns as the number of elements in the ElementSet associated to the 
    /// providing component (i.e. the fromElements).</p>
    /// 
    /// <p>Mapping is possible for any zero-, one- and two-dimensional elemets. Zero dimensional 
    /// elements will always be points, one-dimensional elements will allways be polylines and two-
    /// dimensional elements will allways be polygons.</p>
    /// 
    /// <p>The ElementMapper contains a number of methods for mapping between the different element types.
    /// As an example polyline to polygon mapping may be done either as Weighted Mean or as Weighted Sum.
    /// Typically the method choice will depend on the quantity mapped. Such that state variables such as 
    /// water level will be mapped using Weighted Mean whereas flux variables such as seepage from river 
    /// to groundwater will be mapped using Weighted Sum. The list of available methods for a given 
    /// combination of from and to element types is obtained using the GetAvailableMethods method.</p>
    /// </summary>
    public class ElementMapper
    {
        private static readonly IList<ElementMappingMethod> methods;
        
        private ElementMappingMethod currentMethod;

        private bool isInitialised;

        private IMatrix<double> mappingMatrix;

        // these 2 fields are added for debugging purposes
        private IMatrix<double> intersectedLengthMatrix;

        private int rowsCount;
        private int columnsCount;

        static ElementMapper()
        {
            methods = new List<ElementMappingMethod>();

            AddNewMethod(ElementType.XYPoint, ElementType.XYPoint, "Nearest", (int)DefaultMethodType.PointToPoint.Nearest);
            AddNewMethod(ElementType.XYPoint, ElementType.XYPoint, "Inverse", (int)DefaultMethodType.PointToPoint.Inverse);
            AddNewMethod(ElementType.XYPoint, ElementType.XYPolyLine, "Nearest", (int)DefaultMethodType.PointToPolyline.Nearest);
            AddNewMethod(ElementType.XYPoint, ElementType.XYPolyLine, "Inverse", (int)DefaultMethodType.PointToPolyline.Inverse);
            AddNewMethod(ElementType.XYPoint, ElementType.XYPolygon, "Mean", (int)DefaultMethodType.PointToPolygon.Mean);
            AddNewMethod(ElementType.XYPoint, ElementType.XYPolygon, "Sum", (int)DefaultMethodType.PointToPolygon.Sum);
            AddNewMethod(ElementType.XYPolyLine, ElementType.XYPoint, "Nearest", (int)DefaultMethodType.PolylineToPoint.Nearest);
            AddNewMethod(ElementType.XYPolyLine, ElementType.XYPoint, "Inverse", (int)DefaultMethodType.PolylineToPoint.Inverse);
            AddNewMethod(ElementType.XYPolyLine, ElementType.XYPolygon, "Weighted Mean", (int)DefaultMethodType.PolylineToPolygon.WeightedMean);
            AddNewMethod(ElementType.XYPolyLine, ElementType.XYPolygon, "Weighted Sum", (int)DefaultMethodType.PolylineToPolygon.WeightedSum);
            AddNewMethod(ElementType.XYPolygon, ElementType.XYPoint, "Value", (int)DefaultMethodType.PolygonToPoint.Value);
            AddNewMethod(ElementType.XYPolygon, ElementType.XYPolyLine, "Weighted Mean", (int)DefaultMethodType.PolygonToPolyline.WeightedMean);
            AddNewMethod(ElementType.XYPolygon, ElementType.XYPolyLine, "Weighted Sum", (int)DefaultMethodType.PolygonToPolyline.WeightedSum);
            AddNewMethod(ElementType.XYPolygon, ElementType.XYPolygon, "Weighted Mean", (int)DefaultMethodType.PolygonToPolygon.WeightedMean);
            AddNewMethod(ElementType.XYPolygon, ElementType.XYPolygon, "Weighted Sum", (int)DefaultMethodType.PolygonToPolygon.WeightedSum);
            AddNewMethod(ElementType.XYPoint, ElementType.XYLine, "Nearest", (int)DefaultMethodType.PointToLine.Nearest);
            AddNewMethod(ElementType.XYPoint, ElementType.XYLine, "Inverse", (int)DefaultMethodType.PointToLine.Inverse);
            AddNewMethod(ElementType.XYLine, ElementType.XYPoint, "Nearest", (int)DefaultMethodType.LineToPoint.Nearest);
            AddNewMethod(ElementType.XYLine, ElementType.XYPoint, "Inverse", (int)DefaultMethodType.LineToPoint.Inverse);
            AddNewMethod(ElementType.XYLine, ElementType.XYPolygon, "Weighted Mean", (int)DefaultMethodType.LineToPolygon.WeightedMean);
            AddNewMethod(ElementType.XYLine, ElementType.XYPolygon, "Weighted Sum", (int)DefaultMethodType.LineToPolygon.WeightedSum);
            AddNewMethod(ElementType.XYPolygon, ElementType.XYLine, "Weighted Mean", (int)DefaultMethodType.PolygonToLine.WeightedMean);
            AddNewMethod(ElementType.XYPolygon, ElementType.XYLine, "Weighted Sum", (int)DefaultMethodType.PolygonToLine.WeightedSum);
            AddNewMethod(ElementType.XYPolyLine, ElementType.XYPolyLine, "Value", (int)DefaultMethodType.PolylineToPolyline.Value); // TODO: do we need this one? remove it.
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ElementMapper()
        {
            isInitialised = false;
        }

        public static void AddNewMethod(ElementType from, ElementType to, string name, int id)
        {
            methods.Add(new ElementMappingMethod
                (id, name, from, to));
        }

        public virtual IMatrix<double> MappingMatrix
        {
            get { return mappingMatrix; } 
            set { mappingMatrix = value; }
        }

        public virtual IMatrix<double> IntersectedLengthMatrix
        {
            get { return intersectedLengthMatrix; }
            set { intersectedLengthMatrix = value; }
        }

        public virtual int RowsCount
        {
            get { return rowsCount; }
            set { rowsCount = value; }
        }

        public virtual int ColumnsCount
        {
            get { return columnsCount; }
            set { columnsCount = value; }
        }

        public virtual bool IsInitialised { get { return isInitialised; } }

        public virtual ElementMappingMethod CurrentMethod
        {
            get { return currentMethod; }
            set { currentMethod = value; }
        }

        public virtual IList<ElementMappingMethod> Methods { get { return methods; } }

        public static bool SupportsCaching { get; set; }

        public static string CacheFileDirectory { get; set; }

        /// <summary>
        /// Initialises the ElementMapper. The initialisation includes setting the isInitialised
        /// flag and calls UpdateMappingMatrix for claculation of the mapping matrix.
        /// </summary>
        ///
        /// <param name="methodDescription">String description of mapping method</param> 
        /// <param name="fromElements">The IElementSet to map from.</param>
        /// <param name="toElements">The IElementSet to map to</param>
        /// 
        /// <returns>
        /// The method has no return value.
        /// </returns>
        public virtual void Initialise(string methodDescription, IElementSet fromElements, IElementSet toElements)
        {
            this.fromElements = fromElements;
            this.toElements = toElements;

            RowsCount = toElements.ElementCount;
            ColumnsCount = fromElements.ElementCount;

            if (SupportsCaching && CacheFileDirectory != null && LoadMappingMatrixFromCache(methodDescription, fromElements, toElements))
            {
                isInitialised = true;
                return; // no need to calculate mapping matrix
            }

            fromXYPolygons.Clear();
            toXYPolygons.Clear();

            CreateGeometries();

            UpdateMappingMatrix(methodDescription, fromElements, toElements);

            if (SupportsCaching && CacheFileDirectory != null)
            {
                SaveMappingMatrixToCache(methodDescription, fromElements, toElements);
            }

            isInitialised = true;
        }

        private bool LoadMappingMatrixFromCache(string method, IElementSet fromElementSet, IElementSet toElementSet)
        {
            var cacheFilePath = GetCacheFilePath(method, fromElementSet, toElementSet);

            FileStream file = null;
            try
            {
                if (File.Exists(cacheFilePath))
                {
                    file = File.Open(cacheFilePath, FileMode.Open);

                    var bf = new BinaryFormatter();

                    var cachedMethod = bf.Deserialize(file) as string;
                    var cachedFromElementSetID = bf.Deserialize(file) as string;
                    var cachedFromElementSetElementCount = bf.Deserialize(file) as int[];
                    var cachedToElementSetID = bf.Deserialize(file) as string;
                    var cachedToElementSetElementCount = bf.Deserialize(file) as int[];

                    if (cachedMethod != method
                        || !fromElementSet.ID.Equals(cachedFromElementSetID)
                        || !fromElementSet.ElementCount.Equals(cachedFromElementSetElementCount[0])
                        || !toElementSet.ID.Equals(cachedToElementSetID)
                        || !toElementSet.ElementCount.Equals(cachedToElementSetElementCount[0]))
                    {
                        return false;
                    }

                    MappingMatrix = bf.Deserialize(file) as IMatrix<double>;

                    // when file format changes - no exception is thrown on the above line so we have to make additional check here
                    if(MappingMatrix == null || (MappingMatrix is DoubleSparseMatrix && ((DoubleSparseMatrix)MappingMatrix).Values == null))
                    {
                        throw new IOException("File format is incorrect");
                    }

                    file.Close();

                    return true;
                }
            }
            catch(Exception e)
            {
                if(file != null)
                {
                    file.Close();
                }
                if(File.Exists(cacheFilePath))
                {
                    File.Delete(cacheFilePath);
                }
                return false;
            }


            return false;
        }

        private void SaveMappingMatrixToCache(string method, IElementSet fromElementSet, IElementSet toElementSet)
        {
            var cacheFilePath = GetCacheFilePath(method, fromElementSet, toElementSet);
            var file = File.Open(cacheFilePath, FileMode.Create);
            var bf = new BinaryFormatter();
            bf.Serialize(file, method);
            bf.Serialize(file, fromElementSet.ID);
            bf.Serialize(file, new [] {fromElementSet.ElementCount});
            bf.Serialize(file, toElementSet.ID);
            bf.Serialize(file, new [] {toElementSet.ElementCount});
            bf.Serialize(file, MappingMatrix);
            file.Close();
        }

        public static string GetCacheFilePath(string method, IElementSet fromElementSet, IElementSet toElementSet)
        {
            var cacheFileName = string.Format("ElementMapperCache-{0}-{1}.{2}-{3}.{4}", method, fromElementSet.ID,
                                              fromElementSet.ElementCount, toElementSet.ID, toElementSet.ElementCount);
            return Path.Combine(CacheFileDirectory, cacheFileName);
        }

        protected IList<XYPolygon> fromXYPolygons = new List<XYPolygon>();
        protected IList<XYPolygon> toXYPolygons = new List<XYPolygon>();

        private void CreateGeometries()
        {
            if(fromElements.ElementType == ElementType.XYPolygon)
            {
                for (int i = 0; i < fromElements.ElementCount; i++)
                {
                    fromXYPolygons.Add(CreateFromXYPolygon(fromElements, i));
                }
            }

            if(toElements.ElementType == ElementType.XYPolygon)
            {
                for (int i = 0; i < toElements.ElementCount; i++)
                {
                    toXYPolygons.Add(CreateFromXYPolygon(toElements, i));
                }
            }
        }

        private IElementSet fromElements;
        private IElementSet toElements;

        /// <summary>
        /// MapValues calculates a IValueSet through multiplication of an inputValues IValueSet
        /// vector or matrix (ScalarSet or VectorSet) on to the mapping maprix. IScalarSets maps
        /// to IScalarSets and IVectorSets maps to IVectorSets.
        /// </summary>
        /// 
        /// <remarks>
        /// Mapvalues is called every time a georeferenced link is evaluated.
        /// </remarks>
        /// 
        /// <param name="inputValues">IValueSet of values to be mapped.</param>
        /// 
        /// <returns>
        /// A IValueSet found by mapping of the inputValues on to the toElementSet.
        /// </returns>
        public virtual IValueSet MapValues(IValueSet inputValues)
        {
            if (!isInitialised)
            {
                throw new Exception(
                    "ElementMapper objects needs to be initialised before the MapValue method can be used");
            }

            if (!inputValues.Count.Equals(ColumnsCount))
            {
                throw new Exception("Dimension mismatch between inputValues and mapping matrix");
            }
            
            if (inputValues is IScalarSet)
            {
                return MappingMatrix.Product((IScalarSet)inputValues);
            }
            else if (inputValues is IVectorSet)
            {
                Vector[] outValues = new Vector[RowsCount];
                //--- Multiply the Values vector with the MappingMatrix ---
                for (int i = 0; i < RowsCount; i++)
                {
                    for (int n = 0; n < ColumnsCount; n++)
                    {
                        outValues[i] = new Vector(); // Need this as Initialisation only valid for value types!

                        outValues[i].XComponent += MappingMatrix[i, n]*
                                                   ((IVectorSet) inputValues).GetVector(n).XComponent;
                        outValues[i].YComponent += MappingMatrix[i, n]*
                                                   ((IVectorSet) inputValues).GetVector(n).YComponent;
                        outValues[i].ZComponent += MappingMatrix[i, n]*
                                                   ((IVectorSet) inputValues).GetVector(n).ZComponent;
                    }
                }
                VectorSet outputValues = new VectorSet(outValues);
                return outputValues;
            }
            else
            {
                throw new Exception("Invalid datatype used for inputValues parameter. MapValues failed");
            }
        }

        /// <summary>
        /// Calculates the mapping matrix between fromElements and toElements. The mapping method 
        /// is decided from the combination of methodDescription, fromElements.ElementType and 
        /// toElements.ElementType. 
        /// The valid values for methodDescription is obtained through use of the 
        /// GetAvailableMethods method.
        /// </summary>
        /// 
        /// <remarks>
        /// UpdateMappingMatrix is called during initialisation. UpdateMappingMatrix must be called prior
        /// to Mapvalues.
        /// </remarks>
        /// 
        /// <param name="methodDescription">String description of mapping method</param> 
        /// <param name="fromElements">The IElementset to map from.</param>
        /// <param name="toElements">The IElementset to map to</param>
        ///
        /// <returns>
        /// The method has no return value.
        /// </returns>
        public virtual void UpdateMappingMatrix(string methodDescription, IElementSet fromElements,
                                                IElementSet toElements)
        {
            var useSparseMatrix = true;
            UpdateMappingMatrix(methodDescription, fromElements, toElements, useSparseMatrix);

            // check ratio of non-zero elements
            var nonZeroCellCount = 0;
            for (var row = 0; row < mappingMatrix.RowCount; row++)
            {
                for (var column = 0; column < mappingMatrix.ColumnCount; column++)
                {
                    if(mappingMatrix[row, column] != 0)
                    {
                        nonZeroCellCount++;
                    }
                }
            }

            var totalCellCount = fromElements.ElementCount * toElements.ElementCount;
            if (nonZeroCellCount/(double) totalCellCount <= 0.5) // TODO: expose as a user argument
            {
                return;
            }

            // switch to dense matrix
            var newMappingMatrix = new DoubleMatrix(RowsCount, ColumnsCount);
            for (var row = 0; row < mappingMatrix.RowCount; row++)
            {
                for (var column = 0; column < mappingMatrix.ColumnCount; column++)
                {
                    if (mappingMatrix[row, column] != 0)
                    {
                        newMappingMatrix[row, column] = mappingMatrix[row, column];
                    }
                }
            }

            mappingMatrix = newMappingMatrix;
        }

        public virtual void UpdateMappingMatrix(string methodDescription, IElementSet fromElements,
                                                IElementSet toElements, bool useSparseMatrix)
        {
            try
            {
                ElementSetChecker.CheckElementSet(fromElements);
                ElementSetChecker.CheckElementSet(toElements);

                CurrentMethod = GetMethod(methodDescription, fromElements.ElementType, toElements.ElementType);

                if(useSparseMatrix)
                {
                    MappingMatrix = new DoubleSparseMatrix(RowsCount, ColumnsCount);
                    intersectedLengthMatrix = new DoubleSparseMatrix(RowsCount, ColumnsCount);
                }
                else
                {
                    MappingMatrix = new DoubleMatrix(RowsCount, ColumnsCount);
                    intersectedLengthMatrix = new DoubleMatrix(RowsCount, ColumnsCount);
                }

                

                if (fromElements.ElementType == ElementType.XYPoint && toElements.ElementType == ElementType.XYPoint)
                    // Point to Point
                {
                    #region

                    try
                    {
                        for (int i = 0; i < RowsCount; i++)
                        {
                            XYPoint ToPoint = CreateXYPoint(toElements, i);
                            for (int j = 0; j < ColumnsCount; j++)
                            {
                                XYPoint FromPoint = CreateXYPoint(fromElements, j);
                                MappingMatrix[i, j] = XYGeometryTools.CalculatePointToPointDistance(ToPoint, FromPoint);
                            }
                        }

                        if (CurrentMethod.ID.Equals((int) DefaultMethodType.PointToPoint.Nearest))
                        {
                            for (int i = 0; i < RowsCount; i++)
                            {
                                double MinDist = MappingMatrix[i, 0];
                                for (int j = 1; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] < MinDist)
                                    {
                                        MinDist = MappingMatrix[i, j];
                                    }
                                }
                                int Denominator = 0;
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] == MinDist)
                                    {
                                        MappingMatrix[i, j] = 1;
                                        Denominator++;
                                    }
                                    else
                                    {
                                        MappingMatrix[i, j] = 0;
                                    }
                                }
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    MappingMatrix[i, j] = MappingMatrix[i, j]/Denominator;
                                }
                            }
                        } // if (currentMethodId.Equals((int) DefaultMethodType.PointToPoint.Nearest))   
                        else if (CurrentMethod.ID.Equals((int) DefaultMethodType.PointToPoint.Inverse))
                        {
                            for (int i = 0; i < RowsCount; i++)
                            {
                                double MinDist = MappingMatrix[i, 0];
                                for (int j = 1; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] < MinDist)
                                    {
                                        MinDist = MappingMatrix[i, j];
                                    }
                                }
                                if (MinDist == 0)
                                {
                                    int Denominator = 0;
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        if (MappingMatrix[i, j] == MinDist)
                                        {
                                            MappingMatrix[i, j] = 1;
                                            Denominator++;
                                        }
                                        else
                                        {
                                            MappingMatrix[i, j] = 0;
                                        }
                                    }
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = MappingMatrix[i, j]/Denominator;
                                    }
                                }
                                else
                                {
                                    double Denominator = 0;
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = 1/MappingMatrix[i, j];
                                        Denominator = Denominator + MappingMatrix[i, j];
                                    }
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = MappingMatrix[i, j]/Denominator;
                                    }
                                }
                            }
                        } // else if (currentMethodId.Equals((int) DefaultMethodType.PointToPoint.Inverse))
                        else
                        {
                            throw new Exception("methodDescription unknown for point point mapping");
                        }
                        // else if (currentMethodId.Equals((int) DefaultMethodType.PointToPoint.Nearest)) and else if (currentMethodId.Equals((int) DefaultMethodType.PointToPoint.Inverse))
                    }
                    catch (Exception e) // Catch for all of the Point to Point part
                    {
                        throw new Exception("Point to point mapping failed", e);
                    }

                    #endregion
                }
                else if (fromElements.ElementType == ElementType.XYPoint &&
                         ((toElements.ElementType == ElementType.XYPolyLine) ||
                          (toElements.ElementType == ElementType.XYLine)))
                    // Point to PolyLine/Line
                {
                    #region

                    try
                    {
                        for (int i = 0; i < RowsCount; i++)
                        {
                            XYPolyline toPolyLine = CreateXYPolyline(toElements, i);
                            for (int j = 0; j < ColumnsCount; j++)
                            {
                                XYPoint fromPoint = CreateXYPoint(fromElements, j);
                                MappingMatrix[i, j] = XYGeometryTools.CalculatePolylineToPointDistance(toPolyLine,
                                                                                                       fromPoint);
                            }
                        }

                        if (CurrentMethod.ID.Equals((int) DefaultMethodType.PointToPolyline.Nearest) ||
                            CurrentMethod.ID.Equals((int) DefaultMethodType.PointToLine.Nearest))
                        {
                            for (int i = 0; i < RowsCount; i++)
                            {
                                double MinDist = MappingMatrix[i, 0];
                                for (int j = 1; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] < MinDist)
                                    {
                                        MinDist = MappingMatrix[i, j];
                                    }
                                }
                                int denominator = 0;
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] == MinDist)
                                    {
                                        MappingMatrix[i, j] = 1;
                                        denominator++;
                                    }
                                    else
                                    {
                                        MappingMatrix[i, j] = 0;
                                    }
                                }
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    MappingMatrix[i, j] = MappingMatrix[i, j]/denominator;
                                }
                            }
                        } // if (currentMethodId.Equals((int) DefaultMethodType.PointToPolyline.Nearest))
                        else if ((CurrentMethod.ID.Equals((int) DefaultMethodType.PointToPolyline.Inverse)) ||
                                 (CurrentMethod.ID.Equals((int) DefaultMethodType.PointToLine.Inverse)))
                        {
                            for (int i = 0; i < RowsCount; i++)
                            {
                                double minDist = MappingMatrix[i, 0];
                                for (int j = 1; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] < minDist)
                                    {
                                        minDist = MappingMatrix[i, j];
                                    }
                                }
                                if (minDist == 0)
                                {
                                    int denominator = 0;
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        if (MappingMatrix[i, j] == minDist)
                                        {
                                            MappingMatrix[i, j] = 1;
                                            denominator++;
                                        }
                                        else
                                        {
                                            MappingMatrix[i, j] = 0;
                                        }
                                    }
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = MappingMatrix[i, j]/denominator;
                                    }
                                }
                                else
                                {
                                    double denominator = 0;
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = 1/MappingMatrix[i, j];
                                        denominator = denominator + MappingMatrix[i, j];
                                    }
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = MappingMatrix[i, j]/denominator;
                                    }
                                }
                            }
                        } // else if (currentMethodId.Equals((int) DefaultMethodType.PointToPolyline.Inverse))
                        else // if currentMethodId != Nearest and Inverse
                        {
                            throw new Exception("methodDescription unknown for point to polyline mapping");
                        }
                    }
                    catch (Exception e) // Catch for all of the Point to Polyline part 
                    {
                        throw new Exception("Point to polyline mapping failed", e);
                    }

                    #endregion
                }
                else if (fromElements.ElementType == ElementType.XYPoint &&
                         toElements.ElementType == ElementType.XYPolygon)
                    // Point to Polygon
                {
                    #region

                    try
                    {
                        XYPolygon polygon;
                        XYPoint point;
                        int count;
                        for (int i = 0; i < RowsCount; i++)
                        {
                            polygon = toXYPolygons[i];
                            count = 0;
                            for (int n = 0; n < ColumnsCount; n++)
                            {
                                point = CreateXYPoint(fromElements, n);
                                if (XYGeometryTools.IsPointInPolygon(point, polygon))
                                {
                                    if (CurrentMethod.ID.Equals((int) DefaultMethodType.PointToPolygon.Mean))
                                    {
                                        count = count + 1;
                                    }
                                    else if (CurrentMethod.ID.Equals((int) DefaultMethodType.PointToPolygon.Sum))
                                    {
                                        count = 1;
                                    }
                                    else
                                    {
                                        throw new Exception(
                                            "methodDescription unknown for point to polygon mapping");
                                    }
                                }
                            }
                            for (int n = 0; n < ColumnsCount; n++)
                            {
                                point = CreateXYPoint(fromElements, n);

                                if (XYGeometryTools.IsPointInPolygon(point, polygon))
                                {
                                    MappingMatrix[i, n] = 1.0/count;
                                }
                            }
                        }
                    }
                    catch (Exception e) // Catch for all of the Point to Polyline part 
                    {
                        throw new Exception("Point to polygon mapping failed", e);
                    }

                    #endregion
                }
                else if (((fromElements.ElementType == ElementType.XYPolyLine) ||
                          (fromElements.ElementType == ElementType.XYLine)) &&
                         toElements.ElementType == ElementType.XYPoint)
                    // Polyline/Line to Point
                {
                    #region

                    try
                    {
                        for (int i = 0; i < RowsCount; i++)
                        {
                            XYPoint toPoint = CreateXYPoint(toElements, i);
                            for (int j = 0; j < ColumnsCount; j++)
                            {
                                XYPolyline fromPolyLine = CreateXYPolyline(fromElements, j);
                                MappingMatrix[i, j] =
                                    XYGeometryTools.CalculatePolylineToPointDistance(fromPolyLine, toPoint);
                            }
                        }

                        if (CurrentMethod.ID.Equals((int) DefaultMethodType.PolylineToPoint.Nearest) ||
                            CurrentMethod.ID.Equals((int) DefaultMethodType.LineToPoint.Nearest))
                        {
                            for (int i = 0; i < RowsCount; i++)
                            {
                                double minDist = MappingMatrix[i, 0];
                                for (int j = 1; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] < minDist)
                                    {
                                        minDist = MappingMatrix[i, j];
                                    }
                                }
                                int denominator = 0;
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] == minDist)
                                    {
                                        MappingMatrix[i, j] = 1;
                                        denominator++;
                                    }
                                    else
                                    {
                                        MappingMatrix[i, j] = 0;
                                    }
                                }
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    MappingMatrix[i, j] = MappingMatrix[i, j]/denominator;
                                }
                            }
                        } // if (currentMethodId.Equals((int) DefaultMethodType.PolylineToPoint.Nearest))
                        else if (CurrentMethod.ID.Equals((int) DefaultMethodType.PolylineToPoint.Inverse) ||
                                 CurrentMethod.ID.Equals((int) DefaultMethodType.LineToPoint.Inverse))
                        {
                            for (int i = 0; i < RowsCount; i++)
                            {
                                double minDist = MappingMatrix[i, 0];
                                for (int j = 1; j < ColumnsCount; j++)
                                {
                                    if (MappingMatrix[i, j] < minDist)
                                    {
                                        minDist = MappingMatrix[i, j];
                                    }
                                }
                                if (minDist == 0)
                                {
                                    int denominator = 0;
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        if (MappingMatrix[i, j] == minDist)
                                        {
                                            MappingMatrix[i, j] = 1;
                                            denominator++;
                                        }
                                        else
                                        {
                                            MappingMatrix[i, j] = 0;
                                        }
                                    }
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = MappingMatrix[i, j]/denominator;
                                    }
                                }
                                else
                                {
                                    double denominator = 0;
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = 1/MappingMatrix[i, j];
                                        denominator = denominator + MappingMatrix[i, j];
                                    }
                                    for (int j = 0; j < ColumnsCount; j++)
                                    {
                                        MappingMatrix[i, j] = MappingMatrix[i, j]/denominator;
                                    }
                                }
                            }
                        } // if (currentMethodId.Equals((int) DefaultMethodType.PolylineToPoint.Inverse))
                        else // MethodID != Nearest and Inverse
                        {
                            throw new Exception(
                                "methodDescription unknown for polyline to point mapping");
                        }
                    }
                    catch (Exception e) // Catch for all of the Point to Polyline part 
                    {
                        throw new Exception("Polyline to point mapping failed", e);
                    }

                    #endregion
                }
                else if (((fromElements.ElementType == ElementType.XYPolyLine) ||
                          (fromElements.ElementType == ElementType.XYLine)) &&
                         toElements.ElementType == ElementType.XYPolygon)
                    // PolyLine to Polygon
                {
                    #region

                    try
                    {
                        for (int i = 0; i < RowsCount; i++)
                        {
                            XYPolygon polygon = toXYPolygons[i];

                            if (
                                CurrentMethod.ID.Equals(
                                    (int) DefaultMethodType.PolylineToPolygon.WeightedMean) ||
                                CurrentMethod.ID.Equals((int) DefaultMethodType.LineToPolygon.WeightedMean))
                            {
                                double totalLineLengthInPolygon = 0;
                                for (int n = 0; n < ColumnsCount; n++)
                                {
                                    XYPolyline polyline = CreateXYPolyline(fromElements, n);
                                    MappingMatrix[i, n] =
                                        XYGeometryTools.CalculateLengthOfPolylineInsidePolygon(
                                            polyline, polygon);

                                    IntersectedLengthMatrix[i, n] = MappingMatrix[i, n];

                                    totalLineLengthInPolygon += MappingMatrix[i, n];
                                }
                                if (totalLineLengthInPolygon > 0)
                                {
                                    for (int n = 0; n < ColumnsCount; n++)
                                    {
                                        MappingMatrix[i, n] = MappingMatrix[i, n]/
                                                              totalLineLengthInPolygon;
                                    }
                                }
                            }
                                // if (currentMethodId.Equals((int) DefaultMethodType.PolylineToPolygon.WeightedMean))
                            else if (
                                CurrentMethod.ID.Equals(
                                    (int) DefaultMethodType.PolylineToPolygon.WeightedSum) ||
                                CurrentMethod.ID.Equals(
                                    (int) DefaultMethodType.LineToPolygon.WeightedSum))
                            {
                                for (int n = 0; n < ColumnsCount; n++)
                                {
                                    XYPolyline polyline = CreateXYPolyline(fromElements, n);
                                    MappingMatrix[i, n] =
                                        XYGeometryTools.CalculateLengthOfPolylineInsidePolygon(
                                            polyline, polygon)/polyline.GetLength();

                                    IntersectedLengthMatrix[i, n] = MappingMatrix[i, n];
                                } // for (int n = 0; n < ColumnsCount; n++)
                            }
                                // else if (currentMethodId.Equals((int) DefaultMethodType.PolylineToPolygon.WeightedSum))
                            else // if MethodID != WeightedMean and WeigthedSum
                            {
                                throw new Exception(
                                    "methodDescription unknown for polyline to polygon mapping");
                            }
                        } // for (int i = 0; i < RowsCount; i++)
                    }
                    catch (Exception e) // Catch for all of polyLine to polygon
                    {
                        throw new Exception("Polyline to polygon mapping failed", e);
                    }

                    #endregion
                }
                else if (fromElements.ElementType == ElementType.XYPolygon &&
                         toElements.ElementType == ElementType.XYPoint)
                    // Polygon to Point
                {
                    #region

                    try
                    {
                        for (int n = 0; n < RowsCount; n++)
                        {
                            XYPoint point = CreateXYPoint(toElements, n);
                            for (int i = 0; i < ColumnsCount; i++)
                            {
                                XYPolygon polygon = fromXYPolygons[i];
                                if (XYGeometryTools.IsPointInPolygon(point, polygon))
                                {
                                    if (
                                        CurrentMethod.ID.Equals(
                                            (int) DefaultMethodType.PolygonToPoint.Value))
                                    {
                                        MappingMatrix[n, i] = 1.0;
                                    }
                                    else // if currentMethodId != Value
                                    {
                                        throw new Exception(
                                            "methodDescription unknown for polygon to point mapping");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e) // catch for all of Polygon to Point
                    {
                        throw new Exception("Polygon to point mapping failed", e);
                    }

                    #endregion
                }
                else if (fromElements.ElementType == ElementType.XYPolygon &&
                         ((toElements.ElementType == ElementType.XYPolyLine) ||
                          (toElements.ElementType == ElementType.XYLine)))
                    // Polygon to PolyLine
                {
                    #region

                    try
                    {
                        for (int i = 0; i < RowsCount; i++)
                        {
                            XYPolyline polyline = CreateXYPolyline(toElements, i);
                            if (
                                CurrentMethod.ID.Equals(
                                    (int) DefaultMethodType.PolygonToPolyline.WeightedMean) ||
                                CurrentMethod.ID.Equals(
                                    (int) DefaultMethodType.PolygonToLine.WeightedMean))
                            {
                                for (int n = 0; n < ColumnsCount; n++)
                                {
                                    XYPolygon polygon = fromXYPolygons[n];
                                    var intersectedLength = XYGeometryTools.CalculateLengthOfPolylineInsidePolygon(polyline, polygon);
                                    MappingMatrix[i, n] = intersectedLength/polyline.GetLength();

                                    IntersectedLengthMatrix[i, n] = intersectedLength;
                                }
                                double sum = 0;
                                for (int n = 0; n < ColumnsCount; n++)
                                {
                                    sum += MappingMatrix[i, n];
                                }
                                for (int n = 0; n < ColumnsCount; n++)
                                {
                                    MappingMatrix[i, n] = MappingMatrix[i, n]/sum;
                                }
                            }
                                // if (currentMethodId.Equals((int) DefaultMethodType.PolygonToPolyline.WeightedMean))
                            else if (
                                CurrentMethod.ID.Equals((int) DefaultMethodType.PolygonToPolyline.WeightedSum) ||
                                CurrentMethod.ID.Equals((int) DefaultMethodType.PolygonToLine.WeightedSum))
                            {
                                for (int n = 0; n < ColumnsCount; n++)
                                {
                                    XYPolygon polygon = fromXYPolygons[n];
                                    var intersectedLength = XYGeometryTools.CalculateLengthOfPolylineInsidePolygon(polyline, polygon);
                                    MappingMatrix[i, n] = intersectedLength/polyline.GetLength();
                                    IntersectedLengthMatrix[i, n] = intersectedLength;
                                }
                            }
                                // else if (currentMethodId.Equals((int) DefaultMethodType.PolygonToPolyline.WeightedSum))
                            else // currentMethodId != WeightedMean and WeightedSum
                            {
                                throw new Exception(
                                    "methodDescription unknown for polygon to polyline mapping");
                            }
                        }
                    }
                    catch (Exception e) // catch for all of Polygon to PolyLine
                    {
                        throw new Exception("Polygon to polyline mapping failed", e);
                    }

                    #endregion
                }
                else if (fromElements.ElementType == ElementType.XYPolygon &&
                         toElements.ElementType == ElementType.XYPolygon)
                    // Polygon to Polygon
                {
                    #region

                    try
                    {
                        for (int i = 0; i < RowsCount; i++)
                        {
                            XYPolygon toPolygon = toXYPolygons[i];
                            for (int j = 0; j < ColumnsCount; j++)
                            {
                                XYPolygon fromPolygon = fromXYPolygons[j];
                                MappingMatrix[i, j] =
                                    XYGeometryTools.CalculateSharedArea(toPolygon,
                                                                        fromPolygon);
                            }
                            if (
                                CurrentMethod.ID.Equals(
                                    (int) DefaultMethodType.PolygonToPolygon.WeightedMean))
                            {
                                double denominator = 0;
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    denominator = denominator + MappingMatrix[i, j];
                                }
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    if (denominator != 0)
                                    {
                                        MappingMatrix[i, j] = MappingMatrix[i, j]/
                                                              denominator;
                                    }
                                }
                            }
                                // if (currentMethodId.Equals((int) DefaultMethodType.PolygonToPolygon.WeightedMean)) 
                            else if (
                                CurrentMethod.ID.Equals(
                                    (int)
                                    DefaultMethodType.PolygonToPolygon.WeightedSum))
                            {
                                for (int j = 0; j < ColumnsCount; j++)
                                {
                                    MappingMatrix[i, j] = MappingMatrix[i, j]/
                                                          toPolygon.GetArea();
                                }
                            }
                                // else if (currentMethodId.Equals((int) DefaultMethodType.PolygonToPolygon.WeightedSum))
                            else // currentMethodId != WeightedMean and WeightedSum
                            {
                                throw new Exception(
                                    "methodDescription unknown for polygon to polygon mapping");
                            }
                        }
                    }
                    catch (Exception e) // catch for all of Polygon to Polygon
                    {
                        throw new Exception("Polygon to polygon mapping failed", e);
                    }

                    #endregion
                }
                else if (fromElements.ElementType == ElementType.XYPolyLine &&
                    toElements.ElementType == ElementType.XYPolyLine)
                // PolyLine to PolyLine
                {
                    #region


                    if (toElements.Equals(fromElements) && CurrentMethod.ID.Equals((int)DefaultMethodType.PolylineToPolyline.Value)) 
                    {
                        int nElements = fromElements.ElementCount;


                        if (useSparseMatrix)
                        {
                            MappingMatrix = new DoubleSparseMatrix(nElements, nElements);
                        }
                        else
                        {
                            MappingMatrix = new DoubleMatrix(nElements, nElements);
                        }

                        for (int i = 0; i < nElements; i++)
                        {
                            MappingMatrix[i, i] = 1;
                        }
                    }
                    else
                    {
                        throw new Exception("Polyline to Polyline mapping failed");
                    }
                    #endregion
                }
                else // if the fromElementType, toElementType combination is no implemented
                {
                    throw new Exception(
                        "Mapping of specified ElementTypes not included in ElementMapper");
                }
            }
            catch (Exception e)
            {
                throw new Exception("UpdateMappingMatrix failed to update mapping matrix", e);
            }
        }

        /// <summary>
        /// Extracts the (row, column) element from the MappingMatrix.
        /// </summary>
        /// 
        /// <param name="row">Zero based row index</param>
        /// <param name="column">Zero based column index</param>
        /// <returns>
        /// Element(row, column) from the mapping matrix.
        /// </returns>
        public virtual double GetValueFromMappingMatrix(int row, int column)
        {
            try
            {
                ValidateIndicies(row, column);
            }
            catch (Exception e)
            {
                throw new Exception("GetValueFromMappingMatrix failed.", e);
            }
            return MappingMatrix[row, column];
        }

        /// <summary>
        /// Sets individual the (row, column) element in the MappingMatrix.
        /// </summary>
        /// 
        /// <param name="value">Element value to set</param>
        /// <param name="row">Zero based row index</param>
        /// <param name="column">Zero based column index</param>
        /// <returns>
        /// No value is returned.
        /// </returns>
        public virtual void SetValueInMappingMatrix(double value, int row, int column)
        {
            try
            {
                ValidateIndicies(row, column);
            }
            catch (Exception e)
            {
                throw new Exception("SetValueInMappingMatrix failed.", e);
            }
            MappingMatrix[row, column] = value;
        }

        protected void ValidateIndicies(int row, int column)
        {
            if (row < 0)
            {
                throw new Exception("Negative row index not allowed. GetValueFromMappingMatrix failed.");
            }
            else if (row >= RowsCount)
            {
                throw new Exception("Row index exceeds mapping matrix dimension. GetValueFromMappingMatrix failed.");
            }
            else if (column < 0)
            {
                throw new Exception("Negative column index not allowed. GetValueFromMappingMatrix failed.");
            }
            else if (column >= ColumnsCount)
            {
                throw new Exception("Column index exceeds mapping matrix dimension. GetValueFromMappingMatrix failed.");
            }
        }

        /// <summary>
        /// Gives a list of descriptions (strings) for available mapping methods 
        /// given the combination of fromElementType and toElementType
        /// </summary>
        /// 
        /// <param name="fromElementsElementType">Element type of the elements in
        /// the fromElementset</param>
        /// <param name="toElementsElementType">Element type of the elements in
        /// the toElementset</param>
        /// 
        /// <returns>
        ///	<p>ArrayList of method descriptions</p>
        /// </returns>
        public virtual ArrayList GetAvailableMethods(ElementType fromElementsElementType,
                                                     ElementType toElementsElementType)
        {
            ArrayList methodDescriptions = new ArrayList();

            for (int i = 0; i < methods.Count; i++)
            {
                if (fromElementsElementType == methods[i].FromElementsShapeType)
                {
                    if (toElementsElementType == methods[i].ToElementsShapeType)
                    {
                        methodDescriptions.Add(methods[i].Description);
                    }
                }
            }
            return methodDescriptions;
        }

        /// <summary>
        /// Gives a list of ID's (strings) for available mapping methods 
        /// given the combination of fromElementType and toElementType
        /// </summary>
        /// 
        /// <param name="fromElementsElementType">Element type of the elements in
        /// the fromElementset</param>
        /// <param name="toElementsElementType">Element type of the elements in
        /// the toElementset</param>
        /// 
        /// <returns>
        ///	<p>ArrayList of method ID's</p>
        /// </returns>
        public virtual ArrayList GetIDsForAvailableDataOperations(ElementType fromElementsElementType,
                                                                  ElementType toElementsElementType)
        {
            ArrayList methodIDs = new ArrayList();

            for (int i = 0; i < methods.Count; i++)
            {
                if (fromElementsElementType == methods[i].FromElementsShapeType)
                {
                    if (toElementsElementType == methods[i].ToElementsShapeType)
                    {
                        methodIDs.Add("ElementMapper" + methods[i].ID);
                    }
                }
            }
            return methodIDs;
        }

        /// <summary>
        /// This method will return an ArrayList of IDataOperations that the ElementMapper provides when
        /// mapping from the ElementType specified in the method argument. 
        /// </summary>
        /// <remarks>
        ///  Each IDataOperation object will contain 3 IArguments:
        ///  <p> [Key]              [Value]                      [ReadOnly]    [Description]----------------- </p>
        ///  <p> ["Type"]           ["SpatialMapping"]           [true]        ["Using the ElementMapper"] </p>
        ///  <p> ["ID"]             [The Operation ID]           [true]        ["Internal ElementMapper dataoperation ID"] </p>
        ///  <p> ["Description"]    [The Operation Description]  [true]        ["Using the ElementMapper"] </p>
        ///  <p> ["ToElementType"]  [ElementType]                [true]        ["Valid To-Element Types"]  </p>
        /// </remarks>
        /// <param name="fromElementsElementType"></param>
        /// <returns>
        ///  ArrayList which contains the available dataOperations (IDataOperation).
        /// </returns>
        public static IList<DataOperation> GetAvailableDataOperations(ElementType fromElementsElementType)
        {
            if(dataOperations.ContainsKey(fromElementsElementType))
            {
                return dataOperations[fromElementsElementType];
            }
            
            var op = new List<DataOperation>();

            for (var i = 0; i < methods.Count; i++)
            {
                if (fromElementsElementType != methods[i].FromElementsShapeType)
                {
                    continue;
                }

                var dataOperation = new DataOperation("ElementMapper" + methods[i].ID);
                dataOperation.AddArgument(new Argument("ID", methods[i].ID.ToString(), true,
                                                       "Internal ElementMapper dataoperation ID"));
                dataOperation.AddArgument(new Argument("Description", methods[i].Description, true,
                                                       "Operation description"));
                dataOperation.AddArgument(new Argument("Type", "SpatialMapping", true, "Using the ElementMapper"));
                dataOperation.AddArgument(new Argument("FromElementType",
                                                       methods[i].FromElementsShapeType.ToString(), true,
                                                       "Valid From-Element Types"));
                dataOperation.AddArgument(new Argument("ToElementType",
                                                       methods[i].ToElementsShapeType.ToString(), true,
                                                       "Valid To-Element Types"));

                op.Add(dataOperation);
            }

            dataOperations[fromElementsElementType] = op;

            return op;
        }

        /// <summary>
        /// Cache data operations (performance)
        /// </summary>
        private static IDictionary<ElementType, IList<DataOperation>> dataOperations = new Dictionary<ElementType, IList<DataOperation>>();

        protected virtual ElementMappingMethod GetMethod(string methodDescription, ElementType fromElementsElementType, ElementType toElementsElementType)
        {
            for (int i = 0; i < methods.Count; i++)
            {
                if (fromElementsElementType == methods[i].FromElementsShapeType)
                {
                    if (toElementsElementType == methods[i].ToElementsShapeType)
                    {
                        if (methodDescription == methods[i].Description)
                            return methods[i];
                    }
                }
            }
            throw new Exception("methodDescription: " + methodDescription +
                                " not known for fromElementType: " + fromElementsElementType +
                                " and to ElementType: " + toElementsElementType);
        }

        protected virtual XYPoint CreateXYPoint(IElementSet elementSet, int index)
        {
            if (elementSet.ElementType != ElementType.XYPoint)
            {
                throw new Exception("Cannot create XYPoint");
            }

            XYPoint xyPoint = new XYPoint();
            xyPoint.X = elementSet.GetXCoordinate(index, 0);
            xyPoint.Y = elementSet.GetYCoordinate(index, 0);
            return xyPoint;
        }

        public static XYPolyline CreateXYPolyline(IElementSet elementSet, int index)
        {
            if (!(elementSet.ElementType == ElementType.XYPolyLine || elementSet.ElementType == ElementType.XYLine))
            {
                throw new Exception("Cannot create XYPolyline");
            }

            XYPolyline xyPolyline = new XYPolyline();
            for (int i = 0; i < elementSet.GetVertexCount(index); i++)
            {
                xyPolyline.Points.Add(new XYPoint(elementSet.GetXCoordinate(index, i),
                                                  elementSet.GetYCoordinate(index, i)));
            }

            return xyPolyline;
        }

        protected virtual XYPolygon CreateFromXYPolygon(IElementSet elementSet, int index)
        {
            if (elementSet.ElementType != ElementType.XYPolygon)
            {
                throw new Exception("Cannot create XYPolyline");
            }

            XYPolygon xyPolygon = new XYPolygon();

            for (int i = 0; i < elementSet.GetVertexCount(index); i++)
            {
                xyPolygon.Points.Add(new XYPoint(elementSet.GetXCoordinate(index, i),
                                                 elementSet.GetYCoordinate(index, i)));
            }

            return xyPolygon;
        }

        #region Nested type: DefaultMethodType

        public struct DefaultMethodType
        {
            #region LineToPoint enum

            public enum LineToPoint
            {
                Nearest = 1000,
                Inverse = 1001,
            }

            #endregion

            #region LineToPolygon enum

            public enum LineToPolygon
            {
                WeightedMean = 1100,
                WeightedSum = 1101,
            }

            #endregion

            #region PointToLine enum

            public enum PointToLine
            {
                Nearest = 900,
                Inverse = 901,
            }

            #endregion

            #region PointToPoint enum

            public enum PointToPoint
            {
                Nearest = 100,
                Inverse = 101
            }

            #endregion

            #region PointToPolygon enum

            public enum PointToPolygon
            {
                Mean = 300,
                Sum = 301,
            }

            #endregion

            #region PointToPolyline enum

            public enum PointToPolyline
            {
                Nearest = 200,
                Inverse = 201,
            }

            #endregion

            #region PolygonToLine enum

            public enum PolygonToLine
            {
                WeightedMean = 1200,
                WeightedSum = 1201,
            }

            #endregion

            #region PolygonToPoint enum

            public enum PolygonToPoint
            {
                Value = 600,
            }

            #endregion

            #region PolygonToPolygon enum

            public enum PolygonToPolygon
            {
                WeightedMean = 800,
                WeightedSum = 801,
            }

            #endregion

            #region PolygonToPolyline enum

            public enum PolygonToPolyline
            {
                WeightedMean = 700,
                WeightedSum = 701,
            }

            #endregion

            #region PolylineToPoint enum

            public enum PolylineToPoint
            {
                Nearest = 400,
                Inverse = 401,
            }

            #endregion

            #region PolylineToPolygon enum

            public enum PolylineToPolygon
            {
                WeightedMean = 500,
                WeightedSum = 501,
            }

            #endregion

            #region PolylineToPolyline enum

            public enum PolylineToPolyline
            {
                Value = 1300
            }

            #endregion
        }

        #endregion
    }
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
}