using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Web.UI;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace APITesting
{
    [TestFixture]

    public class UnitTest1
    {
        [OneTimeSetUp]
        public void StartSession()
        {
            Console.WriteLine("Hei StartSession");
        }

        [Test]
        public void Test01()
        {
            int age = 99;
            Assert.That(age, Is.EqualTo(99), $"Expected {age} to be 99");
            //Assert.AreEqual(99, age, "result");
        }

        [TestCase("Google")]
        [TestCase("Bing")]
        [TestCase("Kuku")]

        public void Test02(string value)
        {
            //Assert.AreEqual("Bing", value, "result");
            ClassicAssert.AreEqual("Bing", (value), $"Expected {"Bing"} equal to {value}");
        }

        [TestCase("Google")]
        [TestCase("Bing")]
        [TestCase("Kuku")]

        public void Test03(string value)
        {
            // Assert.AreNotEqual("Bing", value, "result");
            Assert.That("Bing", Is.Not.EqualTo(value), $"Expected {"Bing"} not equal to {value}");
        }
        
    

        [Test]
        public void Test04()
        {
            int age = 99;
            //Assert.True(age > 5);
            ClassicAssert.True(age > 5);
        }

        [Test]
        public void Test05()
        {
            int age = 99;
            //deprecated
            //Assert.False(age > 5);
            //Assert.That(age < 5, Is.False);
            ClassicAssert.False(age < 5);
        }



        [OneTimeTearDown]
        public void CloseSession()
        {
            Console.WriteLine("Hei CloseSession");
        }
    }
}
