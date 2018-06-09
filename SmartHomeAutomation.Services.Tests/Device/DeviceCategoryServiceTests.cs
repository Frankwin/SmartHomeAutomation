using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHomeAutomation.Domain.Models.DeviceModels;

namespace SmartHomeAutomation.Services.Tests.Device
{
    [TestClass]
    public class DeviceCategoryServiceTests : TestBase
    {
        [TestMethod]        
        public void CreateNewDeviceCategoryWithUpsertTest()
        {
            var newDeviceCategory = new DeviceCategory {DeviceCategoryName = "New Upsert Test Device Category"};
            DeviceCategoryService.Upsert(newDeviceCategory, TestUserPrincipal);
            var foundDeviceCategories = DeviceCategoryService.Search("New Upsert Test Device Category").ToList();

            Assert.AreEqual(1, foundDeviceCategories.Count);
            Assert.AreEqual(foundDeviceCategories.First().DeviceCategoryName, newDeviceCategory.DeviceCategoryName);

            DeviceCategoryService.DeleteByGuid(foundDeviceCategories.First().DeviceCategoryId);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateNameException))]
        public void InsertDeviceCategoryThatAlreadyExistsTest()
        {
            TestDeviceCategory.IsDeleted = true;
            DeviceCategoryService.Upsert(TestDeviceCategory, TestUserPrincipal);
        }

        [TestMethod]        
        public void SoftDeleteDeviceCategoryTest()
        {
            var foundDeviceCategories = DeviceCategoryService.Search(TestDeviceCategoryName).ToList();

            Assert.AreEqual(1, foundDeviceCategories.Count);
            DeviceCategoryService.SoftDelete(foundDeviceCategories.First().DeviceCategoryId, TestUserPrincipal);
            
            var softDeletedAccount = DeviceCategoryService.Search(TestDeviceCategoryName).ToList();
            Assert.AreEqual(1,softDeletedAccount.Count);
            Assert.IsTrue(softDeletedAccount.First().IsDeleted);
        }

        [TestMethod]
        public void UniqueNameCheckDuplicateDeviceCategoryTest()
        {
            var uniqueName = DeviceCategoryService.CheckForExistingDeviceCategory(TestDeviceCategoryName);

            Assert.AreEqual(TestDeviceCategory.DeviceCategoryName, uniqueName.DeviceCategoryName);
        }

        [TestMethod]
        public void InsertDeviceCategoryWithNameThatIsSoftDeletedTest()
        {
            TestDeviceCategory.IsDeleted = true;
            DeviceCategoryService.Update(TestDeviceCategory);

            var newDeviceCategory = new DeviceCategory() { DeviceCategoryName = TestDeviceCategoryName };
            DeviceCategoryService.Upsert(newDeviceCategory, TestUserPrincipal);

            var foundDeviceCategories = DeviceCategoryService.Search(TestDeviceCategoryName).ToList();

            Assert.AreEqual(1, foundDeviceCategories.Count);
            Assert.AreEqual(foundDeviceCategories.First().DeviceCategoryName, TestDeviceCategory.DeviceCategoryName);
            Assert.IsFalse(foundDeviceCategories.First().IsDeleted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SoftDeleteDeviceCategoryThatDoesNotExistTest()
        {
            var guid = Guid.NewGuid();
            DeviceCategoryService.SoftDelete(guid, TestUserPrincipal);
        }

        [TestMethod]
        public void UniqueNameCheckNonDuplicateDeviceCategory()
        {
            var uniqueName = DeviceCategoryService.CheckForExistingDeviceCategory("Testing Device Category");

            Assert.IsNull(uniqueName);
        }

        [TestMethod]
        public void UpdateDeviceCategoryUsingUpsertTest()
        {
            TestDeviceCategory.DeviceCategoryName = TestDeviceCategoryName + " updated";
            DeviceCategoryService.Upsert(TestDeviceCategory, TestUserPrincipal);
            var foundAccounts = DeviceCategoryService.Search(TestDeviceCategoryName + " updated").ToList();

            Assert.AreEqual(1, foundAccounts.Count);
            Assert.AreEqual(foundAccounts.First().DeviceCategoryName, TestDeviceCategory.DeviceCategoryName);
        }
    }
}
