using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;


namespace APITesting.HomeAssignment
{
    class InitTDDPage : TDDCommon
    {
        private IWebDriver driver;


        public InitTDDPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(5)));
        }

        [FindsBy(How = How.Id, Using = "Test-driven_development")]
        public IWebElement testDrivenDevelopmentSection;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"mw-content-text\"]/div[1]/p[11]")]
        public IWebElement parentDiv;
    }
}
