using NUnit.Framework;
using KeepThingsAPI.Controllers;
using KeepThingsAPI.Models;
using Newtonsoft.Json;
using System;
using NUnit.Framework.Interfaces;


namespace KeepThingsAPITests
{
    class MarketplaceItemTest
    {
        MarketplaceItem testMarketplaceItem = new MarketplaceItem();
        SqlConnectionController sql = new SqlConnectionController();
        private bool stop;
        [SetUp]
        public void Setup()
        {
            testMarketplaceItem.item_name = "TestItem";
            testMarketplaceItem.item_desc = "This is a Test Item";
            testMarketplaceItem.user_id = 9;
            testMarketplaceItem.borrower = "Sir. Borrower";
            testMarketplaceItem.date_from = "9999-12-30";
            testMarketplaceItem.date_to = "9999-12-30";

            if (stop)
            {
                Assert.Inconclusive("Previous test failed");
            }
        }
        [Test, Order(1)]
        public void TestMarketplaceItemPost()
        {
            String resultString = sql.MarketplaceItem_postMarketplaceItem(testMarketplaceItem);
            MarketplaceItem resultMarketplaceItem = JsonConvert.DeserializeObject<MarketplaceItem>(resultString);
            Assert.AreEqual(testMarketplaceItem.item_name, resultMarketplaceItem.item_name);
            Assert.AreEqual(testMarketplaceItem.item_desc, resultMarketplaceItem.item_desc);
            Assert.AreEqual(testMarketplaceItem.user_id, resultMarketplaceItem.user_id);
            Assert.AreEqual(testMarketplaceItem.borrower, resultMarketplaceItem.borrower);
            Assert.AreEqual(testMarketplaceItem.date_from, resultMarketplaceItem.date_from);
            Assert.AreEqual(testMarketplaceItem.date_to, resultMarketplaceItem.date_to);
            testMarketplaceItem.id = resultMarketplaceItem.id;
        }
        [Test, Order(2)]
        public void TestMarketplaceItemGetWithId()
        {
            String resultString = sql.MarketplaceItem_getMarketplaceItem(testMarketplaceItem.id);
            MarketplaceItem resultMarketplaceItem = JsonConvert.DeserializeObject<MarketplaceItem>(resultString);
            Assert.AreEqual(testMarketplaceItem.item_name, resultMarketplaceItem.item_name);
            Assert.AreEqual(testMarketplaceItem.item_desc, resultMarketplaceItem.item_desc);
            Assert.AreEqual(testMarketplaceItem.user_id, resultMarketplaceItem.user_id);
            Assert.AreEqual(testMarketplaceItem.borrower, resultMarketplaceItem.borrower);
            Assert.AreEqual(testMarketplaceItem.date_from, resultMarketplaceItem.date_from);
            Assert.AreEqual(testMarketplaceItem.date_to, resultMarketplaceItem.date_to);
        }
        [Test, Order(3)]
        public void TestMarketplaceItemPut()
        {
            testMarketplaceItem.item_desc = "This MarketplaceItem is now changed";
            String resultString = sql.MarketplaceItem_putMarketplaceItem(testMarketplaceItem.id, testMarketplaceItem);
            MarketplaceItem resultMarketplaceItem = JsonConvert.DeserializeObject<MarketplaceItem>(resultString);
            Assert.AreEqual(testMarketplaceItem.item_name, resultMarketplaceItem.item_name);
            Assert.AreEqual(testMarketplaceItem.item_desc, resultMarketplaceItem.item_desc);
            Assert.AreEqual(testMarketplaceItem.user_id, resultMarketplaceItem.user_id);
            Assert.AreEqual(testMarketplaceItem.borrower, resultMarketplaceItem.borrower);
            Assert.AreEqual(testMarketplaceItem.date_from, resultMarketplaceItem.date_from);
            Assert.AreEqual(testMarketplaceItem.date_to, resultMarketplaceItem.date_to);
        }
        [Test, Order(4)]
        public void TestMarketplaceItemDelete()
        {
            String result = sql.MarketplaceItem_deleteMarketplaceItem(testMarketplaceItem.id);
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
