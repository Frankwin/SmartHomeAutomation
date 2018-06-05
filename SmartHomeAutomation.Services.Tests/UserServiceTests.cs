using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHomeAutomation.Domain.Models.User;

namespace SmartHomeAutomation.Services.Tests
{
    [TestClass]
    public class UserServiceTests : TestBase
    {
        [TestInitialize]
        public void TestInitialize()
        {
            TestUser = CreateTestUser();
        } 

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteUser(TestUser);
        }

        [TestMethod]        
        public void CreateNewUserWithUpsertTest()
        {
            var newUser = new User {UserName = "New Upsert Test User", EmailAddress = "TestUser@test.com", Password = "password", AccountId = TestAccount.AccountId};
            UserService.Upsert(newUser, TestUserPrincipal);
            var foundUsers = UserService.Search("New Upsert Test User").ToList();

            Assert.AreEqual(1, foundUsers.Count);
            Assert.AreEqual(foundUsers.First().UserName, newUser.UserName);

            UserService.DeleteByGuid(foundUsers.First().UserId);

        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateNameException))]
        public void InsertUserThatAlreadyExistsTest()
        {
            TestUser.IsDeleted = true;
            UserService.Upsert(TestUser, TestUserPrincipal);
        }

        [TestMethod]
        public void InsertUserWithNameThatIsSoftDeletedTest()
        {
            TestUser.IsDeleted = true;
            UserService.Update(TestUser);

            var newUser = new User {UserName= TestUserName, EmailAddress = "TestUser@test.com", Password = "password", AccountId = TestAccount.AccountId};
            UserService.Upsert(newUser, TestUserPrincipal);

            var foundUsers = UserService.Search(TestUserName).ToList();

            Assert.AreEqual(1, foundUsers.Count);
            Assert.AreEqual(foundUsers.First().UserName, TestUser.UserName);
            Assert.IsFalse(foundUsers.First().IsDeleted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SoftDeleteUserThatDoesNotExist()
        {
            var guid = Guid.NewGuid();
            UserService.SoftDelete(guid, TestUserPrincipal);
        }

        [TestMethod]        
        public void SoftDeleteUserTest()
        {
            var foundUsers = UserService.Search(TestUserName).ToList();

            Assert.AreEqual(1, foundUsers.Count);
            UserService.SoftDelete(foundUsers.First().UserId, TestUserPrincipal);
            
            var softDeletedUser = UserService.Search(TestUserName).ToList();
            Assert.AreEqual(1,softDeletedUser.Count);
            Assert.IsTrue(softDeletedUser.First().IsDeleted);
        }

        [TestMethod]
        public void CheckDuplicateUser()
        {
            var uniqueName = UserService.CheckForExistingUserName(TestUserName);

            Assert.AreEqual(TestUser.UserName, uniqueName.UserName);
        }

        [TestMethod]
        public void UniqueNameCheckNonDuplicateAccount()
        {
            var uniqueName = UserService.CheckForExistingUserName("Testing User");

            Assert.IsNull(uniqueName);
        }

        [TestMethod]
        public void UpdateUserUsingUpsert()
        {
            TestUser.UserName = TestUserName + " updated";
            UserService.Upsert(TestUser, TestUserPrincipal);
            var foundAccounts = UserService.Search(TestUserName + " updated").ToList();

            Assert.AreEqual(1, foundAccounts.Count);
            Assert.AreEqual(foundAccounts.First().UserName, TestUser.UserName);
        }
    }
}