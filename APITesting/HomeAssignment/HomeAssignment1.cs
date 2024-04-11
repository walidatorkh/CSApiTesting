using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using APITesting.HomeAssignment;

namespace APITesting.HomeAssignment
{
    [TestFixture]

    public class HomeAssigment1
    {
        IWebDriver driver;
        //TDDPage tddp = new TDDPage();
        InitTDDPage tddp;
        WebDriverWait wait;


        [OneTimeSetUp]
        public void init()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/Test_automation");
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //PageFactory.InitElements(driver, tddp);
            tddp = new InitTDDPage(driver);

        }

        [Test]

        public void Test01_FindUniqeWords()
        {
            try
            {
                Console.WriteLine(tddp.testDrivenDevelopmentSection.Text);
                Console.WriteLine(tddp.parentDiv.Text);
                // Print the extracted text content
                string sectionContent = (tddp.testDrivenDevelopmentSection.Text) + "\n" + (tddp.parentDiv.Text);
                Console.WriteLine(sectionContent);

            }
            catch (NoSuchElementException)
            {
                // Handle case where elements are not found
                Console.WriteLine("Element not found.");
            }
        }

        [Test]
        public void Test01_FindUniqeWord()
        {
            try
            {
                // Find the title element by its ID
                IWebElement titleElement = driver.FindElement(By.Id("Test-driven_development"));

                // Find the first paragraph element following the title element
                IWebElement paragraphElement = titleElement.FindElement(By.XPath("./following::p[1]"));

                // Print the found text
                Console.WriteLine("Found text: " + paragraphElement.Text);

                // Extract text from the paragraph element
                string text = paragraphElement.Text;

                // Split the text into words and remove non-alphanumeric characters
                string[] words = Regex.Replace(text.ToLower(), @"[^\w\s]", "").Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                // Use HashSet to store unique words
                HashSet<string> uniqueWords = new HashSet<string>(words);

                // Print unique words to the console
                Console.WriteLine("Unique words in the text:");
                foreach (string word in uniqueWords)
                {
                    Console.WriteLine(word);
                }

                // Define output file path
                string outputPath = "C:\\Automation\\unique_words.txt";

                // Write unique words to a text file
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    foreach (string word in uniqueWords)
                    {
                        writer.WriteLine(word);
                    }
                }
            }
            catch (NoSuchElementException)
            {
                // Handle case where elements are not found
                Console.WriteLine("Element not found.");
            }
        }



        [Test]
        public void Test02_FindBrackets()
        {
            try
            {
                // Find the title element by its ID
                IWebElement titleElement = driver.FindElement(By.Id("Test-driven_development"));

                // Find the first paragraph element following the title element
                IWebElement paragraphElement = titleElement.FindElement(By.XPath("./following::p[1]"));

                // Print the found text
                Console.WriteLine("Found text: " + paragraphElement.Text);

                // Extract text from the paragraph element
                string text = paragraphElement.Text;

                // Remove content within brackets using regular expression
                string resultWithoutBrackets = Regex.Replace(text, @"\[[^\]]*\]", "");

                // Define output file path
                string outputPath = "C:\\Automation\\withoutBrackets.txt";

                // Write text without brackets to a text file
                File.WriteAllText(outputPath, resultWithoutBrackets);
            }
            catch (NoSuchElementException)
            {
                // Handle case where elements are not found
                Console.WriteLine("Element not found.");
            }
        }


        [Test]
        public void Test03_FindDelimiters()
        {
            try
            {
                // Find the title element by its ID
                IWebElement titleElement = driver.FindElement(By.Id("Test-driven_development"));

                // Find the first paragraph element following the title element
                IWebElement paragraphElement = titleElement.FindElement(By.XPath("./following::p[1]"));

                // Print the found text
                Console.WriteLine("Found text: " + paragraphElement.Text);

                // Extract text from the paragraph element
                string text = paragraphElement.Text;

                // Replace common delimiters with a single space using regular expression
                string resultWithoutDelimiters = Regex.Replace(text, @"[\s\.,;-]+", " ");

                // Define output file path
                string outputPath = "C:\\Automation\\withoutDelimiters.txt";

                // Write text without delimiters to a text file
                File.WriteAllText(outputPath, resultWithoutDelimiters);
            }
            catch (NoSuchElementException)
            {
                // Handle case where elements are not found
                Console.WriteLine("Element not found.");
            }
        }

        [Test]
        public void Test04_CountOccurences()
        {
            try
            {
                // Find the title element by its ID
                IWebElement titleElement = driver.FindElement(By.Id("Test-driven_development"));

                // Find the first paragraph element following the title element
                IWebElement paragraphElement = titleElement.FindElement(By.XPath("./following::p[1]"));

                // Print the found text
                Console.WriteLine("Found text: " + paragraphElement.Text);

                // Extract text from the paragraph element
                string text = paragraphElement.Text;

                // Split the text into words
                string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Count occurrences of each word
                Dictionary<string, int> wordCounts = new Dictionary<string, int>();
                foreach (string word in words)
                {
                    if (wordCounts.ContainsKey(word))
                    {
                        wordCounts[word]++;
                    }
                    else
                    {
                        wordCounts[word] = 1;
                    }
                }

                // Print word counts
                Console.WriteLine("Word Counts:");
                foreach (KeyValuePair<string, int> pair in wordCounts)
                {
                    Console.WriteLine($"{pair.Key}: {pair.Value}");
                }
            }
            catch (NoSuchElementException)
            {
                // Handle case where elements are not found
                Console.WriteLine("Element not found.");
            }
        }


        [OneTimeTearDown]
        public void UnloadDriver()
        {
            driver.Quit();
        }
    }
}