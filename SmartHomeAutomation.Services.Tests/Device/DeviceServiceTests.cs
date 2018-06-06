using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHomeAutomation.Services.Tests.Device
{
    [TestClass]
    public class DeviceServiceTests : TestBase
    {
        [TestMethod]        
        public void CreateNewDeviceWithUpsertTest()
        {
            var newDevice = new Domain.Models.Device.Device
            {
                DeviceName = "New Upsert Test Device",
                DeviceTypeId = TestDeviceType.DeviceTypeId,
                ManufacturerId = TestManufacturer.ManufacturerId
            };
            DeviceService.Upsert(newDevice, TestUserPrincipal);
            var foundDevices = DeviceService.Search("New Upsert Test Device").ToList();

            Assert.AreEqual(1, foundDevices.Count);
            Assert.AreEqual(foundDevices.First().DeviceName, newDevice.DeviceName);

            DeviceService.DeleteByGuid(foundDevices.First().DeviceId);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateNameException))]
        public void InsertDeviceThatAlreadyExistsTest()
        {
            TestDevice.IsDeleted = true;
            DeviceService.Upsert(TestDevice, TestUserPrincipal);
        }

        [TestMethod]
        public void InsertDeviceWithNameThatIsSoftDeletedTest()
        {
            TestDevice.IsDeleted = true;
            DeviceService.Update(TestDevice);

            var newDevice = new Domain.Models.Device.Device {DeviceName= TestDeviceName, DeviceTypeId = TestDeviceType.DeviceTypeId};
            DeviceService.Upsert(newDevice, TestUserPrincipal);

            var foundDevices = DeviceService.Search(TestDeviceName).ToList();

            Assert.AreEqual(1, foundDevices.Count);
            Assert.AreEqual<string>(foundDevices.First().DeviceName, TestDevice.DeviceName);
            Assert.IsFalse(foundDevices.First().IsDeleted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SoftDeleteDeviceThatDoesNotExistTest()
        {
            var guid = Guid.NewGuid();
            DeviceService.SoftDelete(guid, TestUserPrincipal);
        }

        [TestMethod]        
        public void SoftDeleteDeviceTest()
        {
            var foundDevices = DeviceService.Search(TestDeviceName).ToList();

            Assert.AreEqual(1, foundDevices.Count);
            DeviceService.SoftDelete(foundDevices.First().DeviceId, TestUserPrincipal);
            
            var softDeletedDevice = DeviceService.Search(TestDeviceName).ToList();
            Assert.AreEqual(1,softDeletedDevice.Count);
            Assert.IsTrue(softDeletedDevice.First().IsDeleted);
        }

        [TestMethod]
        public void CheckDuplicateDeviceTest()
        {
            var uniqueName = DeviceService.CheckForExistingDevice(TestDeviceName);

            Assert.AreEqual<string>(TestDevice.DeviceName, uniqueName.DeviceName);
        }

        [TestMethod]
        public void UniqueNameCheckNonDuplicateDeviceTest()
        {
            var uniqueName = DeviceService.CheckForExistingDevice("Testing Device");

            Assert.IsNull(uniqueName);
        }

        [TestMethod]
        public void UpdateDeviceUsingUpsertTest()
        {
            TestDevice.DeviceName = TestDeviceName + " updated";
            DeviceService.Upsert(TestDevice, TestUserPrincipal);
            var foundDevices = DeviceService.Search(TestDeviceName + " updated").ToList();

            Assert.AreEqual(1, foundDevices.Count);
            Assert.AreEqual<string>(foundDevices.First().DeviceName, TestDevice.DeviceName);
        }
    }
}
