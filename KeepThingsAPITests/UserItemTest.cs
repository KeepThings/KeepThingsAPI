using NUnit.Framework;
using KeepThingsAPI.Models;
using Newtonsoft.Json;
using System;
using NUnit.Framework.Interfaces;
using KeepThingsAPITests.Models;
using System.Net;
using System.IO;

namespace KeepThingsAPITests
{
    public class UserItemTest
    {
        TestUserItem testTestUserItem = new TestUserItem();
        private bool stop;
        UserItem testUserItem;
        HttpWebRequest httpWebRequest;
        Boolean isinit = false;
        TestData testData = new TestData();
        [SetUp]
        public void Setup()
        {
            if (!isinit)
            {
                inittestTestUserItem();
            }
            if (stop)
            {
                Assert.Inconclusive("Previous test failed");
            }
        }
        private void inittestTestUserItem()
        {
            testTestUserItem.item_name = "TestItem";
            testTestUserItem.item_desc = "This is a Test Item";
            testTestUserItem.user_id = 0;
            testTestUserItem.borrower = "TestBorrower";
            testTestUserItem.date_from = "1000-12-11";
            testTestUserItem.date_to = "1000-12-12";
            isinit = true;
        }
        private void initHttpWebRequest(String optional)
        {
            httpWebRequest = (HttpWebRequest)WebRequest.Create(testData.HttpAdress + "api/useritem/" + optional);
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
        private UserItem GetResponse()
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<UserItem>(result);
            }
        }
        [Test, Order(1)]
        public void TestUserItemPost()
        {
            initHttpWebRequest("");
            httpWebRequest.Method = "POST";           

            sendRequest(testTestUserItem);
            testUserItem = GetResponse();

            Assert.AreEqual(testTestUserItem.item_name, testUserItem.item_name);
            Assert.AreEqual(testTestUserItem.item_desc, testUserItem.item_desc);
            Assert.AreEqual(testTestUserItem.user_id, testUserItem.user_id);
            Assert.AreEqual(testTestUserItem.borrower, testUserItem.borrower);
            Assert.AreEqual(testTestUserItem.date_from, testUserItem.date_from);
            Assert.AreEqual(testTestUserItem.date_to, testUserItem.date_to);
        }
        [Test, Order(2)]
        public void TestUserItemGetWithId()
        {
            initHttpWebRequest(testUserItem.id + "");
            httpWebRequest.Method = "GET";

            var resultUserItem = GetResponse();
            
            Assert.AreEqual(testUserItem.item_name, resultUserItem.item_name);
            Assert.AreEqual(testUserItem.item_desc, resultUserItem.item_desc);
            Assert.AreEqual(testUserItem.user_id, resultUserItem.user_id);
            Assert.AreEqual(testUserItem.borrower, resultUserItem.borrower);
            Assert.AreEqual(testUserItem.date_from, resultUserItem.date_from);
            Assert.AreEqual(testUserItem.date_to, resultUserItem.date_to);
        }
        [Test, Order(3)]
        public void TestUserItemPut()
        {
            testUserItem.item_desc = "This UserItem is now changed";

            initHttpWebRequest(testUserItem.id+"");
            httpWebRequest.Method = "PUT";

            sendRequest(testUserItem);

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);
        }
        [Test, Order(4)]
        public void TestUserItemDelete()
        {
            initHttpWebRequest(testUserItem.id + "");
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