using System.Linq;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHomeAutomation.Domain.Models.Device;
using SmartHomeAutomation.Domain.Models.Settings;
using SmartHomeAutomation.Services.Interfaces;
using SmartHomeAutomation.Services.Services;
using SmartHomeAutomation.Services.Services.Device;
using SmartHomeAutomation.Services.Services.Settings;

namespace SmartHomeAutomation.Services.Tests
{
    public class TestBase
    {
        private static readonly ISmartHomeAutomationService SmartHomeAutomationService = new SmartHomeAutomationService();
        public readonly IPrincipal TestUserPrincipal = new GenericPrincipal(new GenericIdentity("Test Account"), new[] { "Test" });
        public readonly AccountService AccountService = new AccountService(SmartHomeAutomationService);
        public readonly DeviceCategoryService DeviceCategoryService = new DeviceCategoryService(SmartHomeAutomationService);
        public readonly ManufacturerService ManufacturerService = new ManufacturerService(SmartHomeAutomationService);
        public readonly DeviceTypeService DeviceTypeService = new DeviceTypeService(SmartHomeAutomationService);
        public readonly UserService UserService = new UserService(SmartHomeAutomationService);
        public readonly RoomService RoomService = new RoomService(SmartHomeAutomationService);
        public readonly DeviceService DeviceService = new DeviceService(SmartHomeAutomationService);
        public readonly OwnedDeviceService OwnedDeviceService = new OwnedDeviceService(SmartHomeAutomationService);
        public readonly DeviceSettingService DeviceSettingService = new DeviceSettingService(SmartHomeAutomationService);

        public const string TestAccountName = "Test Account";
        public const string TestDeviceCategoryName = "Test Device Category";
        public const string TestManufacturerName = "Test Manufacturer";
        public const string TestDeviceTypeName = "Test Device Type";
        public const string TestUserName = "Test User Name";
        public const string TestRoomName = "Test Room Name";
        public const string TestDeviceName = "Test Device Name";
        public const string TestOwnedDeviceName = "Test Owned Device Name";
        public const string TestDeviceSettingName = "Test Device Setting Name";
        public const string TestDeviceSettingValue = "Test Device Setting Value";

        public Domain.Models.Account.Account TestAccount { get; set; }
        public DeviceCategory TestDeviceCategory { get; set; }
        public Manufacturer TestManufacturer { get; set; }
        public DeviceType TestDeviceType { get; set; }
        public Domain.Models.User.User TestUser { get; set; }
        public Room TestRoom { get; set; }
        public Domain.Models.Device.Device TestDevice { get; set; }
        public OwnedDevice TestOwnedDevice { get; set; }
        public DeviceSetting TestDeviceSetting { get; set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            TestAccount = CreateTestAccount();
            TestUser = CreateTestUser();
            TestDeviceCategory = CreateTestDeviceCategory();
            TestDeviceType = CreateTestDeviceType();
            TestManufacturer = CreateTestManufacturer();
            TestDevice = CreateTestDevice();
            TestRoom = CreateTestRoom();
            TestOwnedDevice = CreateTestOwnedDevice();
            TestDeviceSetting = CreateTestDeviceSetting();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            DeleteTestDeviceSetting(TestDeviceSetting);
            DeleteTestOwnedDevice(TestOwnedDevice);
            DeleteTestRoom(TestRoom);
            DeleteTestDevice(TestDevice);
            DeleteTestManufacturer(TestManufacturer);
            DeleteTestDeviceType(TestDeviceType);
            DeleteTestDeviceCategory(TestDeviceCategory);
            DeleteTestUser(TestUser);
            DeleteTestAccount(TestAccount);
        }

        public Domain.Models.Account.Account CreateTestAccount(string accountName = TestAccountName)
        {
            var newAccount = new Domain.Models.Account.Account { AccountName = accountName };
            AccountService.Insert(newAccount);
            newAccount = AccountService.Search(accountName).First();
            return newAccount;
        }

        public void DeleteTestAccount(Domain.Models.Account.Account account)
        {
            AccountService.DeleteByGuid(account.AccountId);
        }

        public void DeleteTestAccountByName(string name)
        {
            var toDeleteAccount = AccountService.Search(name).First();
            if (toDeleteAccount != null)
            {
                DeleteTestAccount(toDeleteAccount);
            }
        }

        public DeviceCategory CreateTestDeviceCategory()
        {
            var newDeviceCategory = new DeviceCategory { DeviceCategoryName = TestDeviceCategoryName };
            DeviceCategoryService.Insert(newDeviceCategory);
            newDeviceCategory = DeviceCategoryService.Search(TestDeviceCategoryName).First();
            return newDeviceCategory;
        }

