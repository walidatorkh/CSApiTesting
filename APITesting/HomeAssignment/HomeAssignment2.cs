using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace APITesting.HomeAssignment
{
    [TestFixture]
    internal class HomeAssignment2
    {
        string url = "https://www.mediawiki.org/w/api.php";
        RestClient client;
        RestRequest request;
        RestResponse queryResult;

        [OneTimeSetUp]

        public void Init()
        {
            client = new RestClient(url);
        }

        [Test]

        public void Test01_FindOnPageUniqeWords()
        {
            try
            {
                // Prepare request to query the API
                request = new RestRequest("?action=query&list=Search$srsearch=Test-driven development$format=json", Method.Get);

                // Execute the request and get the response
                queryResult = client.Execute(request);
                Console.WriteLine(queryResult.Content);

                // Check if the request was successful
                if (queryResult.IsSuccessful)
                {
                    // Parse JSON response
                    JObject jsonResponse = JObject.Parse(queryResult.Content);

                    // Extract search results from the response
                    JToken searchResults = jsonResponse["query"]["search"];

                    // Concatenate text content from search results
                    string textContent = string.Join(" ", searchResults.Select(r => r["snippet"].ToString()));

                    // Tokenize the text content into words
                    string[] words = textContent.Split(new char[] { ' ', ',', '.', ':', ';', '-', '(', ')', '[', ']', '{', '}', '<', '>', '"', '\'', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    // Use HashSet to store unique words (case-insensitive)
                    HashSet<string> uniqueWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    foreach (string word in words)
                    {
                        uniqueWords.Add(word);
                    }

                    // Print unique words to the console
                    Console.WriteLine("Unique words in the text (case-insensitive):");
                    foreach (string uniqueWord in uniqueWords)
                    {
                        Console.WriteLine(uniqueWord);
                    }

                    // Define output file path
                    string outputPath = "C:\\Automation\\FindOnPageUniqeWords.txt";

                    // Write unique words to a text file
                    using (StreamWriter writer = new StreamWriter(outputPath))
                    {
                        foreach (string word in uniqueWords)
                        {
                            writer.WriteLine(word);
                        }
                    }
                }
                else
                {
                    // Print error message if request was not successful
                    Console.WriteLine("Error occurred: " + queryResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                // Print exception message if an error occurs
                Console.WriteLine(ex.Message);
            }
        }


        [Test]

        public void Test02_FindPageAndExcludeDelimeters()
        {
            try
            {
                // Prepare request to query the API
                request = new RestRequest("?action=query&list=search&srsearch=Test automation&format=json", Method.Get);

                // Execute the request and get the response
                queryResult = client.Execute(request);
                Console.WriteLine(queryResult.Content);

                // Check if the request was successful
                if (queryResult.IsSuccessful)
                {
                    // Parse JSON response
                    JObject jsonResponse = JObject.Parse(queryResult.Content);

                    // Extract text content from search results and concatenate
                    string textContent = string.Join(" ", jsonResponse["query"]["search"].Select(s => s["snippet"].ToString()));

                    // Define delimiters to exclude
                    char[] delimiters = { ' ', ',', '.', ':', ';', '-', '(', ')', '[', ']', '{', '}', '<', '>', '"', '\'', '\n', '\r', '\t' };

                    // Tokenize the text content into words while excluding delimiters
                    string[] words = textContent.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                    // Remove punctuation characters from words
                    string[] cleanedWords = words.Select(w => new string(w.Where(c => !char.IsPunctuation(c)).ToArray())).ToArray();

                    // Use HashSet to store unique words (case-insensitive) excluding delimiters
                    HashSet<string> uniqueWords = new HashSet<string>(cleanedWords.Where(w => !string.IsNullOrWhiteSpace(w)), StringComparer.OrdinalIgnoreCase);

                    // Print unique words to the console
                    Console.WriteLine("Unique words in the text (case-insensitive, excluding delimiters):");
                    foreach (string word in uniqueWords)
                    {
                        Console.WriteLine(word);
                    }

                    // Define output file path
                    string outputPath = "C:\\Automation\\FindPageAndExcludeDelimeters.txt";

                    // Write unique words to a text file
                    using (StreamWriter writer = new StreamWriter(outputPath))
                    {
                        foreach (string word in uniqueWords)
                        {
                            writer.WriteLine(word);
                        }
                    }
                }
                else
                {
                    // Print error message if request was not successful
                    Console.WriteLine("Error occurred: " + queryResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                // Print exception message if an error occurs
                Console.WriteLine(ex.Message);
            }
        }

        [Test]

        public void Test03_PrintOccurrences()
        {
            try
            {
                // Prepare request to query the API
                request = new RestRequest("?action=query&list=search&srsearch=Test automation&format=json", Method.Get);

                // Execute the request and get the response
                queryResult = client.Execute(request);
                Console.WriteLine(queryResult.Content);

                // Check if the request was successful
                if (queryResult.IsSuccessful)
                {
                    // Parse JSON response
                    JObject jsonResponse = JObject.Parse(queryResult.Content);

                    // Extract text content from search results and concatenate
                    string textContent = string.Join(" ", jsonResponse["query"]["search"].Select(s => s["snippet"].ToString()));

                    // Define delimiters to exclude
                    char[] delimiters = { ' ', ',', '.', ':', ';', '-', '(', ')', '[', ']', '{', '}', '<', '>', '"', '\'', '\n', '\r', '\t' };

                    // Tokenize the text content into words while excluding delimiters
                    string[] words = textContent.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                    // Count occurrences of each word
                    Dictionary<string, int> wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    foreach (string word in words)
                    {
                        // Remove punctuation characters from words
                        string cleanedWord = new string(word.Where(c => !char.IsPunctuation(c)).ToArray());
                        if (!string.IsNullOrWhiteSpace(cleanedWord))
                        {
                            if (wordCounts.ContainsKey(cleanedWord))
                            {
                                wordCounts[cleanedWord]++;
                            }
                            else
                            {
                                wordCounts[cleanedWord] = 1;
                            }
                        }
                    }

                    // Print the number of occurrences for each word
                    Console.WriteLine("Number of occurrences for each word:");
                    foreach (var kvp in wordCounts)
                    {
                        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                    }
                }
                else
                {
                    // Print error message if request was not successful
                    Console.WriteLine("Error occurred: " + queryResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                // Print exception message if an error occurs
                Console.WriteLine(ex.Message);
            }
        }


    }
}
