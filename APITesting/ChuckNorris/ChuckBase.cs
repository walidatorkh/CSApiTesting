
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace APITesting.ChuckNorris
{
    class ChuckBase
    {
        protected string url = "https://api.chucknorris.io/jokes/";
        protected RestClient client;
        protected RestRequest request;
        protected RestResponse queryResult;
        private string filePath = @"C:\Automation\result.csv";
        protected IWebDriver driver;

        //public RestRequest SendRequest(string req);
        //{
        //    return new RestRequest(req);
        //}
        public RestRequest BuildRequest(string req)
        {
            Console.WriteLine("Build Request to server: " + req);
            return new RestRequest(req);
        }

        public JObject GetObject()
        {
            return JObject.Parse(queryResult.Content);
        }

        public void WriteToFile(List<string> text)
        {
            using (TextWriter writer = File.CreateText(filePath))
            {
                string append;
                foreach (string word in text)
                {
                    //writer.WriteLine(word);
                    append = word.Contains(",") ? string.Format("\"{0}\"", word) : word;
                    writer.WriteLine(string.Format("{0}{1},", word, append));
                }
            }
        }

        public void InitBrowser(string url)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }
    }
}
