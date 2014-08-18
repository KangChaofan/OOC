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
using System.Diagnostics;
using NUnit.Framework;
using OpenMI.Standard;
using Oatc.OpenMI.Sdk.Backbone;

namespace Oatc.OpenMI.Sdk.Backbone.UnitTest
{
	[TestFixture]
	public class ScalarSetTest
	{
		ScalarSet scalarSet;

		[SetUp]
		public void Init()
		{
			double[] values = {1.0,2.0,3.0};
			scalarSet = new ScalarSet(values);
		}

		[Test]
		public void Constructor()
		{
			ScalarSet scalarSet2 = new ScalarSet(scalarSet);
			Assert.AreEqual(scalarSet,scalarSet2);
		}

		[Test]
		public void GetScalar()
		{
			Assert.AreEqual(1.0,scalarSet.GetScalar(0));
			Assert.AreEqual(2.0,scalarSet.GetScalar(1));
			Assert.AreEqual(3.0,scalarSet.GetScalar(2));
		}

		[Test]
		public void Data()
		{
			Assert.AreEqual(1.0,scalarSet.data[0]);
			Assert.AreEqual(2.0,scalarSet.data[1]);
			Assert.AreEqual(3.0,scalarSet.data[2]);
		}

		[Test]
		public void Count()
		{
			Assert.AreEqual(3,scalarSet.Count);
		}

		[Test]
		public void Equals()
		{
			double[] values={1.0,2.0,3.0};
			ScalarSet scalarSet2 = new ScalarSet(values);
			Assert.IsTrue(scalarSet.Equals(scalarSet2));
			scalarSet2.data[1] = 2.5;
			Assert.IsFalse(scalarSet.Equals(scalarSet2));
		}

        [Test]
        public void CopyConstructorSpeed()
        {
            var random = new Random();

            var values = new double[5000000];
            for (var i = 0; i < 5000000; i++)
            {
                values[i] = random.Next();
            }

            var scalarSet = new ScalarSet(values);

            // copying values
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            values.Clone();
            stopwatch.Stop();

            var copyArrayTime = stopwatch.ElapsedMilliseconds;
            Trace.WriteLine("Copying array with 1M values took: " + copyArrayTime + " ms");

            stopwatch.Reset();
            stopwatch.Start();
            new ScalarSet(scalarSet);
            stopwatch.Stop();

            Trace.WriteLine("Copying scalar set with 1M values took: " + stopwatch.ElapsedMilliseconds + " ms");

            var fraction = stopwatch.ElapsedMilliseconds/copyArrayTime;

            Assert.IsTrue(fraction < 1.1);
        }
    }
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
using System.Diagnostics;
using NUnit.Framework;
using OpenMI.Standard;
using Oatc.OpenMI.Sdk.Backbone;

namespace Oatc.OpenMI.Sdk.Backbone.UnitTest
{
	[TestFixture]
	public class ScalarSetTest
	{
		ScalarSet scalarSet;

		[SetUp]
		public void Init()
		{
			double[] values = {1.0,2.0,3.0};
			scalarSet = new ScalarSet(values);
		}

		[Test]
		public void Constructor()
		{
			ScalarSet scalarSet2 = new ScalarSet(scalarSet);
			Assert.AreEqual(scalarSet,scalarSet2);
		}

		[Test]
		public void GetScalar()
		{
			Assert.AreEqual(1.0,scalarSet.GetScalar(0));
			Assert.AreEqual(2.0,scalarSet.GetScalar(1));
			Assert.AreEqual(3.0,scalarSet.GetScalar(2));
		}

		[Test]
		public void Data()
		{
			Assert.AreEqual(1.0,scalarSet.data[0]);
			Assert.AreEqual(2.0,scalarSet.data[1]);
			Assert.AreEqual(3.0,scalarSet.data[2]);
		}

		[Test]
		public void Count()
		{
			Assert.AreEqual(3,scalarSet.Count);
		}

		[Test]
		public void Equals()
		{
			double[] values={1.0,2.0,3.0};
			ScalarSet scalarSet2 = new ScalarSet(values);
			Assert.IsTrue(scalarSet.Equals(scalarSet2));
			scalarSet2.data[1] = 2.5;
			Assert.IsFalse(scalarSet.Equals(scalarSet2));
		}

        [Test]
        public void CopyConstructorSpeed()
        {
            var random = new Random();

            var values = new double[5000000];
            for (var i = 0; i < 5000000; i++)
            {
                values[i] = random.Next();
            }

            var scalarSet = new ScalarSet(values);

            // copying values
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            values.Clone();
            stopwatch.Stop();

            var copyArrayTime = stopwatch.ElapsedMilliseconds;
            Trace.WriteLine("Copying array with 1M values took: " + copyArrayTime + " ms");

            stopwatch.Reset();
            stopwatch.Start();
            new ScalarSet(scalarSet);
            stopwatch.Stop();

            Trace.WriteLine("Copying scalar set with 1M values took: " + stopwatch.ElapsedMilliseconds + " ms");

            var fraction = stopwatch.ElapsedMilliseconds/copyArrayTime;

            Assert.IsTrue(fraction < 1.1);
        }
    }
}
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
