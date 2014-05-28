using NUnit.Framework;
using Oatc.OpenMI.Sdk.Backbone;
using Oatc.OpenMI.Sdk.Spatial;

namespace Oatc.OpenMI.Sdk.DevelopmentSupport.UnitTest
{
    [TestFixture]
    public class SparseMatrixTest
    {
        [Test]
        public void CopyDenseToSparse()
        {
            var denseMatrix = new double[2, 2] { {1, 2}, {3, 4} };

            var sparseMatrix = new DoubleSparseMatrix(2, 2);

            sparseMatrix[0, 0] = denseMatrix[0, 0];
            sparseMatrix[0, 1] = denseMatrix[0, 1];
            sparseMatrix[1, 0] = denseMatrix[1, 0];
            sparseMatrix[1, 1] = denseMatrix[1, 1];

            Assert.AreEqual(1, sparseMatrix[0, 0]);
            Assert.AreEqual(2, sparseMatrix[0, 1]);
            Assert.AreEqual(3, sparseMatrix[1, 0]);
            Assert.AreEqual(4, sparseMatrix[1, 1]);
        }

        [Test]
        public void Product()
        {
            var values = new double[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };
            var sparseMatrix = new DoubleSparseMatrix(2, 3, values);

            var vector = new double[] { 2, 2, 2 };

            var result = sparseMatrix.Product(new ScalarSet(vector));

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(12, result.GetScalar(0));
            Assert.AreEqual(30, result.GetScalar(1));
        }

        [Test]
        public void DefaultValue()
        {
            var sparseMatrix = new DoubleSparseMatrix(2, 2);

            Assert.AreEqual(default(double), sparseMatrix[0, 0]);
        }
    }
}