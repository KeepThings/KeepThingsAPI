using NUnit.Framework;
using KeepThingsAPI.Models;
using Newtonsoft.Json;
using System;
using NUnit.Framework.Interfaces;
using KeepThingsAPITests.Models;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace KeepThingsAPITests
{
    class UserTest
    {
        TestUser testTestUser = new TestUser();
        private bool stop;
        User testUser;
        HttpWebRequest httpWebRequest;
        Boolean isinit = false;
        TestData testData = new TestData();
        [SetUp]
        public void Setup()
        {
            if (!isinit)
            {
                inittestTestUser();
            }
            if (stop)
            {
                Assert.Inconclusive("Previous test failed");
            }
        }
        private void inittestTestUser()
        {
            testTestUser.Auth0_id = "ThisIsAAuth0Id";
            testTestUser.name = "van Testenburg";
            testTestUser.first_name = "Sir. Test";
            testTestUser.password = "ThisIsASuperPasswort";
            testTestUser.email = "sirTest@gmail.com";
            testTestUser.tel_nr = "999911119999";
            testTestUser.username = "SirSuperTest";
            testTestUser.type = "User";
            testTestUser.verified = false;
            isinit = true;
        }
        private void initHttpWebRequest(String optional)
        {
            httpWebRequest = (HttpWebRequest)WebRequest.Create(testData.HttpAdress + "api/user/" + optional);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.PreAuthenticate = true;
            httpWebRequest.Headers.Add("Authorization", "Bearer " + testData.Auth0Token);

        }
        private void initHttpWebRequestUserNameID(String optional)
        {
            httpWebRequest = (HttpWebRequest)WebRequest.Create(testData.HttpAdress + "api/usernameid/" + optional);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.PreAuthenticate = true;
            httpWebRequest.Headers.Add("Authorization", "Bearer " + testData.Auth0Token);

        }
        private void sendRequest(Object parObject)
        {
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(parObject);
                streamWriter.Write(json);
            }
        }
        private User GetResponse()
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<User>(result);
            }
        }
        private List<User> GetResponses()
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<IEnumerable<User>>(result) as List<User>;
            }
        }
        private UserNameID GetResponseUserNameID()
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<UserNameID>(result);
            }
        }
        private List<UserNameID> GetResponsesUserNameID()
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<IEnumerable<UserNameID>>(result) as List<UserNameID>;
            }
        }
        [Test, Order(1)]
        public void TestUserPost()
        {
            initHttpWebRequest("");
            httpWebRequest.Method = "POST";

            sendRequest(testTestUser);
            testUser = GetResponse();

            Assert.AreEqual(testTestUser.Auth0_id, testUser.Auth0_id);
            Assert.AreEqual(testTestUser.name, testUser.name);
            Assert.AreEqual(testTestUser.first_name, testUser.first_name);
            Assert.AreEqual(testTestUser.password, testUser.password);
            Assert.AreEqual(testTestUser.email, testUser.email);
            Assert.AreEqual(testTestUser.tel_nr, testUser.tel_nr);
            Assert.AreEqual(testTestUser.username, testUser.username);
            Assert.AreEqual(testTestUser.type, testUser.type);
            Assert.AreEqual(testTestUser.verified, testUser.verified);
        }
        [Test, Order(2)]
        public void TestUserGetWithId()
        {
            initHttpWebRequest(testUser.Auth0_id + "");
            httpWebRequest.Method = "GET";

            var resultUser = GetResponse();

            Assert.AreEqual(testUser.Auth0_id, resultUser.Auth0_id);
            Assert.AreEqual(testUser.name, resultUser.name);
            Assert.AreEqual(testUser.first_name, resultUser.first_name);
            Assert.AreEqual(testUser.password, resultUser.password);
            Assert.AreEqual(testUser.email, resultUser.email);
            Assert.AreEqual(testUser.tel_nr, resultUser.tel_nr);
            Assert.AreEqual(testUser.username, resultUser.username);
            Assert.AreEqual(testUser.type, resultUser.type);
            Assert.AreEqual(testUser.verified, resultUser.verified);
        }
        [Test, Order(3)]
        public void TestUserGet()
        {
            initHttpWebRequest("");
            httpWebRequest.Method = "GET";

            var resultUsers = GetResponses();
            Assert.NotNull(resultUsers.IndexOf(testUser));
        }
        [Test, Order(4)]
        public void TestUserPut()
        {
            testUser.username = "This User is now changed";

            initHttpWebRequest(testUser.Auth0_id + "");
            httpWebRequest.Method = "PUT";

            sendRequest(testUser);

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);
        }
        [Test, Order(5)]
        public void TestUserNameIDGetWithId()
        {
            initHttpWebRequestUserNameID(testUser.id + "");
            httpWebRequest.Method = "GET";

            var resultUser = GetResponseUserNameID();

            Assert.AreEqual(testUser.username, resultUser.username);
        }
        [Test, Order(6)]
        public void TestUserNameIDGet()
        {
            initHttpWebRequestUserNameID("");
            httpWebRequest.Method = "GET";

            var resultUsers = GetResponsesUserNameID();
            UserNameID testUserNameID = new UserNameID();
            testUserNameID.id = testUser.id;
            testUserNameID.username = testUser.username;
            Assert.NotNull(resultUsers.IndexOf(testUserNameID));
        }
        [Test, Order(7)]
        public void TestUserDelete()
        {
            initHttpWebRequest(testUser.Auth0_id + "");
            httpWebRequest.Method = "DELETE";
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);

        }
        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                stop = true;
            }
        }
    }
}
