using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHomeAutomation.Domain.Models.Device;

namespace SmartHomeAutomation.Services.Tests
{
    [TestClass]
    public class DeviceCategoryServiceTests : TestBase
    {
        [TestInitialize]
        public void TestInitialize()
        {
            TestDeviceCategory = CreateTestDeviceCategory();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteTestAccount(TestDeviceCategory);
        }

        [TestMethod]        
        public void CreateNewAccountWithUpsertTest()
        {
            var newDeviceCategory = new DeviceCategory {DeviceCategoryName = "New Upsert Test Device Category"};
            DeviceCategoryService.Upsert(newDeviceCategory, TestUser);
            var foundDeviceCategories = DeviceCategoryService.Search("New Upsert Test Device Category").ToList();

            Assert.AreEqual(1, foundDeviceCategories.Count);
            Assert.AreEqual(foundDeviceCategories.First().DeviceCategoryName, newDeviceCategory.DeviceCategoryName);

            DeviceCategoryService.DeleteByGuid(foundDeviceCategories.First().DeviceCategoryId);
        }

        [TestMethod]        
        public void SoftDeleteAccountTest()
        {
            var foundAccounts = DeviceCategoryService.Search("TestDeviceCategory").ToList();

            Assert.AreEqual(1, foundAccounts.Count);
            DeviceCategoryService.SoftDelete(foundAccounts.First().DeviceCategoryId, TestUser);
            
            var softDeletedAccount = DeviceCategoryService.Search("TestDeviceCategory").ToList();
            Assert.AreEqual(1,softDeletedAccount.Count);
            Assert.IsTrue(softDeletedAccount.First().IsDeleted);
        }

        [TestMethod]
        public void UniqueNameCheckDuplicateAccount()
        {
            var uniqueName = DeviceCategoryService.UniqueNameCheck("TestDeviceCategory");

            Assert.AreEqual(TestDeviceCategory.DeviceCategoryName, uniqueName.DeviceCategoryName);
        }

        [TestMethod]
        public void UniqueNameCheckNonDuplicateAccount()
        {
            var uniqueName = DeviceCategoryService.UniqueNameCheck("TestingAccount");

            Assert.IsNull(uniqueName);
        }

        [TestMethod]
        public void UpdateAccountUsingUpsert()
        {
            TestDeviceCategory.DeviceCategoryName = "TestDeviceCategory updated";
            DeviceCategoryService.Upsert(TestDeviceCategory, TestUser);
            var foundAccounts = DeviceCategoryService.Search("TestDeviceCategory updated").ToList();

            Assert.AreEqual(1, foundAccounts.Count);
            Assert.AreEqual(foundAccounts.First().DeviceCategoryName, TestDeviceCategory.DeviceCategoryName);
        }
    }
}
