using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHomeAutomation.Domain.Models.SettingsModels;

namespace SmartHomeAutomation.Services.Tests.Settings
{
    [TestClass]
    public class DeviceSettingServiceTests : TestBase
    {
        [TestMethod]        
        public void CreateNewDeviceSettingWithUpsertTest()
        {
            var newDeviceSetting = new DeviceSetting
            {
                DeviceSettingName = "New Upsert Test Device Setting",
                DeviceSettingValue = "New Device Setting Upsert Value",
                OwnedDeviceId = TestOwnedDevice.OwnedDeviceId
            };
            DeviceSettingService.Upsert(newDeviceSetting, TestUserPrincipal);
            var foundDeviceSettings = DeviceSettingService.Search("New Upsert Test Device Setting").ToList();

            Assert.AreEqual(1, foundDeviceSettings.Count);
            Assert.AreEqual(foundDeviceSettings.First().DeviceSettingName, newDeviceSetting.DeviceSettingName);
            Assert.AreEqual(foundDeviceSettings.First().DeviceSettingValue, newDeviceSetting.DeviceSettingValue);
            
            DeviceSettingService.DeleteByGuid(foundDeviceSettings.First().DeviceSettingId);
        }

        [TestMethod]        
        public void SoftDeleteDeviceSettingTest()
        {
            var foundDeviceSettings = DeviceSettingService.Search(TestDeviceSettingName).ToList();

            Assert.AreEqual(1, foundDeviceSettings.Count);
            DeviceSettingService.SoftDelete(foundDeviceSettings.First().DeviceSettingId, TestUserPrincipal);
            
            var softDeleteDeviceSetting = DeviceSettingService.Search(TestDeviceSettingName).ToList();
            Assert.AreEqual(1,softDeleteDeviceSetting.Count);
            Assert.IsTrue(softDeleteDeviceSetting.First().IsDeleted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SoftDeleteDeviceSettingThatDoesNotExistTest()
        {
            var guid = Guid.NewGuid();
            DeviceSettingService.SoftDelete(guid, TestUserPrincipal);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateNameException))]
        public void InsertDeviceSettingThatAlreadyExistsTest()
        {
            TestDeviceSetting.IsDeleted = true;
            DeviceSettingService.Upsert(TestDeviceSetting, TestUserPrincipal);
        }

        [TestMethod]
        public void UniqueNameCheckDuplicateDeviceSettingTest()
        {
            var uniqueName = DeviceSettingService.CheckForExistingDeviceSetting(TestDeviceSettingName);

            Assert.AreEqual(TestDeviceSetting.DeviceSettingName, uniqueName.DeviceSettingName);
        }

        [TestMethod]
        public void InsertDeviceSettingWithNameThatIsSoftDeletedTest()
        {
            TestDeviceSetting.IsDeleted = true;
            DeviceSettingService.Update(TestDeviceSetting);

            var newDeviceSetting = new DeviceSetting { DeviceSettingName = TestDeviceSettingName, DeviceSettingValue = TestDeviceSettingValue};
            DeviceSettingService.Upsert(newDeviceSetting, TestUserPrincipal);

            var foundDeviceSettings = DeviceSettingService.Search(TestDeviceSettingName).ToList();

            Assert.AreEqual(1, foundDeviceSettings.Count);
            Assert.AreEqual(foundDeviceSettings.First().DeviceSettingName, TestDeviceSetting.DeviceSettingName);
            Assert.AreEqual(foundDeviceSettings.First().DeviceSettingValue, TestDeviceSetting.DeviceSettingValue);
            Assert.IsFalse(foundDeviceSettings.First().IsDeleted);
        }

        [TestMethod]
        public void UniqueNameCheckNonDuplicateDeviceSettingTest()
        {
            var uniqueName = DeviceSettingService.CheckForExistingDeviceSetting("Testing Device Type");

            Assert.IsNull(uniqueName);
        }

        [TestMethod]
        public void UpdateDeviceSettingUsingUpsertTest()
        {
            TestDeviceSetting.DeviceSettingName = TestDeviceSettingName + " updated";
            DeviceSettingService.Upsert(TestDeviceSetting, TestUserPrincipal);
            var foundDeviceSettings = DeviceSettingService.Search(TestDeviceSettingName + " updated").ToList();

            Assert.AreEqual(1, foundDeviceSettings.Count);
            Assert.AreEqual(foundDeviceSettings.First().DeviceSettingName, TestDeviceSetting.DeviceSettingName);
        }
    }
}