        public void DeleteTestDeviceCategory(DeviceCategory deviceCategory)
        {
            DeviceCategoryService.DeleteByGuid(deviceCategory.DeviceCategoryId);
        }

        public Manufacturer CreateTestManufacturer()
        {
            var newManufacturer = new Manufacturer { ManufacturerName = TestManufacturerName };
            ManufacturerService.Insert(newManufacturer);
            newManufacturer = ManufacturerService.Search(TestManufacturerName).First();
            return newManufacturer;
        }

        public void DeleteTestManufacturer(Manufacturer manufacturer)
        {
            ManufacturerService.DeleteByGuid(manufacturer.ManufacturerId);
        }

        public DeviceType CreateTestDeviceType()
        {
            var newDeviceType = new DeviceType { DeviceTypeName = TestDeviceTypeName, DeviceCategoryId = TestDeviceCategory.DeviceCategoryId};
            DeviceTypeService.Insert(newDeviceType);
            newDeviceType = DeviceTypeService.Search(TestDeviceTypeName).First();
            return newDeviceType;
        }

        public void DeleteTestDeviceType(DeviceType deviceType)
        {
            DeviceTypeService.DeleteByGuid(deviceType.DeviceTypeId);
        }

        public Domain.Models.User.User CreateTestUser(string testUserName = TestUserName)
        {
            var newTestUser = new Domain.Models.User.User {UserName = testUserName, AccountId = TestAccount.AccountId,EmailAddress = "testuser@test.com", Password = "password"};
            UserService.Insert(newTestUser);
            newTestUser = UserService.Search(testUserName).First();
            return newTestUser;
        }

        public void DeleteTestUser(Domain.Models.User.User user)
        {
            UserService.DeleteByGuid(user.UserId);
        }

        public Room CreateTestRoom(string testRoomName = TestRoomName)
        {
            var newTestRoom = new Room { RoomName = testRoomName, AccountId = TestAccount.AccountId};
            RoomService.Insert(newTestRoom);
            newTestRoom = RoomService.Search(testRoomName).First();
            return newTestRoom;
        }

        public void DeleteTestRoom(Room room)
        {
            RoomService.DeleteByGuid(room.RoomId);
        }

        public Domain.Models.Device.Device CreateTestDevice(string testDeviceName = TestDeviceName)
        {
            var newTestDevice = new Domain.Models.Device.Device
            {
                DeviceName = testDeviceName,
                DeviceTypeId = TestDeviceType.DeviceTypeId,
                ManufacturerId = TestManufacturer.ManufacturerId
            };
            DeviceService.Insert(newTestDevice);
            newTestDevice = DeviceService.Search(testDeviceName).First();
            return newTestDevice;
        }

        public void DeleteTestDevice(Domain.Models.Device.Device device)
        {
            DeviceService.DeleteByGuid(device.DeviceId);
        }

        public OwnedDevice CreateTestOwnedDevice(string testOwnedDeviceName = TestOwnedDeviceName)
        {
            var newOwnedDevice = new OwnedDevice
            {
                DeviceName = testOwnedDeviceName,
                RoomId = TestRoom.RoomId,
                AccountId = TestAccount.AccountId
            };
            OwnedDeviceService.Insert(newOwnedDevice);
            newOwnedDevice = OwnedDeviceService.Search(testOwnedDeviceName).First();
            return newOwnedDevice;
        }

        public void DeleteTestOwnedDevice(OwnedDevice ownedDevice)
        {
            OwnedDeviceService.DeleteByGuid(ownedDevice.OwnedDeviceId);
        }

        public DeviceSetting CreateTestDeviceSetting(string deviceSettingName = TestDeviceSettingName)
        {
            var newTestDeviceSetting = new DeviceSetting
            {
                DeviceSettingName = deviceSettingName,
                DeviceSettingValue = TestDeviceSettingValue,
                OwnedDeviceId = TestOwnedDevice.OwnedDeviceId
            };
            DeviceSettingService.Insert(newTestDeviceSetting);
            newTestDeviceSetting = DeviceSettingService.Search(deviceSettingName).First();
            return newTestDeviceSetting;
        }

        public void DeleteTestDeviceSetting(DeviceSetting deviceSetting)
        {
            DeviceSettingService.DeleteByGuid(deviceSetting.DeviceSettingId);
        }
    }
}
