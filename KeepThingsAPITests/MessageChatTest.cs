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
    class MessageChatTest
    {
        KeepThingsAPITests.Models.TestMessage testTestMessage = new KeepThingsAPITests.Models.TestMessage();
        TestChat testTestChat = new TestChat();
        Chat testChat = new Chat();
        private bool stop;
        Message testMessage;
        HttpWebRequest httpWebRequest;
        Boolean isinit = false;
        TestData testData = new TestData();
        [SetUp]
        public void Setup()
        {
            if (!isinit)
            {
                inittestTestMessage();
            }
            if (stop)
            {
                Assert.Inconclusive("Previous test failed");
            }
        }
        private void inittestTestMessage()
        {
            testTestChat.sender_id = 0;
            testTestChat.receiver_id = 1;
            testTestChat.topic = "This is a Test-Consversation-Chat";
            testTestMessage.message = "Hello there, this is a Test-Message";
            testTestMessage.sender_id = 0;
            testTestMessage.sent_timestamp = 222222222;
            isinit = true;
        }
        private void initHttpWebRequestMessage(String optional)
        {
            httpWebRequest = (HttpWebRequest)WebRequest.Create(testData.HttpAdress + "api/message/" + optional);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.PreAuthenticate = true;
            httpWebRequest.Headers.Add("Authorization", "Bearer " + testData.Auth0Token);

        }
        private void initHttpWebRequestChat(String optional)
        {
            httpWebRequest = (HttpWebRequest)WebRequest.Create(testData.HttpAdress + "api/chat/" + optional);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.PreAuthenticate = true;
            httpWebRequest.Headers.Add("Authorization", "Bearer " + testData.Auth0Token);

        }
        private void sendRequest(Object parObjectd)
        {
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(parObjectd);
                streamWriter.Write(json);
            }
        }
        private Message GetResponseMessage()
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<Message>(result);
            }
        }
        private Chat GetResponseChat()
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<Chat>(result);
            }
        }
        private List<Chat> GetResponseChats()
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<IEnumerable<Chat>>(result) as List<Chat>;
            }
        }
        private List<Message> GetResponseMessages()
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<IEnumerable<Message>>(result) as List<Message>;
            }
        }
        [Test, Order(4)]
        public void TestMessagePost()
        {
            initHttpWebRequestMessage("");
            httpWebRequest.Method = "POST";

            sendRequest(testTestMessage);
            testMessage = GetResponseMessage();

            Assert.AreEqual(testTestMessage.message, testMessage.message);
            Assert.AreEqual(testTestMessage.sender_id, testMessage.sender_id);
            Assert.AreEqual(testTestMessage.sent_timestamp, testMessage.sent_timestamp);
        }
        [Test, Order(5)]
        public void TestMessageGetWithId()
        {
            initHttpWebRequestMessage(testMessage.chat_id + "");
            httpWebRequest.Method = "GET";

            var resultChats = GetResponseMessages();

            Assert.NotNull(resultChats.IndexOf(testMessage));
        }
        [Test, Order(6)]
        public void TestMessagePut()
        {
            testMessage.message = "Hello there, this is a Test-Message and is now changed";

            initHttpWebRequestMessage(testMessage.id + "");
            httpWebRequest.Method = "PUT";

            sendRequest(testMessage);

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);
        }
        [Test, Order(7)]
        public void TestMessageDelete()
        {
            initHttpWebRequestMessage(testMessage.id + "");
            httpWebRequest.Method = "DELETE";
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);

        }
        [Test, Order(1)]
        public void TestChatPostAsync()
        {
            initHttpWebRequestChat("");
            httpWebRequest.Method = "POST";

            sendRequest(testTestChat);

            testChat = GetResponseChat();

            Assert.AreEqual(testTestChat.sender_id, testChat.sender_id);
            Assert.AreEqual(testTestChat.receiver_id, testChat.receiver_id);
            Assert.AreEqual(testTestChat.topic, testChat.topic);
        }
        [Test, Order(2)]
        public void TestChatGetWithId()
        {
            initHttpWebRequestChat(testChat.sender_id + "");
            httpWebRequest.Method = "GET";

            var resultChats = GetResponseChats();

            Assert.NotNull(resultChats.IndexOf(testChat));
        }
        [Test, Order(3)]
        public void TestChatPut()
        {
            testChat.topic = "This is a Test-Consversation-Chat and this was changed";

            initHttpWebRequestChat(testChat.id + "");
            httpWebRequest.Method = "PUT";

            sendRequest(testChat);

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);

        }
        [Test, Order(8)]
        public void TestChatDelete()
        {
            initHttpWebRequestChat(testChat.id + "");
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
