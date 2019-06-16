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
    class MarketplaceItemTest
    {
        TestMarketplaceItem testTestMarketplaceItem = new TestMarketplaceItem();
        private bool stop;
        MarketplaceItem testMarketplaceItem;
        HttpWebRequest httpWebRequest;
        Boolean isinit = false;
        TestData testData = new TestData();
        [SetUp]
        public void Setup()
        {
            if (!isinit)
            {
                inittestTestMarketplaceItem();
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
            isinit = true;
        }
        private void initHttpWebRequest(String optional)
        {
            httpWebRequest = (HttpWebRequest)WebRequest.Create(testData.HttpAdress + "api/MarketplaceItem/" + optional);
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
        private MarketplaceItem GetResponse()
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<MarketplaceItem>(result);
            }
        }
        [Test, Order(1)]
        public void TestMarketplaceItemPost()
        {
            initHttpWebRequest("");
            httpWebRequest.Method = "POST";

            sendRequest(testTestMarketplaceItem);
            testMarketplaceItem = GetResponse();

            Assert.AreEqual(testTestMarketplaceItem.item_name, testMarketplaceItem.item_name);
            Assert.AreEqual(testTestMarketplaceItem.item_desc, testMarketplaceItem.item_desc);
            Assert.AreEqual(testTestMarketplaceItem.user_id, testMarketplaceItem.user_id);
            Assert.AreEqual(testTestMarketplaceItem.borrower, testMarketplaceItem.borrower);
            Assert.AreEqual(testTestMarketplaceItem.date_from, testMarketplaceItem.date_from);
            Assert.AreEqual(testTestMarketplaceItem.date_to, testMarketplaceItem.date_to);
        }
        [Test, Order(2)]
        public void TestMarketplaceItemGetWithId()
        {
            initHttpWebRequest(testMarketplaceItem.id + "");
            httpWebRequest.Method = "GET";

            var resultMarketplaceItem = GetResponse();

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

            initHttpWebRequest(testMarketplaceItem.id + "");
            httpWebRequest.Method = "PUT";

            sendRequest(testMarketplaceItem);

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Assert.AreEqual(HttpStatusCode.NoContent, myHttpWebResponse.StatusCode);
        }
        [Test, Order(4)]
        public void TestMarketplaceItemDelete()
        {
            initHttpWebRequest(testMarketplaceItem.id + "");
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





        //        MarketplaceItem testMarketplaceItem = new MarketplaceItem();
        //        SqlConnectionController sql = new SqlConnectionController();
        //        private bool stop;
        //        [SetUp]
        //        public void Setup()
        //        {
        //            testMarketplaceItem.item_name = "TestItem";
        //            testMarketplaceItem.item_desc = "This is a Test Item";
        //            testMarketplaceItem.user_id = 9;
        //            testMarketplaceItem.borrower = "Sir. Borrower";
        //            testMarketplaceItem.date_from = "9999-12-30";
        //            testMarketplaceItem.date_to = "9999-12-30";

        //            if (stop)
        //            {
        //                Assert.Inconclusive("Previous test failed");
        //            }
        //        }
        //        [Test, Order(1)]
        //        public void TestMarketplaceItemPost()
        //        {
        //            String resultString = sql.MarketplaceItem_postMarketplaceItem(testMarketplaceItem);
        //            MarketplaceItem resultMarketplaceItem = JsonConvert.DeserializeObject<MarketplaceItem>(resultString);
        //            Assert.AreEqual(testMarketplaceItem.item_name, resultMarketplaceItem.item_name);
        //            Assert.AreEqual(testMarketplaceItem.item_desc, resultMarketplaceItem.item_desc);
        //            Assert.AreEqual(testMarketplaceItem.user_id, resultMarketplaceItem.user_id);
        //            Assert.AreEqual(testMarketplaceItem.borrower, resultMarketplaceItem.borrower);
        //            Assert.AreEqual(testMarketplaceItem.date_from, resultMarketplaceItem.date_from);
        //            Assert.AreEqual(testMarketplaceItem.date_to, resultMarketplaceItem.date_to);
        //            testMarketplaceItem.id = resultMarketplaceItem.id;
        //        }
        //        [Test, Order(2)]
        //        public void TestMarketplaceItemGetWithId()
        //        {
        //            String resultString = sql.MarketplaceItem_getMarketplaceItem(testMarketplaceItem.id);
        //            MarketplaceItem resultMarketplaceItem = JsonConvert.DeserializeObject<MarketplaceItem>(resultString);
        //            Assert.AreEqual(testMarketplaceItem.item_name, resultMarketplaceItem.item_name);
        //            Assert.AreEqual(testMarketplaceItem.item_desc, resultMarketplaceItem.item_desc);
        //            Assert.AreEqual(testMarketplaceItem.user_id, resultMarketplaceItem.user_id);
        //            Assert.AreEqual(testMarketplaceItem.borrower, resultMarketplaceItem.borrower);
        //            Assert.AreEqual(testMarketplaceItem.date_from, resultMarketplaceItem.date_from);
        //            Assert.AreEqual(testMarketplaceItem.date_to, resultMarketplaceItem.date_to);
        //        }
        //        [Test, Order(3)]
        //        public void TestMarketplaceItemPut()
        //        {
        //            testMarketplaceItem.item_desc = "This MarketplaceItem is now changed";
        //            String resultString = sql.MarketplaceItem_putMarketplaceItem(testMarketplaceItem.id, testMarketplaceItem);
        //            MarketplaceItem resultMarketplaceItem = JsonConvert.DeserializeObject<MarketplaceItem>(resultString);
        //            Assert.AreEqual(testMarketplaceItem.item_name, resultMarketplaceItem.item_name);
        //            Assert.AreEqual(testMarketplaceItem.item_desc, resultMarketplaceItem.item_desc);
        //            Assert.AreEqual(testMarketplaceItem.user_id, resultMarketplaceItem.user_id);
        //            Assert.AreEqual(testMarketplaceItem.borrower, resultMarketplaceItem.borrower);
        //            Assert.AreEqual(testMarketplaceItem.date_from, resultMarketplaceItem.date_from);
        //            Assert.AreEqual(testMarketplaceItem.date_to, resultMarketplaceItem.date_to);
        //        }
        //        [Test, Order(4)]
        //        public void TestMarketplaceItemDelete()
        //        {
        //            String result = sql.MarketplaceItem_deleteMarketplaceItem(testMarketplaceItem.id);
        //            Assert.AreEqual("done", result);
        //        }
        //        [TearDown]
        //        public void TearDown()
        //        {
        //            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        //            {
        //                stop = true;
        //            }
        //        }


    }
}
