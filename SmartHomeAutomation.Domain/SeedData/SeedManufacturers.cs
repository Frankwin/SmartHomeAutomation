using System.Collections.Generic;
using SmartHomeAutomation.Domain.Models;
using SmartHomeAutomation.Domain.Models.DeviceModels;

namespace SmartHomeAutomation.Domain.SeedData
{
    internal static class SeedManufacturers
    {
        public static void Seed(SmartHomeAutomationContext dbContext)
        {
            var manufacturers = new List<Manufacturer>
            {
                new Manufacturer
                {
                    ManufacturerName = "Philips Hue",
                    ManufacturerWebsiteAddress = "http://meethue.com"
                },
                new Manufacturer
                {
                    ManufacturerName = "EcoBee",
                    ManufacturerWebsiteAddress = "http://www.ecobee.com"
                },
                new Manufacturer
                {
                    ManufacturerName = "Nest",
                    ManufacturerWebsiteAddress = "http://nest.com"
                }
            };
            dbContext.Manufacturers.AddRange(manufacturers);
            dbContext.SaveChanges();
        }
    }
}
