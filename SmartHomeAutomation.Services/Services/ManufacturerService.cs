using System;
using System.Linq;
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

        public Manufacturer GetByManufacturerGuid(Guid guid)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                return context.Manufacturers.SingleOrDefault(x =>
                    x.ManufacturerId == guid);
            }
        }

        public PagingResult GetDevicesForManufacturer(Guid manufacturerId, int pageSize, int pageNumber, string orderBy, string direction)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                Query = context.Manufacturers
                    .Where(m => m.ManufacturerId == manufacturerId)
                    .Include(d => d.Devices)
                    .AsQueryable();
                var result = PagingHelper.GetPageResult(context, Query, pageSize, pageNumber, orderBy, direction);
                return result;
            }
        }
    }
}
