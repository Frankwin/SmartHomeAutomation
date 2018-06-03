using System.Collections.Generic;
using System.Linq;
using SmartHomeAutomation.Domain.Models;
using SmartHomeAutomation.Domain.Models.Device;

namespace SmartHomeAutomation.Domain.SeedData
{
    internal static class SeedDevices
    {
        public static void Seed(SmartHomeAutomationContext dbContext)
        {
            var deviceTypes = dbContext.DeviceTypes;
            var manufactures = dbContext.Manufacturers;

            var devices = new List<Device>();
            foreach (var deviceType in deviceTypes)
            {
                var ecoBee = manufactures.FirstOrDefault(m => m.ManufacturerName == "EcoBee");
                var nest = manufactures.FirstOrDefault(m => m.ManufacturerName == "Nest");
                var hue = manufactures.FirstOrDefault(m => m.ManufacturerName == "Philips Hue");
                switch (deviceType.DeviceTypeName)
                {
                    case "Thermostat":
                        if (ecoBee != null)
                        {
                            devices.Add(new Device
                            {
                                DeviceName = "EcoBee 3",
                                DeviceTypeId = deviceType.DeviceTypeId,
                                ManufacturerId = ecoBee.ManufacturerId
                            });
                        }
                        if (nest != null)
                        {
                            devices.Add(new Device
                            {
                                DeviceName = "Nest Series 4",
                                DeviceTypeId = deviceType.DeviceTypeId,
                                ManufacturerId = nest.ManufacturerId
                            });
                        }
                        break;
                    case "Temperature Sensor":
                        if (ecoBee != null)
                        {
                            devices.Add(new Device
                            {
                                DeviceName = "EcoBee Sensor",
                                DeviceTypeId = deviceType.DeviceTypeId,
                                ManufacturerId = ecoBee.ManufacturerId
                            });
                        }
                        if (nest != null)
                        {
                            devices.Add(new Device
                            {
                                DeviceName = "Nest Sensor",
                                DeviceTypeId = deviceType.DeviceTypeId,
                                ManufacturerId = nest.ManufacturerId
                            });
                        }
                        break;
                    case "Lightbulb - White":
                        if (hue != null)
                        {
                            devices.Add(new Device
                            {
                                DeviceName = "Hue White Smart LED Bulb - E26",
                                DeviceTypeId = deviceType.DeviceTypeId,
                                ManufacturerId = hue.ManufacturerId
                            });
                            devices.Add(new Device
                            {
                                DeviceName = "Hue White Ambiance Smart LED Bulb - E26",
                                DeviceTypeId = deviceType.DeviceTypeId,
                                ManufacturerId = hue.ManufacturerId
                            });
                        }
                        break;
                    case "LightBulb - Color":
                        if (hue != null)
                        {
                            devices.Add(new Device
                            {
                                DeviceName = "Hue White & Color Ambiance Smart LED Bulb - E26",
                                DeviceTypeId = deviceType.DeviceTypeId,
                                ManufacturerId = hue.ManufacturerId
                            });
                            devices.Add(new Device
                            {
                                DeviceName = "Hue White & Color Ambiance Smart LED Bulb - E12",
                                DeviceTypeId = deviceType.DeviceTypeId,
                                ManufacturerId = hue.ManufacturerId
                            });
                        }
                        break;
                }
            }

            dbContext.Devices.AddRange(devices);
            dbContext.SaveChanges();
        }
    }
}
