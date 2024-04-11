using NUnit.Framework.Internal.Execution;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Xml;
using RestSharp;

namespace HomeAssignment.HomeAssignment.Utilities
{
    internal class Common : Base
    {


        public static string GetData(string nodeName)
        {
            using (XmlReader reader = XmlReader.Create(@"C:\Automation\CSApiTesting\TestAutomation\HomeAssignment\HomeAssignment\Configuration\data.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.Name.ToString().Equals(nodeName))
                            return reader.ReadString();
                    }
                }
            }
            return "NULL";
        }

        public static void InitBrowser(string browserType)
        {
            switch (browserType.ToLower())
            {
                case "chrome":
                    {
                        driver = new ChromeDriver();
                        break;
                    }
                case "firefox":
                    {
                        driver = new FirefoxDriver();
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Browser not supported");
                    }
            }
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(GetData("URL"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Convert.ToDouble(GetData("TIME_OUT")));
        }

        [OneTimeSetUp]
        public void LoadDriver()
        {
            InitBrowser(GetData("BROWSER_TYPE"));
            ManagePages.InitElements();
        }

        [OneTimeTearDown]
        public void UnloadDriver()
        {
            driver.Quit();
        }
    }
}

