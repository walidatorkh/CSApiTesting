using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System;


namespace APITesting.Exersices
{
    [TestFixture]
    class Grafana
    {
        string url = "http://localhost:3001";
        RestClient client;
        RestRequest request;
        RestResponse queryResult;

        [OneTimeSetUp]

        public void Init()
        {
            client = new RestClient(url);
            //deprecated
            //client.Authenticator = new HttpBasicAuthenticator("admin", "admin");
            var options = new RestClientOptions(url)
            {
                Authenticator = new HttpBasicAuthenticator("admin", "admin")
            };
            client = new RestClient(options);
        }

        [Test] 
        public void Test01_GetAllTeams()
        {
            //get first  curl http://admin:admin@localhost:3001/api/teams/1
            request = new RestRequest("/api/teams/search?", Method.Get);
            Console.WriteLine(request);
            queryResult = client.Execute(request);
            if (queryResult.ErrorException != null)
            {
                throw queryResult.ErrorException;
            }
            Console.WriteLine(queryResult.Content);
            Console.WriteLine($"All teams: {queryResult.Content}");
        }

        [Test]
        public void Test02_AddTeam()
        {
            var team = new

            {
                name = "MyTumTumTeam",
                email = "TumTum@test.com",
                orgId = 3
            };

            request = new RestRequest("/api/teams", Method.Post);
            request.AddJsonBody(team);
            queryResult = client.Execute(request);
            //if (queryResult.ErrorException != null)
            //{
            //    throw queryResult.ErrorException;
            //}
            Console.WriteLine(queryResult.Content);
        }

        [Test]
        public void Test03_AddTeamMember()
        {
            var member = new

            {
                userId = 1
            };

            request = new RestRequest("/api/teams/3/members", Method.Post);
            request.AddJsonBody(member);
            queryResult = client.Execute(request);
            //if (queryResult.ErrorException == null)
            //{
            //    throw queryResult.ErrorException;
            //}
            Console.WriteLine(queryResult.Content);
        }

        [Test]
        public void Test04_GetUsers()
        {
            // curl http://admin:admin@localhost:3001/api/teams/1
            request = new RestRequest("/api/users", Method.Get);
            Console.WriteLine(request);
            queryResult = client.Execute(request);
            if (queryResult.ErrorException != null)
            {
                throw queryResult.ErrorException;
            }
            Console.WriteLine(queryResult.Content);
            Console.WriteLine("#################");
        }

        [Test]
        public void Test05_UpdateTeam()
        {
            var team = new

            {
                name = "MyTumTumTeam",
                email = "blablasa@test.com"
            };

            request = new RestRequest("/api/teams/3", Method.Put);
            request.AddJsonBody(team);
            queryResult = client.Execute(request);
            //if (queryResult.ErrorException != null)
            //{
            //    throw queryResult.ErrorException;
            //}
            Console.WriteLine(queryResult.Content);
        }

        [Test]
        public void Test06_VerifyTeamMembers()
        {

            request = new RestRequest("/api/teams/1/members", Method.Get);
            queryResult = client.Execute(request);
            if (queryResult.ErrorException != null)
            {
                throw queryResult.ErrorException;
            }
            Console.WriteLine(queryResult.Content);
            string loginName = (string)JArray.Parse(queryResult.Content)[0]["login"];
            Console.WriteLine(loginName);
            //deprecated
            //Assert.AreEqual("IgorKh", loginName);
            Assert.That("igork", Is.EqualTo(loginName), $"Expected {"igork"} to be {loginName}");

        }

        //[Test]
        //public void Test07_UpdateUser()
        //{
        //    //var newUser = new
        //    //{
        //    //    email = "kuku@test.com",
        //    //    name = "User2",
        //    //    login = "user2",
        //    //    theme = "light"
        //    //};
        //    var newUser = new
        //    {
        //        email = "crap@crap.com",
        //        name = "crap",
        //        login = "crap",
        //        theme = "light"
        //    };

        //    request = new RestRequest("/api/1/users", Method.Post);
        //    request.AddJsonBody(newUser);
        //    queryResult = client.Execute(request);
        //    //if (queryResult.ErrorException != null)
        //    //{
        //    //    throw queryResult.ErrorException;
        //    //}
        //    Console.WriteLine(queryResult.Content);
        //}


    }
}
