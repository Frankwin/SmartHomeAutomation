using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHomeAutomation.Services.Tests.Account
{
    [TestClass]
    public class AccountServiceTests : TestBase
    {
        [TestMethod]        
        public void CreateNewAccountWithUpsertTest()
        {
            var newAccount = new Domain.Models.AccountModels.Account {AccountName = "New Upsert Test Account"};
            AccountService.Upsert(newAccount, TestUserPrincipal);
            var foundAccounts = AccountService.Search("New Upsert Test Account").ToList();

            Assert.AreEqual(1, foundAccounts.Count);
            Assert.AreEqual(foundAccounts.First().AccountName, newAccount.AccountName);

            AccountService.DeleteByGuid(foundAccounts.First().AccountId);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateNameException))]
        public void InsertAccountThatAlreadyExistsTest()
        {
            TestAccount.IsDeleted = true;
            AccountService.Upsert(TestAccount, TestUserPrincipal);
        }

        [TestMethod]
        public void InsertAccountWithNameThatIsSoftDeletedTest()
        {
            TestAccount.IsDeleted = true;
            AccountService.Update(TestAccount);

            var newAccount = new Domain.Models.AccountModels.Account { AccountName = TestAccountName};
            AccountService.Upsert(newAccount, TestUserPrincipal);

            var foundAccounts = AccountService.Search(TestAccountName).ToList();

            Assert.AreEqual(1, foundAccounts.Count);
            Assert.AreEqual<string>(foundAccounts.First().AccountName, TestAccount.AccountName);
            Assert.IsFalse(foundAccounts.First().IsDeleted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SoftDeleteAccountThatDoesNotExistTest()
        {
            var guid = Guid.NewGuid();
            AccountService.SoftDelete(guid, TestUserPrincipal);
        }

        [TestMethod]        
        public void SoftDeleteAccountTest()
        {
            var foundAccounts = AccountService.Search(TestAccountName).ToList();

            Assert.AreEqual(1, foundAccounts.Count);
            AccountService.SoftDelete(foundAccounts.First().AccountId, TestUserPrincipal);
            
            var softDeletedAccount = AccountService.Search(TestAccountName).ToList();
            Assert.AreEqual(1,softDeletedAccount.Count);
            Assert.IsTrue(softDeletedAccount.First().IsDeleted);
        }

        [TestMethod]
        public void UniqueNameCheckDuplicateAccountTest()
        {
            var uniqueName = AccountService.CheckForExistingAccountName(TestAccountName);

            Assert.AreEqual<string>(TestAccount.AccountName, uniqueName.AccountName);
        }

        [TestMethod]
        public void UniqueNameCheckNonDuplicateAccountTest()
        {
            var uniqueName = AccountService.CheckForExistingAccountName("Testing Account");

            Assert.IsNull(uniqueName);
        }

        [TestMethod]
        public void UpdateAccountUsingUpsertTest()
        {
            TestAccount.AccountName = TestAccountName + " updated";
            AccountService.Upsert(TestAccount, TestUserPrincipal);
            var foundAccounts = AccountService.Search(TestAccountName + " updated").ToList();

            Assert.AreEqual(1, foundAccounts.Count);
            Assert.AreEqual<string>(foundAccounts.First().AccountName, TestAccount.AccountName);
        }
    }
}
