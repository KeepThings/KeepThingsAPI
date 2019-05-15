using NUnit.Framework;
using KeepThingsAPI.Controllers;
using KeepThingsAPI.Models;
using Newtonsoft.Json;
using System;
using NUnit.Framework.Interfaces;

namespace KeepThingsAPITests
{
    public class UserItemTest
    {
        UserItem testUserItem = new UserItem();
        SqlConnectionController sql = new SqlConnectionController();
        private bool stop;
        [SetUp]
        public void Setup()
        {
            testUserItem.item_name = "TestItem";
            testUserItem.item_desc = "This is a Test Item";
            testUserItem.user_id = 0;
            testUserItem.borrower = "TestBorrower";
            testUserItem.date_from = "9999-12-30";
            testUserItem.date_to = "9999-12-30";

            if (stop)
            {
                Assert.Inconclusive("Previous test failed");
            }
        }

        [Test, Order(1)]
        public void TestUserItemPost()
        {
            String resultString = sql.UserItem_postUserItem(testUserItem);
            UserItem resultUserItem = JsonConvert.DeserializeObject<UserItem>(resultString);
            Assert.AreEqual(testUserItem.item_name, resultUserItem.item_name);
            Assert.AreEqual(testUserItem.item_desc, resultUserItem.item_desc);
            Assert.AreEqual(testUserItem.user_id, resultUserItem.user_id);
            Assert.AreEqual(testUserItem.borrower, resultUserItem.borrower);
            Assert.AreEqual(testUserItem.date_from, resultUserItem.date_from);
            Assert.AreEqual(testUserItem.date_to, resultUserItem.date_to);
            testUserItem.id = resultUserItem.id;
        }
        [Test, Order(2)]
        public void TestUserItemGetWithId()
        {
            String resultString = sql.UserItem_getUserItem(testUserItem.id);
            UserItem resultUserItem = JsonConvert.DeserializeObject<UserItem>(resultString);
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
            String resultString = sql.UserItem_putUserItem(testUserItem.id, testUserItem);
            UserItem resultUserItem = JsonConvert.DeserializeObject<UserItem>(resultString);
            Assert.AreEqual(testUserItem.item_name, resultUserItem.item_name);
            Assert.AreEqual(testUserItem.item_desc, resultUserItem.item_desc);
            Assert.AreEqual(testUserItem.user_id, resultUserItem.user_id);
            Assert.AreEqual(testUserItem.borrower, resultUserItem.borrower);
            Assert.AreEqual(testUserItem.date_from, resultUserItem.date_from);
            Assert.AreEqual(testUserItem.date_to, resultUserItem.date_to);
        }
        [Test, Order(4)]
        public void TestUserItemDelete()
        {
            String result = sql.UserItem_deleteUserItem(testUserItem.id);
            Assert.AreEqual("done", result);
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