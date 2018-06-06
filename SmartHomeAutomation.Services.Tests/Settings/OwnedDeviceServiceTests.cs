using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHomeAutomation.Domain.Models.Settings;

namespace SmartHomeAutomation.Services.Tests.Settings
{
    [TestClass]
    public class OwnedDeviceServiceTests : TestBase
    {
        [TestMethod]        
        public void CreateNewOwnedDeviceWithUpsertTest()
        {
            var newOwnedDevice = new OwnedDevice { DeviceName = "New Upsert Test Owned Device", AccountId = TestAccount.AccountId, RoomId = TestRoom.RoomId};
            OwnedDeviceService.Upsert(newOwnedDevice, TestUserPrincipal);
            var foundOwnedDevices = OwnedDeviceService.Search("New Upsert Test Owned Device").ToList();

            Assert.AreEqual(1, foundOwnedDevices.Count);
            Assert.AreEqual(foundOwnedDevices.First().DeviceName, newOwnedDevice.DeviceName);

            OwnedDeviceService.DeleteByGuid(foundOwnedDevices.First().OwnedDeviceId);
        }

        [TestMethod]        
        public void SoftDeleteRoomTest()
        {
            var foundRooms = RoomService.Search(TestRoomName).ToList();

            Assert.AreEqual(1, foundRooms.Count);
            RoomService.SoftDelete(foundRooms.First().RoomId, TestUserPrincipal);
            
            var softDeleteRoom = RoomService.Search(TestRoomName).ToList();
            Assert.AreEqual(1,softDeleteRoom.Count);
            Assert.IsTrue(softDeleteRoom.First().IsDeleted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SoftDeleteRoomThatDoesNotExistTest()
        {
            var guid = Guid.NewGuid();
            RoomService.SoftDelete(guid, TestUserPrincipal);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateNameException))]
        public void InsertRoomThatAlreadyExistsTest()
        {
            TestRoom.IsDeleted = true;
            RoomService.Upsert(TestRoom, TestUserPrincipal);
        }

        [TestMethod]
        public void UniqueNameCheckDuplicateRoomTest()
        {
            var uniqueName = RoomService.CheckForExistingRoom(TestRoomName);

            Assert.AreEqual(TestRoom.RoomName, uniqueName.RoomName);
        }

        [TestMethod]
        public void InsertRoomWithNameThatIsSoftDeletedTest()
        {
            TestRoom.IsDeleted = true;
            RoomService.Update(TestRoom);

            var newRoom = new Room { RoomName = TestRoomName, AccountId = TestAccount.AccountId};
            RoomService.Upsert(newRoom, TestUserPrincipal);

            var foundRooms = RoomService.Search(TestRoomName).ToList();

            Assert.AreEqual(1, foundRooms.Count);
            Assert.AreEqual(foundRooms.First().RoomName, TestRoom.RoomName);
            Assert.IsFalse(foundRooms.First().IsDeleted);
        }

        [TestMethod]
        public void UniqueNameCheckNonDuplicateRoomTest()
        {
            var uniqueName = RoomService.CheckForExistingRoom("Testing Device Type");

            Assert.IsNull(uniqueName);
        }

        [TestMethod]
        public void UpdateRoomUsingUpsertTest()
        {
            TestRoom.RoomName = TestRoomName + " updated";
            RoomService.Upsert(TestRoom, TestUserPrincipal);
            var foundRooms = RoomService.Search(TestRoomName + " updated").ToList();

            Assert.AreEqual(1, foundRooms.Count);
            Assert.AreEqual(foundRooms.First().RoomName, TestRoom.RoomName);
        }
    }
}
