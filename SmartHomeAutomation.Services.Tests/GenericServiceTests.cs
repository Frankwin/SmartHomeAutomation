﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHomeAutomation.Services.Tests
{
    [TestClass]
    public class GenericServiceTests : TestBase
    {

        [TestInitialize]
        public void TestInitialize()
        {
            TestAccount = CreateTestAccount("Test Account 01");
            CreateTestAccount("Test Account 02");
            CreateTestAccount("Test Account 03");
            CreateTestAccount("Test Account 04");
            CreateTestAccount("Test Account 05");
            CreateTestAccount("Test Account 06");
            CreateTestAccount("Test Account 07");
            CreateTestAccount("Test Account 08");
            CreateTestAccount("Test Account 09");
            CreateTestAccount("Test Account 10");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteTestAccountByName("Test Account 01");
            DeleteTestAccountByName("Test Account 02");
            DeleteTestAccountByName("Test Account 03");
            DeleteTestAccountByName("Test Account 04");
            DeleteTestAccountByName("Test Account 05");
            DeleteTestAccountByName("Test Account 06");
            DeleteTestAccountByName("Test Account 07");
            DeleteTestAccountByName("Test Account 08");
            DeleteTestAccountByName("Test Account 09");
            DeleteTestAccountByName("Test Account 10");
        }

        [TestMethod]
        public void GetAllTest()
        {
            var accounts = AccountService.GetAll();
            Assert.IsTrue(accounts.Count() == 10);
        }

        [TestMethod]
        public void GetAllByPageTest()
        {
            var accountByPage = AccountService.GetByPage(5, 1, "AccountName", "DESC");
            Assert.IsTrue(accountByPage.Collection.Count == 5);
            Assert.IsTrue(accountByPage.TotalCount == 10);
            Assert.IsTrue(accountByPage.TotalPages.Equals(2.0));
            Assert.IsTrue(accountByPage.Collection.First().AccountName == "Test Account 10");
        }

        [TestMethod]
        public void SearchAllByPageTest()
        {
            var accountByPage = AccountService.SearchByPage("Test Account", 5, 1, "AccountName", "ASC");
            Assert.IsTrue(accountByPage.Collection.Count == 5);
            Assert.IsTrue(accountByPage.TotalCount == 10);
            Assert.IsTrue(accountByPage.TotalPages.Equals(2.0));
            Assert.IsTrue(accountByPage.Collection.First().AccountName == "Test Account 01");
        }

        [TestMethod]
        public void GetByGuidTest()
        {
            var account = AccountService.GetByGuid(TestAccount.AccountId);
            Assert.AreEqual(account.AccountName, TestAccount.AccountName);
        }

        [TestMethod]
        public void GetByPropertyTest()
        {
            var account = AccountService.GetByProperty("AccountName", "Test Account 01").ToList();
            Assert.AreEqual(1, account.Count);
            Assert.AreEqual("Test Account 01", account.First().AccountName);
        }

        [TestMethod]
        public void GetAllIncludingTest()
        {
            var accounts = AccountService.GetAllIncluding(a => a.Users);
            Assert.IsTrue(accounts.First().Users.Count == 0);
        }
    }
}
