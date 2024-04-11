using System;
using System.Collections.Generic;
using NUnit.Framework;
using RestSharp;

namespace APITesting.Exersices
{
    [TestFixture]
    internal class StudentsList
    {
        string url = "http://localhost:9000";
        RestClient client;
        RestRequest request;
        RestResponse queryResult;

        [OneTimeSetUp]

        public void Init()
        {
            client = new RestClient(url);
        }

        [Test]    
        
        public void Test01_AddStudent() 
        {
            var student = new
            {
                firstName = "firstName",
                lastName = "lastName",
                email = "bah@bah.bah",
                programme = "API testing"
            };

            request = new RestRequest("/student", Method.Post);
            request.AddJsonBody(student);
            queryResult = client.Execute(request);
            Console.WriteLine(queryResult.Content);
        }


        [Test]

        public void Test02_AddStudentWithCources()
        {

            List<string> courcesList = new List<string>();
            courcesList.Add("CSharp");
            courcesList.Add("Java");
            courcesList.Add("Python");

            var student = new
            {
                firstName = "Vasya",
                lastName = "Petrov",
                email = "bla11111@bla1.bla",
                programme = "API C#",
                courses = courcesList
            };

            request = new RestRequest("/student", Method.Post);
            request.AddJsonBody(student);
            queryResult = client.Execute(request);
            Console.WriteLine(queryResult.Content);

        }
 
        [Test]

        public void Test03_UpdateStudent()
        {
            List<string> courcesList = new List<string>();
            courcesList.Add("CSharp");
            courcesList.Add("Java");
            courcesList.Add("Python");

            var student = new
            {
                firstName = "firstName100",
                lastName = "lastName100",
                email = "crap@crap.crap",
                programme = "API testing!!!",
                courses = courcesList
            };

            request = new RestRequest("/student/102", Method.Put);
            request.AddJsonBody(student);
            queryResult = client.Execute(request);
            Console.WriteLine(queryResult.Content);
        }

        [Test]

        public void Test04_DeleteStudent()
        {
            request = new RestRequest("/student/102", Method.Delete);
            queryResult = client.Execute(request);
            Console.WriteLine(queryResult.Content);
        }
    }
}
