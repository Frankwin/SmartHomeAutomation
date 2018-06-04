using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHomeAutomation.Domain.Models.Device;

namespace SmartHomeAutomation.Services.Tests
{
    [TestClass]
    public class DeviceTypeServiceTests : TestBase
    {
        [TestInitialize]
        public void TestInitialize() => TestDeviceType = CreateTestDeviceType();

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteTestDeviceType(TestDeviceType);
        }

        [TestMethod]        
        public void CreateNewDeviceTypeWithUpsertTest()
        {
            var newDeviceType = new DeviceType { DeviceTypeName = "New Upsert Test Device Type", DeviceCategoryId = TestDeviceCategory.DeviceCategoryId};
            DeviceTypeService.Upsert(newDeviceType, TestUser);
            var foundDeviceTypes = DeviceTypeService.Search("New Upsert Test Device Type").ToList();

            Assert.AreEqual(1, foundDeviceTypes.Count);
            Assert.AreEqual(foundDeviceTypes.First().DeviceTypeName, newDeviceType.DeviceTypeName);

            DeviceTypeService.DeleteByGuid(foundDeviceTypes.First().DeviceTypeId);
        }

        [TestMethod]        
        public void SoftDeleteDeviceTypeTest()
        {
            var foundDeviceTypes = DeviceTypeService.Search(TestDeviceTypeName).ToList();

            Assert.AreEqual(1, foundDeviceTypes.Count);
            DeviceTypeService.SoftDelete(foundDeviceTypes.First().DeviceTypeId, TestUser);
            
            var softDeletedAccount = DeviceTypeService.Search(TestDeviceTypeName).ToList();
            Assert.AreEqual(1,softDeletedAccount.Count);
            Assert.IsTrue(softDeletedAccount.First().IsDeleted);
        }

        [TestMethod]
        public void UniqueNameCheckDuplicateDeviceType()
        {
            var uniqueName = DeviceTypeService.CheckForExistingDeviceType(TestDeviceTypeName);

            Assert.AreEqual(TestDeviceType.DeviceTypeName, uniqueName.DeviceTypeName);
        }

        [TestMethod]
        public void UniqueNameCheckNonDuplicateAccount()
        {
            var uniqueName = DeviceTypeService.CheckForExistingDeviceType("Testing Device Type");

            Assert.IsNull(uniqueName);
        }

        [TestMethod]
        public void UpdateDeviceTypeUsingUpsert()
        {
            TestDeviceType.DeviceTypeName = TestDeviceTypeName + " updated";
            DeviceTypeService.Upsert(TestDeviceType, TestUser);
            var foundDeviceTypes = DeviceTypeService.Search(TestDeviceTypeName + " updated").ToList();

            Assert.AreEqual(1, foundDeviceTypes.Count);
            Assert.AreEqual(foundDeviceTypes.First().DeviceTypeName, TestDeviceType.DeviceTypeName);
        }
    }
}
