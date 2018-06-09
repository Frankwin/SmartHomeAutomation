using System;
using System.Data;
using System.Linq;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using SmartHomeAutomation.Domain.Models;
using SmartHomeAutomation.Domain.Models.DeviceModels;
using SmartHomeAutomation.Services.Helpers;
using SmartHomeAutomation.Services.Interfaces;
using SmartHomeAutomation.Services.Interfaces.Device;

namespace SmartHomeAutomation.Services.Services.Device
{
    public class DeviceTypeService : BaseService<DeviceType, SmartHomeAutomationContext>, IDeviceTypeService
    {
        private IQueryable<DeviceType> Query { get; set; }

        public DeviceTypeService(ISmartHomeAutomationService smartHomeAutomationService) : base(smartHomeAutomationService.ConnectionString)
        {
        }

        public PageResult GetAllDevicesForDeviceTypeByPage(Guid deviceType, int pageSize, int currentPage, string sortOrder, string direction)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                Query = context.DeviceTypes
                    .Include(d => d.Devices)
                    .Where(d => d.Devices.Any(dt => dt.DeviceType.DeviceTypeId == deviceType))
                    .AsQueryable();
                return PagingHelper.GetPageResult(context, Query, pageSize, currentPage, sortOrder, direction);
            }
        }

        public DeviceType Upsert(DeviceType deviceType, IPrincipal userPrincipal)
        {
            var existingDeviceType = CheckForExistingDeviceType(deviceType.DeviceTypeName);
            if (existingDeviceType != null)
            {
                if (existingDeviceType.IsDeleted)
                {
                    existingDeviceType.IsDeleted = false;
                    deviceType = existingDeviceType;
                }
                else
                {
                    throw new DuplicateNameException($"A device type with name '{deviceType.DeviceTypeName}' already exists");
                }
            }

            if (deviceType.DeviceTypeId == Guid.Empty)
            {
                deviceType.SetInsertInfo(userPrincipal);
                Insert(deviceType);
            }
            else
            {
                deviceType.SetUpdateInfo(userPrincipal);
                Update(deviceType);
            }

            return deviceType;
        }

        public DeviceType SoftDelete(Guid guid, IPrincipal userPrincipal)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                var deviceType = context.DeviceTypes.FirstOrDefault(dc => dc.DeviceTypeId == guid);
                if (deviceType == null)
                {
                    throw new ArgumentException($"Device Type with ID {guid} does not exist");
                }
                deviceType.SetDeleteInfo(userPrincipal);
                Update(deviceType);
                return deviceType;
            }
        }

        public DeviceType CheckForExistingDeviceType(string deviceTypeName)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                return context.DeviceTypes.FirstOrDefault(x => x.DeviceTypeName.Equals(deviceTypeName, StringComparison.CurrentCultureIgnoreCase));
            }
        }
    }
}
