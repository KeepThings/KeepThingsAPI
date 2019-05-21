using NUnit.Framework;
using KeepThingsAPI.Controllers;
using KeepThingsAPI.Models;
using Newtonsoft.Json;
using System;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;

namespace KeepThingsAPITests
{
    class MessageChatTest
    {
        Message testMessage = new Message();
        Chat testChat = new Chat();
        SqlConnectionController sql = new SqlConnectionController();
        private bool stop;
        [SetUp]
        public void Setup()
        {
            testChat.sender_id = 0;
            testChat.receiver_id = 1;
            testChat.topic = "This is a Test-Consversation-Chat";
            testMessage.message = "Hello there, this is a Test-Message";
            testMessage.sender_id = 0;
            testMessage.timestamp = 123456789;

            if (stop)
            {
                Assert.Inconclusive("Previous test failed");
            }
        }
        #region ChatTest
        [Test, Order(1)]
        public void TestChatPost()
        {
            String resultString = sql.Chat_postChat(testChat);
            Chat resultChat = JsonConvert.DeserializeObject<Chat>(resultString);
            Assert.AreEqual(testChat.sender_id, resultChat.sender_id);
            Assert.AreEqual(testChat.receiver_id, resultChat.receiver_id);
            Assert.AreEqual(testChat.topic, resultChat.topic);
            testChat.id = resultChat.id;
            testMessage.chat_id = testChat.id;
        }
        [Test, Order(2)]
        public void TestChatGetWithId()
        {
            String resultString = sql.Chat_getChats(testChat.sender_id);
            List<Chat> chats = JsonConvert.DeserializeObject<IEnumerable<Chat>>(resultString) as List<Chat>;
            Assert.NotNull(chats.IndexOf(testChat));
        }
        [Test, Order(3)]
        public void TestChatPut()
        {
            testChat.topic = "This is a Test-Consversation-Chat and this was changed";
            String resultString = sql.Chat_putChat(testChat.id, testChat);
            Chat resultChat = JsonConvert.DeserializeObject<Chat>(resultString);
            Assert.AreEqual(testChat.sender_id, resultChat.sender_id);
            Assert.AreEqual(testChat.receiver_id, resultChat.receiver_id);
            Assert.AreEqual(testChat.topic, resultChat.topic);
        }
        [Test, Order(8)]
        public void TestChatDelete()
        {
            String result = sql.Chat_deleteChat(testChat.id);
            Assert.AreEqual("done", result);
        }
        #endregion
        #region MessageTest
        [Test, Order(4)]
        public void TestMessagePost()
        {
            String resultString = sql.Message_postMessage(testMessage);
            Message resultMessage = JsonConvert.DeserializeObject<Message>(resultString);
            Assert.AreEqual(testMessage.message, resultMessage.message);
            Assert.AreEqual(testMessage.sender_id, resultMessage.sender_id);
            Assert.AreEqual(testMessage.timestamp, resultMessage.timestamp);
            testMessage.id = resultMessage.id;
        }
        [Test, Order(5)]
        public void TestMessageGetWithId()
        {
            String resultString = sql.Message_getMessages(testMessage.chat_id);
            List<Message> chats = JsonConvert.DeserializeObject<IEnumerable<Message>>(resultString) as List<Message>;
            Assert.NotNull(chats.IndexOf(testMessage));
        }
        [Test, Order(6)]
        public void TestMessagePut()
        {
            testMessage.message = "Hello there, this is a Test-Message and is now changed";
            String resultString = sql.Message_putMessage(testMessage.id, testMessage);
            Message resultMessage = JsonConvert.DeserializeObject<Message>(resultString);
            Assert.AreEqual(testMessage.message, resultMessage.message);
            Assert.AreEqual(testMessage.sender_id, resultMessage.sender_id);
            Assert.AreEqual(testMessage.timestamp, resultMessage.timestamp);
        }
        [Test, Order(7)]
        public void TestMessageDelete()
        {
            String result = sql.Message_deleteMessage(testMessage.id);
            Assert.AreEqual("done", result);
        }
        #endregion
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
