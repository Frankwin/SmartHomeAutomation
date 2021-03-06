﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using SmartHomeAutomation.Domain.SeedData;

namespace SmartHomeAutomation.Domain.Models
{
    public static class SmartHomeAutomationContextExtension
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this SmartHomeAutomationContext context)
        {
            // Load Manufacturers
            if (!context.Manufacturers.Any())
            {
                SeedManufacturers.Seed(context);
            }

            // Load Device Categories
            if (!context.DeviceCategories.Any())
            {
                SeedDeviceCategories.Seed(context);
            }

            // Load Device Types
            if (!context.DeviceTypes.Any())
            {
                SeedDeviceTypes.Seed(context);
            }

            // Load Devices
            if (!context.Devices.Any())
            {
                SeedDevices.Seed(context);
            }
        }
    }
}