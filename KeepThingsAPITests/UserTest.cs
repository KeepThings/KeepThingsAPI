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
        TestUserItem testTestUserItem = new TestUserItem();
        TestMarketplaceItem testTestMarketplaceItem = new TestMarketplaceItem();
        KeepThingsAPITests.Models.TestMessage testTestMessage = new KeepThingsAPITests.Models.TestMessage();
        TestChat testTestChat = new TestChat();
        Chat testChat = new Chat();
        private bool stop;
        User testUser;
        UserItem testUserItem;
        MarketplaceItem testMarketplaceItem;
        Message testMessage;
        HttpWebRequest httpWebRequest;
        Boolean isinit = false;
        TestData testData = new TestData();
        [SetUp]
        public void Setup()
        {
            if (!isinit)
            {
                inittestTestUser();
                inittestTestUserItem();
                inittestTestMarketplaceItem();
                inittestTestMessage();
                isinit = true;
            }
            if (stop)
            {
                Assert.Inconclusive("Previous test failed");
            }
        }
        private void inittestTestMarketplaceItem()
        {
            testTestMarketplaceItem.item_name = "TestItem";
            testTestMarketplaceItem.item_desc = "This is a Test Item";
            testTestMarketplaceItem.user_id = 0;
            testTestMarketplaceItem.borrower = "TestBorrower";
            testTestMarketplaceItem.date_from = "1000-12-11";
            testTestMarketplaceItem.date_to = "1000-12-12";
        }
        private void inittestTestMessage()
        {
            testTestChat.sender_id = 0;
            testTestChat.receiver_id = 0;
            testTestChat.topic = "This is a Test-Consversation-Chat";
            testTestMessage.message = "Hello there, this is a Test-Message";
            testTestMessage.sender_id = 0;
            testTestMessage.sent_timestamp = 222222222;
        }
        private void inittestTestUserItem()
        {
            testTestUserItem.item_name = "TestItem";
            testTestUserItem.item_desc = "This is a Test Item";
            testTestUserItem.user_id = 0;
            testTestUserItem.borrower = "TestBorrower";
            testTestUserItem.date_from = "1000-12-11";
            testTestUserItem.date_to = "1000-12-12";
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
        }
        private void initHttpWebRequest(String optional)
        {
            httpWebRequest = (HttpWebRequest)WebRequest.Create(testData.HttpAdress + "api/" + optional);
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
        private String GetResponse()
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }
        [Test, Order(1)]
        public void TestUserPost()
        {
            initHttpWebRequest("user/");
            httpWebRequest.Method = "POST";

            sendRequest(testTestUser);
            testUser = JsonConvert.DeserializeObject<User>(GetResponse());

            Assert.AreEqual(testTestUser.Auth0_id, testUser.Auth0_id);
            Assert.AreEqual(testTestUser.name, testUser.name);
            Assert.AreEqual(testTestUser.first_name, testUser.first_name);
            Assert.AreEqual(testTestUser.password, testUser.password);
            Assert.AreEqual(testTestUser.email, testUser.email);
            Assert.AreEqual(testTestUser.tel_nr, testUser.tel_nr);
            Assert.AreEqual(testTestUser.username, testUser.username);
            Assert.AreEqual(testTestUser.type, testUser.type);
            Assert.AreEqual(testTestUser.verified, testUser.verified);

            testTestMarketplaceItem.user_id = testUser.id;
            testTestChat.sender_id = testUser.id;
            testTestChat.receiver_id = testUser.id;
            testTestMessage.sender_id = testUser.id;
            testTestUserItem.user_id = testUser.id;

        }
        [Test, Order(2)]
        public void TestUserGetWithId()
        {
            initHttpWebRequest("user/"+testUser.Auth0_id);
            httpWebRequest.Method = "GET";

            var resultUser = JsonConvert.DeserializeObject<User>(GetResponse());

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
            initHttpWebRequest("user/");
            httpWebRequest.Method = "GET";

            var resultUsers = JsonConvert.DeserializeObject<IEnumerable<User>>(GetResponse()) as List<User>;
            Assert.NotNull(resultUsers.IndexOf(testUser));
        }
        [Test, Order(4)]
        public void TestUserPut()
        {
            testUser.username = "This User is now changed";

            initHttpWebRequest("user/"+ testUser.Auth0_id);
            httpWebRequest.Method = "PUT";

            sendRequest(testUser);

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);
        }
        [Test, Order(5)]
        public void TestUserNameIDGetWithId()
        {
            initHttpWebRequest("usernameid/" + testUser.id);
            httpWebRequest.Method = "GET";

            var resultUser = JsonConvert.DeserializeObject<UserNameID>(GetResponse());

            Assert.AreEqual(testUser.username, resultUser.username);
        }
        [Test, Order(6)]
        public void TestUserNameIDGet()
        {
            initHttpWebRequest("usernameid/");
            httpWebRequest.Method = "GET";

            var resultUsers = JsonConvert.DeserializeObject<IEnumerable<UserNameID>>(GetResponse()) as List<UserNameID>;
            UserNameID testUserNameID = new UserNameID();
            testUserNameID.id = testUser.id;
            testUserNameID.username = testUser.username;
            Assert.NotNull(resultUsers.IndexOf(testUserNameID));
        }
        [Test, Order(7)]
        public void TestUserItemPost()
        {
            initHttpWebRequest("useritem/");
            httpWebRequest.Method = "POST";

            sendRequest(testTestUserItem);
            testUserItem = JsonConvert.DeserializeObject<UserItem>(GetResponse());

            Assert.AreEqual(testTestUserItem.item_name, testUserItem.item_name);
            Assert.AreEqual(testTestUserItem.item_desc, testUserItem.item_desc);
            Assert.AreEqual(testTestUserItem.user_id, testUserItem.user_id);
            Assert.AreEqual(testTestUserItem.borrower, testUserItem.borrower);
            Assert.AreEqual(testTestUserItem.date_from, testUserItem.date_from);
            Assert.AreEqual(testTestUserItem.date_to, testUserItem.date_to);
        }
        [Test, Order(8)]
        public void TestUserItemGetWithId()
        {
            initHttpWebRequest("useritem/" + testUserItem.id);
            httpWebRequest.Method = "GET";

            var resultUserItem = JsonConvert.DeserializeObject<UserItem>(GetResponse());

            Assert.AreEqual(testUserItem.item_name, resultUserItem.item_name);
            Assert.AreEqual(testUserItem.item_desc, resultUserItem.item_desc);
            Assert.AreEqual(testUserItem.user_id, resultUserItem.user_id);
            Assert.AreEqual(testUserItem.borrower, resultUserItem.borrower);
            Assert.AreEqual(testUserItem.date_from, resultUserItem.date_from);
            Assert.AreEqual(testUserItem.date_to, resultUserItem.date_to);
        }
        [Test, Order(9)]
        public void TestUserItemPut()
        {
            testUserItem.item_desc = "This UserItem is now changed";

            initHttpWebRequest("useritem/" + testUserItem.id);
            httpWebRequest.Method = "PUT";

            sendRequest(testUserItem);

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);
        }
        [Test, Order(10)]
        public void TestUserItemDelete()
        {
            initHttpWebRequest("useritem/" + testUserItem.id);
            httpWebRequest.Method = "DELETE";
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);
        }
        [Test, Order(11)]
        public void TestMarketplaceItemPost()
        {
            initHttpWebRequest("marketplaceitem/");
            httpWebRequest.Method = "POST";

            sendRequest(testTestMarketplaceItem);
            testMarketplaceItem = JsonConvert.DeserializeObject<MarketplaceItem>(GetResponse());

            Assert.AreEqual(testTestMarketplaceItem.item_name, testMarketplaceItem.item_name);
            Assert.AreEqual(testTestMarketplaceItem.item_desc, testMarketplaceItem.item_desc);
            Assert.AreEqual(testTestMarketplaceItem.user_id, testMarketplaceItem.user_id);
            Assert.AreEqual(testTestMarketplaceItem.borrower, testMarketplaceItem.borrower);
            Assert.AreEqual(testTestMarketplaceItem.date_from, testMarketplaceItem.date_from);
            Assert.AreEqual(testTestMarketplaceItem.date_to, testMarketplaceItem.date_to);
        }
        [Test, Order(12)]
        public void TestMarketplaceItemGetWithId()
        {
            initHttpWebRequest("marketplaceitem/" + testMarketplaceItem.id);
            httpWebRequest.Method = "GET";

            var resultMarketplaceItem = JsonConvert.DeserializeObject<MarketplaceItem>(GetResponse());

            Assert.AreEqual(testMarketplaceItem.item_name, resultMarketplaceItem.item_name);
            Assert.AreEqual(testMarketplaceItem.item_desc, resultMarketplaceItem.item_desc);
            Assert.AreEqual(testMarketplaceItem.user_id, resultMarketplaceItem.user_id);
            Assert.AreEqual(testMarketplaceItem.borrower, resultMarketplaceItem.borrower);
            Assert.AreEqual(testMarketplaceItem.date_from, resultMarketplaceItem.date_from);
            Assert.AreEqual(testMarketplaceItem.date_to, resultMarketplaceItem.date_to);
        }
        [Test, Order(13)]
        public void TestMarketplaceItemPut()
        {
            testMarketplaceItem.item_desc = "This MarketplaceItem is now changed";

            initHttpWebRequest("marketplaceitem/" + testMarketplaceItem.id);
            httpWebRequest.Method = "PUT";

            sendRequest(testMarketplaceItem);

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);
        }
        [Test, Order(14)]
        public void TestMarketplaceItemDelete()
        {
            initHttpWebRequest("marketplaceitem/" + testMarketplaceItem.id);
            httpWebRequest.Method = "DELETE";
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);

        }
        [Test, Order(15)]
        public void TestChatPost()
        {
            initHttpWebRequest("chat/");
            httpWebRequest.Method = "POST";

            sendRequest(testTestChat);

            testChat = JsonConvert.DeserializeObject<Chat>(GetResponse());

            Assert.AreEqual(testTestChat.sender_id, testChat.sender_id);
            Assert.AreEqual(testTestChat.receiver_id, testChat.receiver_id);
            Assert.AreEqual(testTestChat.topic, testChat.topic);
            testTestMessage.chat_id = testChat.id;
        }
        [Test, Order(16)]
        public void TestChatGetWithId()
        {
            initHttpWebRequest("chat/" + testChat.sender_id);
            httpWebRequest.Method = "GET";

            var resultChats = JsonConvert.DeserializeObject<IEnumerable<Chat>>(GetResponse()) as List<Chat>;

            Assert.NotNull(resultChats.IndexOf(testChat));
        }
        [Test, Order(17)]
        public void TestChatPut()
        {
            testChat.topic = "This is a Test-Consversation-Chat and this was changed";

            initHttpWebRequest("chat/" + testChat.id);
            httpWebRequest.Method = "PUT";

            sendRequest(testChat);

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);

        }
        [Test, Order(18)]
        public void TestMessagePost()
        {
            initHttpWebRequest("message/");
            httpWebRequest.Method = "POST";

            sendRequest(testTestMessage);
            testMessage = JsonConvert.DeserializeObject<Message>(GetResponse());

            Assert.AreEqual(testTestMessage.message, testMessage.message);
            Assert.AreEqual(testTestMessage.sender_id, testMessage.sender_id);
            Assert.AreEqual(testTestMessage.sent_timestamp, testMessage.sent_timestamp);
        }
        [Test, Order(19)]
        public void TestMessageGetWithId()
        {
            initHttpWebRequest("message/" + testMessage.chat_id);
            httpWebRequest.Method = "GET";

            var resultChats = JsonConvert.DeserializeObject<IEnumerable<Message>>(GetResponse()) as List<Message>;

            Assert.NotNull(resultChats.IndexOf(testMessage));
        }
        [Test, Order(20)]
        public void TestMessagePut()
        {
            testMessage.message = "Hello there, this is a Test-Message and is now changed";

            initHttpWebRequest("message/" + testMessage.id);
            httpWebRequest.Method = "PUT";

            sendRequest(testMessage);

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);
        }
        [Test, Order(21)]
        public void TestMessageDelete()
        {
            initHttpWebRequest("message/" + testMessage.id);
            httpWebRequest.Method = "DELETE";
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);

        }
        [Test, Order(22)]
        public void TestChatDelete()
        {
            initHttpWebRequest("chat/" + testChat.id);
            httpWebRequest.Method = "DELETE";
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);

        }
        [Test, Order(23)]
        public void TestUserDelete()
        {
            initHttpWebRequest("user/" + testUser.Auth0_id);
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
