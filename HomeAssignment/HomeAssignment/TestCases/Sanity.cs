using HomeAssignment.HomeAssignment.Extensions;
using HomeAssignment.HomeAssignment.Utilities;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;


namespace HomeAssignment.HomeAssignment.TestCases
{
    [TestFixture]
    internal class Sanity : Common
    {

        [Test]

        public void Test01()
        {
            // Extract the text content, including the title, from the test-driven development section to variable
            string paragraphContent = (testdd.testDrivenDevPar.Text) + "\n" + (testdd.childTestDrivenDevPar.Text);

            // Print the found text
            Console.WriteLine("Found text: " + paragraphContent);
            Thread.Sleep(2000);

            // Identify all unique words in the text, treating characters as case-insensitive
            var uniqueWords = Actions.GetUniqueWords(paragraphContent);
            // Print all unique words
            foreach (var word in uniqueWords)
            {
                Console.WriteLine($"Print all unique words: {word}");
            }

            // Disregard periods, hyphens, commas, and other delimiters from any word
            List<string> outputList = Actions.ExcludeBracketsAndDelimiters(uniqueWords);

            // Print all words without brackets and delimiters
            foreach (var word in outputList)
            {
                Console.WriteLine($"Print all words without brackets and delimiters: {word}");
            }

            // Count the number of strings in the output list
            Actions.GetListCount(outputList);

            // Get text data from the API
            string textFromApi = Actions.GetResponseText(Common.GetData("URL_API"));

            // Identify and print all unique words in the API response
            var uniqueWordsApi = Actions.GetUniqueWords(textFromApi);
            foreach (var word in uniqueWordsApi)
            {
                Console.WriteLine($"Print all unique words: {word}");
            }

            // Exclude delimiters from unique words in the API response
            List<string> listApiNoDelimeters = Actions.ExcludeDelimiters(uniqueWordsApi);
            // Print all words without brackets and delimiters
            foreach (var word in listApiNoDelimeters)
            {
                Console.WriteLine($"Print all words without brackets and delimiters: {word}");
            }

            // Count the number of strings in the API response
            Actions.GetListCount(listApiNoDelimeters);

            //Verification of unique words identified in UI test matches the count from API test
            Verifications.VerifyEquals(listApiNoDelimeters, outputList);
        }

    }
}
