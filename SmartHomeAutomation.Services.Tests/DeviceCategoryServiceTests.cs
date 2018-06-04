using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHomeAutomation.Domain.Models.Device;

namespace SmartHomeAutomation.Services.Tests
{
    [TestClass]
    public class DeviceCategoryServiceTests : TestBase
    {
        [TestInitialize]
        public void TestInitialize() => TestDeviceCategory = CreateTestDeviceCategory();

        [TestCleanup]
        public void TestCleanup() => DeleteTestDeviceCategory(TestDeviceCategory);

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
            var foundDeviceCategories = DeviceCategoryService.Search(TestDeviceCategoryName).ToList();

            Assert.AreEqual(1, foundDeviceCategories.Count);
            DeviceCategoryService.SoftDelete(foundDeviceCategories.First().DeviceCategoryId, TestUser);
            
            var softDeletedAccount = DeviceCategoryService.Search(TestDeviceCategoryName).ToList();
            Assert.AreEqual(1,softDeletedAccount.Count);
            Assert.IsTrue(softDeletedAccount.First().IsDeleted);
        }

        [TestMethod]
        public void UniqueNameCheckDuplicateAccount()
        {
            var uniqueName = DeviceCategoryService.CheckForExistingDeviceCategory(TestDeviceCategoryName);

            Assert.AreEqual(TestDeviceCategory.DeviceCategoryName, uniqueName.DeviceCategoryName);
        }

        [TestMethod]
        public void UniqueNameCheckNonDuplicateAccount()
        {
            var uniqueName = DeviceCategoryService.CheckForExistingDeviceCategory("Testing Device Category");

            Assert.IsNull(uniqueName);
        }

        [TestMethod]
        public void UpdateAccountUsingUpsert()
        {
            TestDeviceCategory.DeviceCategoryName = TestDeviceCategoryName + " updated";
            DeviceCategoryService.Upsert(TestDeviceCategory, TestUser);
            var foundAccounts = DeviceCategoryService.Search(TestDeviceCategoryName + " updated").ToList();

            Assert.AreEqual(1, foundAccounts.Count);
            Assert.AreEqual(foundAccounts.First().DeviceCategoryName, TestDeviceCategory.DeviceCategoryName);
        }
    }
}
