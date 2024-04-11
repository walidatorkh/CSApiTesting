using HomeAssignment.HomeAssignment.Utilities;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;

namespace APITesting.HomeAssignment.PageObjects
{
    class TestDrivenDevelopment : Common
    {

        public TestDrivenDevelopment()
        {
            //PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(5)));
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(Convert.ToDouble(GetData("TIME_OUT")))));
        }

        [FindsBy(How = How.Id, Using = "Test-driven_development")]
        public IWebElement testDrivenDevPar;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"mw-content-text\"]/div[1]/p[11]")]
        public IWebElement childTestDrivenDevPar;
    }
}
