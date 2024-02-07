using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using RestSharp;

namespace APITesting.Exersices
{
    [TestFixture]

    public class OpenWeatherMap
    {
        string url = "http://api.openweathermap.org/data/2.5/weather";
        string city = "Jerusalem,IL";
        string key = "c5cf8660dca588cd3735d8df6f8c7d9a";//igork

        RestClient client;
        RestRequest request;
        RestResponse queryResult;
        

        [OneTimeSetUp]
        public void init()
        {
            client = new RestClient(url);
        }

        [Test]
        public void Test01()
        {
            request = new RestRequest($"?units=metric&q=" + city + "&appid=" + key, Method.Get);
            queryResult = client.Execute(request);
            Console.WriteLine(queryResult.Content);
            //deprecated
            //Assert.True(queryResult.Content.Contains("Jerusalem"));
            ClassicAssert.True(queryResult.Content.Contains("Jerusalem"));
            string actualLon = (string)JObject.Parse(queryResult.Content)["coord"]["lon"];
            //deprecated
            //Assert.AreEqual("35.2163", actualLon);
            Assert.That("35.2163", Is.EqualTo(actualLon), $"Expected {"35.2163"} Equal To actualLon {actualLon}");

        }

        [Test]
        public void Test02()
        {
            request = new RestRequest($"?units=metric&q=" + city + "&appid=" + key, Method.Get);
            queryResult = client.Execute(request);
            Console.WriteLine(queryResult.Content);
            //deprecated
            //Assert.True(queryResult.Content.StartsWith("{\"coord\":"));
            ClassicAssert.True(queryResult.Content.StartsWith("{\"coord\":"));
        }

        [Test]
        public void Test03()
        {
            request = new RestRequest($"?units=metric&q=" + city + "&appid=" + key, Method.Get);
            queryResult = client.Execute(request);
            //Console.WriteLine(queryResult.Content);
            Console.WriteLine("1");
            //Console.WriteLine(queryResult.Content.EndsWith("\Rishon LeZiyyon\"));
            //TestContext.Out.WriteLine(queryResult.Content.ElementAt(1));
            Console.WriteLine("2");

            TestContext.Out.WriteLine(queryResult.Content.EndsWith("*200\"}]"));
            //Assert.True(queryResult.Content.EndsWith("200\"]}]}]}]}"));
        }


        //[OneTimeTearDown]
        //public void CloseSession()
        //{
        //    Console.WriteLine("Hei CloseSession");
        //}
    }
}
