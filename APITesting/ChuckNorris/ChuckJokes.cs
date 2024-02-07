using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using NUnit;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using OpenQA.Selenium;

namespace APITesting.ChuckNorris
{
    [TestFixture]
    class ChuckJokes : ChuckBase
    {

        [OneTimeSetUp]

        public void Init()
        {
            client = new RestClient(url);
        }

        [Test]
        public void Test01_PrintCategories()
        {
            request = BuildRequest("categories");
            queryResult = client.Execute(request);
            Console.WriteLine(queryResult.Content);
        }

        [Test]
        public void Test02_BarrackOrCharlie()
        {
            queryResult = client.Execute(BuildRequest("search?query=Barack Obama"));
            var barackCount = GetObject().Value<JArray>("result").Count();

            queryResult = client.Execute(BuildRequest("search?query=Charlie Sheen"));
            var charlieCount = GetObject().Value<JArray>("result").Count();

            if (barackCount > charlieCount)
            {
                Console.WriteLine("Barack Obama (" + barackCount + ") has more jokes than Charlie Sheen (" + charlieCount + ")");
            }
            else if (barackCount < charlieCount)
            {
                Console.WriteLine("Barack Obama (" + barackCount + ") has less jokes than Charlie Sheen (" + charlieCount + ")");
            }
            else
            {
                Console.WriteLine("They both have same amount of jokes : " + barackCount);
            }
        }

        [Test]
        public void Test03_InsertRandomJokesToCSV()
        {
            List<string> randomJokes = new List<string>();
            string url, value;

            for (int i = 0; i < 10; i++)
            {
                
                queryResult = client.Execute(BuildRequest("random"));
                url = (string)GetObject()["url"];
                value = (string)GetObject()["value"];
                randomJokes.Add(url + "," + value);
                Console.WriteLine(url + "," + value);
            }
            WriteToFile(randomJokes);
        }

        [Test]
        public void Test04_ChuckWithSelenium()
        {
            //API
            string url, valueAPI, valueWEB;
            queryResult = client.Execute(BuildRequest("random?category=movie"));
            Console.WriteLine($"queryResult = {queryResult}");
            url = (string)GetObject()["url"];
            Console.WriteLine($"url = {url}");
            valueAPI = (string)GetObject()["value"];
            Console.WriteLine($"valueAPI = {valueAPI}");

            try
            {
                //WEB
                InitBrowser(url);
                valueWEB = driver.FindElement(By.Id("joke_value")).Text;
                //deprecated
                //Assert.AreEqual(valueAPI, valueWEB);
                Assert.That(valueAPI, Is.EqualTo(valueWEB), $"Expected {valueAPI} will be equal to {valueWEB}");
                Console.WriteLine("Test passed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test failed: " + ex.Message);
                Assert.Fail("Test failed: " + ex.Message);
            }
            finally
            {
                driver.Quit();
            }
        }
    } 
}

