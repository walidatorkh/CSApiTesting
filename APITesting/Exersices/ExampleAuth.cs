using System;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;

namespace APITesting.Exersices
{
    [TestFixture]
    class ExampleAuth
    {
      string url = "http://example.com";
      RestClient client;
      RestRequest request;
      RestResponse queryResult;

        [OneTimeSetUp]

        public void Init()
        {
            // deprecated
            //client = new RestClient(url);
            //client.Authenticator = new HttpBasicAuthenticator("userName", "password");
            var options = new RestClientOptions(url)
            {
                Authenticator = new HttpBasicAuthenticator("username", "password")
            };
            client = new RestClient(options);  
        }

        [Test]
        public void Test01()
        {
            request = new RestRequest("/resource1", Method.Get);
            queryResult = client.Execute(request);
            Console.WriteLine(queryResult.Content);
        }
    }
}
