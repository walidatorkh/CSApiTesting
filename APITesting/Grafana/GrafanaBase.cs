using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITesting.Grafana
{
    class GrafanaBase
    {
        protected string url = "http://localhost:3001";
        protected RestClient client;
        protected RestRequest request;
        protected RestResponse queryResult;
        private string filePath = @"C:\Automation\RestSharp\resultGrafana.csv";


        public RestRequest BuildRequest(string req)
        {
            Console.WriteLine("Build Request to server: " + req);
            return new RestRequest(req);
        }

        public void WriteToFile(List<string> text)
        {
            using (TextWriter writer = File.CreateText(filePath))
            {
                string append;
                foreach (string word in text)
                {
                    //writer.WriteLine(word);
                    append = word.Contains(",") ? string.Format("\"{0}\"", word) : word;
                    writer.WriteLine(string.Format("{0}{1},", word, append));
                }
            }
        }

        public JObject GetObject()
        {
            return JObject.Parse(queryResult.Content);
        }
    }
}