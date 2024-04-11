using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using HomeAssignment.HomeAssignment.Utilities;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace HomeAssignment.HomeAssignment.Extensions
{
    class Actions
    {

        string url = "https://www.mediawiki.org/w/api.php";
        RestClient client;
        RestRequest request;
        RestResponse queryResult;

        public static List<string> GetUniqueWords(string text)
        {
            try
            {
                // Split the text into words, ignoring punctuation and whitespace
                string[] words = text.Split(new char[] { ' ', ',', '.', '!', '?', ':', ';', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                // Convert words to lowercase for case-insensitive comparison
                words = words.Select(word => word.ToLower()).ToArray();

                // Use a HashSet to store unique words
                HashSet<string> uniqueWordsHashSet = new HashSet<string>(words);

                // Convert HashSet to a List of strings
                List<string> uniqueWordsList = uniqueWordsHashSet.ToList();
                return uniqueWordsList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<string>();
            }
        }

        public static List<string> ExcludeBracketsAndDelimiters(List<string> inputList)
        {
            List<string> resultList = new List<string>();

            try
            {
                foreach (string input in inputList)
                {
                    // Exclude content within brackets
                    string resultWithoutBrackets = Regex.Replace(input, @"\[[^\]]*\]", "");

                    // Remove punctuation characters from words
                    string[] words = resultWithoutBrackets.Split(new char[] { ' ', ',', '.', '!', '?', ':', ';', '=', '>', '<', '-', '(', ')', '"', '\'', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] cleanedWords = words.Select(word => new string(word.Where(c => !char.IsPunctuation(c)).ToArray())).ToArray();

                    // Concatenate cleaned words into a single string
                    string result = string.Join(" ", cleanedWords).ToLower();

                    resultList.Add(result);
                }

                return resultList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<string>();
            }
        }
        public static List<string> ExcludeDelimiters(List<string> words)
        {
            List<string> cleanWords = new List<string>();

            try
            {
                // Define the pattern to match delimiters (periods, hyphens, commas, etc.)
                //string pattern = @"[.,\-;:!?\s]+";
                string pattern = @"[^0-9a-zA-Z\s]";

                // Loop through each word in the input list
                foreach (string word in words)
                {
                    // Use Regex.Replace to remove delimiters from the word
                    string cleanWord = Regex.Replace(word, pattern, "");

                    // Add the clean word to the output list
                    cleanWords.Add(cleanWord);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while processing the words: " + ex.Message);
                // Handle the error as needed, such as logging or returning an empty list
            }

            return cleanWords;
        }

        public static int GetListCount(IList<string> elems)
        {
            Console.WriteLine($"Total strings in the output list: {elems.Count}");
            return elems.Count;
        }
        public static string GetResponseText(string url)
        {
            // Create RestClient instance
            var client = new RestClient(url);

            // Prepare request to query the API
            //var request = new RestRequest("?action=query&list=Search$srsearch=test-driven development$format=json", Method.Get);
            var request = new RestRequest("?action=query&list=search&srsearch=Test%20automation|history&utf8=&format=json", Method.Get);

            //var request = new RestRequest("?action=query&list=Search$srsearch=history$format=json", Method.Get);

            // Execute the request and get the response
            var queryResult = client.Execute(request);

            // Check if the request was successful
            if (queryResult.IsSuccessful)
            {
                // Return the response text
                return queryResult.Content;
            }
            else
            {
                // Handle the error if the request was not successful
                throw new Exception($"Error: {queryResult.StatusCode}, {queryResult.ErrorMessage}");
            }
        }

    }

}

