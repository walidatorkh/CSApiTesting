using OpenQA.Selenium;
using System;
using RelevantCodes.ExtentReports;

namespace APITesting.HomeAssignment
{
    internal class TDDCBase
    {
        protected static IWebDriver driver;
        protected static ExtentReports extent;
        protected static ExtentTest test;
        protected static string timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
    }
}
