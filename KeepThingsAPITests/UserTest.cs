using NUnit.Framework;
using KeepThingsAPI.Controllers;
using KeepThingsAPI.Models;
using Newtonsoft.Json;
using System;
using NUnit.Framework.Interfaces;


namespace KeepThingsAPITests
{
    class UserTest
    {
        User testUser = new User();
        SqlConnectionController sql = new SqlConnectionController();
        private bool stop;
        [SetUp]
        public void Setup()
        {
            testUser.Auth0_id = "ThisIsAAuth0Id";
            testUser.name = "van Testenburg";
            testUser.first_name = "Sir. Test";
            testUser.password = "ThisIsASuperPasswort";
            testUser.email = "sirTest@gmail.com";
            testUser.tel_nr = "999911119999";
            testUser.username = "SirSuperTest";
            testUser.type = "User";
            testUser.verified = false;

            if (stop)
            {
                Assert.Inconclusive("Previous test failed");
            }
        }
        [Test, Order(1)]
        public void testUserPost()
        {
            String resultString = sql.User_postUser(testUser);
            User resultUser = JsonConvert.DeserializeObject<User>(resultString);
            Assert.AreEqual(testUser.Auth0_id, resultUser.Auth0_id);
            Assert.AreEqual(testUser.name, resultUser.name);
            Assert.AreEqual(testUser.first_name, resultUser.first_name);
            Assert.AreEqual(testUser.password, resultUser.password);
            Assert.AreEqual(testUser.email, resultUser.email);
            Assert.AreEqual(testUser.tel_nr, resultUser.tel_nr);
            Assert.AreEqual(testUser.username, resultUser.username);
            Assert.AreEqual(testUser.type, resultUser.type);
            Assert.AreEqual(testUser.verified, resultUser.verified);
            testUser.id = resultUser.id;
        }
        [Test, Order(2)]
        public void testUserGetWithId()
        {
            String resultString = sql.User_getUser(testUser.Auth0_id);
            User resultUser = JsonConvert.DeserializeObject<User>(resultString);
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
        public void testUserPut()
        {
            testUser.username = "SirSuperTestTheSecondChanded";
            String resultString = sql.User_putUser(testUser.Auth0_id, testUser);
            User resultUser = JsonConvert.DeserializeObject<User>(resultString);
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
        [Test, Order(4)]
        public void testUserDelete()
        {
            String result = sql.User_deleteUser(testUser.id);
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
