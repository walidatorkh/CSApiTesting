using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SeleniumExtras.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;
using NUnit.Framework.Legacy;

namespace APITesting.Exersices
{
    [TestFixture]

    public class OpenWeatherMap3
    {
        string url = "http://api.openweathermap.org/data/2.5/weather";
        string city = "Jerusalem,IL";
        string key = "c5cf8660dca588cd3735d8df6f8c7d9a";//igork

        RestClient client;
        RestRequest request;
        RestResponse queryResult;
        IWebDriver driver;
        ChromeOptions chromeOptions;
        //LoginPage login = new LoginPage();
        //LoginPage login;
        //HomePage home;


        [OneTimeSetUp]
        public void init()
        {
            client = new RestClient(url);
        }
        //public void StartSession()
        //{
        //    driver = new ChromeDriver();
        //    driver.Manage().Window.Maximize();
        //    driver.Navigate().GoToUrl("https://openweathermap.org/city/281184");
            // PageFactory.InitElements(driver, login);
            //login = new LoginPage(driver);
            //home = new HomePage(driver);
        //}

        [Test]
        public void Test01ValidateCountry()
        {

            //"country":"IL" "name":"Jerusalem" "humidity":47
            request = new RestRequest($"?units=metric&q=" + city + "&appid=" + key, Method.Get);
            queryResult = client.Execute(request);
            Console.WriteLine(queryResult.Content);
            string actualCountry = (string)JObject.Parse(queryResult.Content)["sys"]["country"];
            //deprecated
            //Assert.AreEqual("IL", actualCountry);
            Assert.That("IL", Is.EqualTo(actualCountry), $"Expected {"IL"} to be {actualCountry}");


        }

        [Test]
        public void Test02ValidateHumidity()
        {

            //"country":"IL" "name":"Jerusalem" "humidity":47
            request = new RestRequest($"?units=metric&q=" + city + "&appid=" + key, Method.Get);
            queryResult = client.Execute(request);
            Console.WriteLine(queryResult.Content);
            string actualHumidity = (string)JObject.Parse(queryResult.Content)["main"]["humidity"];
            //Assert.AreEqual("62", actualHumidity);
            chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://openweathermap.org/city/281184");
            System.Threading.Thread.Sleep(5000);
            //string humidityFromWeb = "";
            //Console.WriteLine(driver.FindElement(By.ClassName("//span[normalize-space()='Humidity:']")));
            //Console.WriteLine(driver.FindElement(By.XPath("(//span[normalize-space()='Humidity:'])[1]")).GetAttribute("value"));
            //Console.WriteLine(driver.FindElement(By.XPath("//span[normalize-space()='Humidity:']")).GetAttribute("value"));
            String humidityFromWeb = driver.FindElement(By.XPath("//ul[contains(@class,'weather-items')]//span[contains(.,'Humidity')]")).Text;
            //String humidityFromWeb = driver.FindElement(By.XPath("(//span[normalize-space()='Humidity:'])[1]")).Text;
            //Console.WriteLine(driver.FindElement(By.Id("//*[@id='kak']")));
            //Console.WriteLine(driver.FindElement(By.CssSelector("div[class='grid-container grid-4-5'] li:nth-child(3) span:nth-child(1)")));
            //Console.WriteLine(driver.FindElement(By.XPath("//*[contains(@class,'symbol') and text()='Humidity:']")));
            //String humidityFromWeb = driver.FindElement(By.XPath("//ul[contains(@class,'weather-items')]//span[contains(.,'Humidity')]")).Text;
            Console.WriteLine("actualHumidity = " + actualHumidity);
            Console.WriteLine("humidityFromWeb = " + humidityFromWeb);
            //Assert.AreEqual(humidityFromWeb, actualHumidity);
            

        }


        [OneTimeTearDown]
        public void UnloadDriver()
        {
            driver.Quit();
        }
    }
}
