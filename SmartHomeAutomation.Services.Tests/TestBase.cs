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

        public Account TestAccount { get; set; }
        public DeviceCategory TestDeviceCategory { get; set; }
        public Manufacturer TestManufacturer { get; set; }

        public Account CreateTestAccount()
        {
            var newAccount = new Account { AccountName = "TestAccount" };
            AccountService.Insert(newAccount);
            return newAccount;
        }

        public void DeleteTestAccount(Account account)
        {
            AccountService.DeleteByGuid(account.AccountId);
        }

        public DeviceCategory CreateTestDeviceCategory()
        {
            var deviceCategory = new DeviceCategory { DeviceCategoryName = "TestDeviceCategory" };
            DeviceCategoryService.Insert(deviceCategory);
            return deviceCategory;
        }

        public void DeleteTestAccount(DeviceCategory deviceCategory)
        {
            DeviceCategoryService.DeleteByGuid(deviceCategory.DeviceCategoryId);
        }

        public Manufacturer CreateTestManufacturer()
        {
            var newManufacturer = new Manufacturer { ManufacturerName = "Test Manufacturer" };
            ManufacturerService.Insert(newManufacturer);
            return newManufacturer;
        }

        public void DeleteTestManufacturer(Manufacturer manufacturer)
        {
            ManufacturerService.DeleteByGuid(manufacturer.ManufacturerId);
        }
    }
}
