using System;
using System.Data;
using System.Linq;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using SmartHomeAutomation.Domain.Models;
using SmartHomeAutomation.Domain.Models.Device;
using SmartHomeAutomation.Services.Helpers;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services
{
    public class ManufacturerService : Service<Manufacturer, SmartHomeAutomationContext>, IManufacturerService
    {
        private IQueryable<Manufacturer> Query { get; set; }

        public ManufacturerService(ISmartHomeAutomationService smartHomeAutomationService) : base (smartHomeAutomationService.ConnectionString)
        {
            
        }

        public PagingResult GetManufacturersByPage(int pageSize, int pageNumber, string orderBy, string direction)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                Query = context.Manufacturers
                    .Include(a => a.Devices)
                    .AsQueryable();

                var result = PagingHelper.GetPageResult(context, Query, pageSize, pageNumber, orderBy, direction);
                return result;
            }
        }

        public Manufacturer GetByManufacturerGuid(Guid manufacturerGuid)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                return context.Manufacturers
                    .SingleOrDefault(x => x.ManufacturerId == manufacturerGuid);
            }
        }

        public Manufacturer Upsert(Manufacturer manufacturer, IPrincipal userPrincipal)
        {
            var existingManufacturer = CheckForExistingManufacturerName(manufacturer.ManufacturerName);
            if (existingManufacturer != null)
            {
                if (existingManufacturer.IsDeleted)
                {
                    existingManufacturer.ManufacturerWebsiteAddress = manufacturer.ManufacturerWebsiteAddress;
                    existingManufacturer.IsDeleted = false;
                    manufacturer = existingManufacturer;
                }
                else
                {
                    throw new DuplicateNameException($"A manufacturer with name '{manufacturer.ManufacturerName}' already exists");
                }
            }

            if (manufacturer.ManufacturerId == Guid.Empty)
            {
                manufacturer.SetInsertInfo(userPrincipal);
                Insert(manufacturer);
            }
            else
            {
                manufacturer.SetUpdateInfo(userPrincipal);
                Update(manufacturer);
            }

            return manufacturer;
        }

        public Manufacturer SoftDelete(Guid guid, IPrincipal userPrincipal)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                var manufacturer = context.Manufacturers.FirstOrDefault(dc => dc.ManufacturerId == guid);
                if (manufacturer == null)
                {
                    throw new ArgumentException($"Manufacturer with ID {guid} does not exist");
                }
                manufacturer.SetDeleteInfo(userPrincipal);
                Update(manufacturer);
                return manufacturer;
            }
        }

        public Manufacturer CheckForExistingManufacturerName(string name)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                return context.Manufacturers.FirstOrDefault(x => x.ManufacturerName.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            }
        }
    }
}
