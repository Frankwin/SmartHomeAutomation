using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHomeAutomation.Domain.Models.Account;

namespace SmartHomeAutomation.Services.Tests
{
    [TestClass]
    public class AccountServiceTests : TestBase
    {
        [TestInitialize]
        public void TestInitialize()
        {
            TestAccount = CreateTestAccount();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteTestAccount(TestAccount);
        }

        [TestMethod]        
        public void CreateNewAccountWithUpsertTest()
        {
            var newAccount = new Account {AccountName = "New Upsert Test Account"};
            AccountService.Upsert(newAccount, TestUser);
            var foundAccounts = AccountService.Search("New Upsert Test Account").ToList();

            Assert.AreEqual(1, foundAccounts.Count);
            Assert.AreEqual(foundAccounts.First().AccountName, newAccount.AccountName);

            AccountService.DeleteByGuid(foundAccounts.First().AccountId);
        }

        [TestMethod]        
        public void SoftDeleteAccountTest()
        {
            var foundAccounts = AccountService.Search("TestAccount").ToList();

            Assert.AreEqual(1, foundAccounts.Count);
            AccountService.SoftDelete(foundAccounts.First().AccountId, TestUser);
            
            var softDeletedAccount = AccountService.Search("TestAccount").ToList();
            Assert.AreEqual(1,softDeletedAccount.Count);
            Assert.IsTrue(softDeletedAccount.First().IsDeleted);
        }

        [TestMethod]
        public void UniqueNameCheckDuplicateAccount()
        {
            var uniqueName = AccountService.CheckForExistingAccountName("TestAccount");

            Assert.AreEqual(TestAccount.AccountName, uniqueName.AccountName);
        }

        [TestMethod]
        public void UniqueNameCheckNonDuplicateAccount()
        {
            var uniqueName = AccountService.CheckForExistingAccountName("TestingAccount");

            Assert.IsNull(uniqueName);
        }

        [TestMethod]
        public void UpdateAccountUsingUpsert()
        {
            TestAccount.AccountName = "TestAccount updated";
            AccountService.Upsert(TestAccount, TestUser);
            var foundAccounts = AccountService.Search("TestAccount updated").ToList();

            Assert.AreEqual(1, foundAccounts.Count);
            Assert.AreEqual(foundAccounts.First().AccountName, TestAccount.AccountName);
        }
    }
}
