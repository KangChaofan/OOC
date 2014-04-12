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

            Assert.AreNotSame(_xiaoyun, _nobody);
            Assert.AreEqual(_xiaoyun,_nobody);
        }

        [TestMethod]
        public void ShallowCopyTestMethod()
        {
            object clone;
            SerializableUtil.ShallowCopy(_yongquan, out clone);
            _nobody = (Person)clone;

            Assert.AreEqual(_yongquan, _nobody);
            Assert.AreSame(_yongquan, _nobody);
        }

        [Serializable]
        public class Person
        {
            protected bool Equals(Person other)
            {
                return string.Equals(Name, other.Name) && Age == other.Age && Equals(Father, other.Father);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = (Name != null ? Name.GetHashCode() : 0);
                    hashCode = (hashCode*397) ^ Age;
                    hashCode = (hashCode*397) ^ (Father != null ? Father.GetHashCode() : 0);
                    return hashCode;
                }
            }

            public string Name { get; set; }
            public int Age { get; set; }
            public Person Father { get; set; }
            
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((Person) obj);
            }
        }
    }
}