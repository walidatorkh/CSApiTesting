using APITesting.HomeAssignment.PageObjects;
using OpenQA.Selenium;
using Newtonsoft.Json.Linq;
using NUnit.Framework;


namespace HomeAssignment.HomeAssignment.Utilities
{
    internal class Base
    {
        protected static IWebDriver driver;
        protected static IWebElement element;
        protected static TestDrivenDevelopment testdd;
    }
}
