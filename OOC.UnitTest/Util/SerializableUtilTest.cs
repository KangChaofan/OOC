using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOC.Util;

namespace OOC.UnitTest.Util
{
    [TestClass]
    public class SerializableUtilTest
    {
        private Person _nobody;
        private Person _xiaoyun;
        private Person _yongquan;

        [TestInitialize]
        public void Init()
        {
            _xiaoyun = new Person
                {
                    Age = 23,
                    Name = "zhangxiaoyun",
                    Father = new Person {Age = 45, Father = null, Name = "zhangdayun"},
                };

            _yongquan = new Person
                {
                    Age = 25,
                    Name = "chenyongquan",
                    Father = new Person {Age = 50, Father = null, Name = "chenbuquan"},
                };
        }

        [TestMethod]
        public void DeepCopyTestMethod()
        {
            object clone;
            SerializableUtil.DeepCopy(_xiaoyun, out clone);
            _nobody = (Person)clone;

            //TODO complete me.
            Assert.IsNotNull(_nobody);
        }

        [TestMethod]
        public void ShallowCopyTestMethod()
        {
            object clone;
            SerializableUtil.ShallowCopy(_yongquan, out clone);
            _nobody = (Person)clone;

            //TODO complete me.
            Assert.IsNotNull(_nobody);
        }

        [Serializable]
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Person Father { get; set; }
        }
    }
}