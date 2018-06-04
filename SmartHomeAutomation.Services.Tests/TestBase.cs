using System.Linq;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models.Account;
using SmartHomeAutomation.Domain.Models.Device;
using SmartHomeAutomation.Services.Interfaces;
using SmartHomeAutomation.Services.Services;

namespace SmartHomeAutomation.Services.Tests
{
    public class TestBase
    {
        private static readonly ISmartHomeAutomationService SmartHomeAutomationService = new SmartHomeAutomationService();
        public readonly IPrincipal TestUser = new GenericPrincipal(new GenericIdentity("Test Account"), new[] { "Test" });
        public readonly AccountService AccountService = new AccountService(SmartHomeAutomationService);
        public readonly DeviceCategoryService DeviceCategoryService = new DeviceCategoryService(SmartHomeAutomationService);
        public readonly ManufacturerService ManufacturerService = new ManufacturerService(SmartHomeAutomationService);
        public readonly DeviceTypeService DeviceTypeService = new DeviceTypeService(SmartHomeAutomationService);

        public const string TestAccountName = "Test Account";
        public const string TestDeviceCategoryName = "Test Device Category";
        public const string TestManufacturerName = "Test Manufacturer";
        public const string TestDeviceTypeName = "Test Device Type";

        public Account TestAccount { get; set; }
        public DeviceCategory TestDeviceCategory { get; set; }
        public Manufacturer TestManufacturer { get; set; }
        public DeviceType TestDeviceType { get; set; }

        public Account CreateTestAccount(string accountName = TestAccountName)
        {
            var newAccount = new Account { AccountName = accountName };
            AccountService.Insert(newAccount);
            newAccount = AccountService.Search(accountName).First();
            return newAccount;
        }

        public void DeleteTestAccount(Account account)
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
            var deviceCategory = new DeviceCategory { DeviceCategoryName = TestDeviceCategoryName };
            DeviceCategoryService.Insert(deviceCategory);
            deviceCategory = DeviceCategoryService.Search(TestDeviceCategoryName).First();
            return deviceCategory;
        }

        public void DeleteTestDeviceCategory(DeviceCategory deviceCategory)
        {
            DeviceCategoryService.DeleteByGuid(deviceCategory.DeviceCategoryId);
        }

        public Manufacturer CreateTestManufacturer()
        {
            var newManufacturer = new Manufacturer { ManufacturerName = TestManufacturerName };
            ManufacturerService.Insert(newManufacturer);
            return newManufacturer;
        }

        public void DeleteTestManufacturer(Manufacturer manufacturer)
        {
            ManufacturerService.DeleteByGuid(manufacturer.ManufacturerId);
        }

        public DeviceType CreateTestDeviceType()
        {
            TestDeviceCategory = CreateTestDeviceCategory();

            var newDeviceType = new DeviceType { DeviceTypeName = TestDeviceTypeName, DeviceCategoryId = TestDeviceCategory.DeviceCategoryId};
            DeviceTypeService.Insert(newDeviceType);
            return newDeviceType;
        }

        public void DeleteTestDeviceType(DeviceType deviceType)
        {
            DeviceTypeService.DeleteByGuid(deviceType.DeviceTypeId);
            DeviceCategoryService.DeleteByGuid(TestDeviceCategory.DeviceCategoryId);
        }
    }
}
