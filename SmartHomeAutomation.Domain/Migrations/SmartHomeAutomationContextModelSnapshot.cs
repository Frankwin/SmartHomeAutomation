﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHomeAutomation.Domain.Models;

namespace SmartHomeAutomation.Domain.Migrations
{
    [DbContext(typeof(SmartHomeAutomationContext))]
    partial class SmartHomeAutomationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.Account.Account", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdatedAt");

                    b.Property<string>("LastUpdatedBy");

                    b.HasKey("AccountId");

                    b.ToTable("Account","Accounts");
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.Device.Device", b =>
                {
                    b.Property<Guid>("DeviceId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("DeviceTypeId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdatedAt");

                    b.Property<string>("LastUpdatedBy");

                    b.Property<Guid>("ManufacturerId");

                    b.HasKey("DeviceId");

                    b.HasIndex("DeviceTypeId");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Device","Devices");
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.Device.DeviceCategory", b =>
                {
                    b.Property<Guid>("DeviceCategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("DeviceCategoryName")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdatedAt");

                    b.Property<string>("LastUpdatedBy");

                    b.HasKey("DeviceCategoryId");

                    b.ToTable("DeviceCategory","Devices");
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.Device.DeviceType", b =>
                {
                    b.Property<Guid>("DeviceTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<Guid>("DeviceCategoryId");

                    b.Property<string>("DeviceTypeName")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdatedAt");

                    b.Property<string>("LastUpdatedBy");

                    b.HasKey("DeviceTypeId");

                    b.HasIndex("DeviceCategoryId");

                    b.ToTable("DeviceType","Devices");
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.Device.Manufacturer", b =>
                {
                    b.Property<Guid>("ManufacturerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdatedAt");

                    b.Property<string>("LastUpdatedBy");

                    b.Property<string>("ManufacturerName")
                        .HasMaxLength(50);

                    b.Property<string>("ManufacturerWebsiteAddress")
                        .HasMaxLength(100);

                    b.HasKey("ManufacturerId");

                    b.ToTable("Manufacturer","Devices");
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.Settings.DeviceSetting", b =>
                {
                    b.Property<Guid>("DeviceSettingId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("DeviceSettingName")
                        .IsRequired();

                    b.Property<string>("DeviceSettingValue")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdatedAt");

                    b.Property<string>("LastUpdatedBy");

                    b.Property<Guid>("OwnedDeviceId");

                    b.HasKey("DeviceSettingId");

                    b.ToTable("DeviceSetting","Settings");
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.Settings.OwnedDevice", b =>
                {
                    b.Property<Guid>("OwnedDeviceId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("DeviceDescription");

                    b.Property<Guid>("DeviceId");

                    b.Property<string>("DeviceName")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdatedAt");

                    b.Property<string>("LastUpdatedBy");

                    b.Property<Guid?>("RoomId");

                    b.HasKey("OwnedDeviceId");

                    b.HasIndex("RoomId");

                    b.ToTable("OwnedDevice","Settings");
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.Settings.Room", b =>
                {
                    b.Property<Guid>("RoomId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdatedAt");

                    b.Property<string>("LastUpdatedBy");

                    b.Property<string>("RoomDescription")
                        .HasMaxLength(200);

                    b.Property<string>("RoomName")
                        .HasMaxLength(50);

                    b.HasKey("RoomId");

                    b.ToTable("Room","Settings");
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.Settings.DeviceSetting", b =>
                {
                    b.Property<Guid>("DeviceSettingId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("DeviceSettingName")
                        .IsRequired();

                    b.Property<string>("DeviceSettingValue")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdatedAt");

                    b.Property<string>("LastUpdatedBy");

                    b.Property<Guid>("OwnedDeviceId");

                    b.HasKey("DeviceSettingId");

                    b.ToTable("DeviceSetting","Settings");
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.Settings.OwnedDevice", b =>
                {
                    b.Property<Guid>("OwnedDeviceId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("DeviceDescription");

                    b.Property<Guid>("DeviceId");

                    b.Property<string>("DeviceName")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdatedAt");

                    b.Property<string>("LastUpdatedBy");

                    b.Property<Guid?>("RoomId");

                    b.HasKey("OwnedDeviceId");

                    b.HasIndex("RoomId");

                    b.ToTable("OwnedDevice","Settings");
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.Settings.Room", b =>
                {
                    b.Property<Guid>("RoomId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdatedAt");

                    b.Property<string>("LastUpdatedBy");

                    b.Property<string>("RoomDescription")
                        .HasMaxLength(200);

                    b.Property<string>("RoomName")
                        .HasMaxLength(50);

                    b.HasKey("RoomId");

                    b.ToTable("Room","Settings");
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.User.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdatedAt");

                    b.Property<string>("LastUpdatedBy");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("UserId");

                    b.HasIndex("AccountId");

                    b.ToTable("User","Users");
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.Device.Device", b =>
                {
                    b.HasOne("SmartHomeAutomation.Domain.Models.Device.DeviceType")
                        .WithMany("Devices")
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmartHomeAutomation.Domain.Models.Device.Manufacturer")
                        .WithMany("Devices")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.Device.DeviceType", b =>
                {
                    b.HasOne("SmartHomeAutomation.Domain.Models.Device.DeviceCategory")
                        .WithMany("DeviceTypes")
                        .HasForeignKey("DeviceCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.Settings.OwnedDevice", b =>
                {
                    b.HasOne("SmartHomeAutomation.Domain.Models.Settings.Room")
                        .WithMany("LinkedDevices")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("SmartHomeAutomation.Domain.Models.User.User", b =>
                {
                    b.HasOne("SmartHomeAutomation.Domain.Models.Account.Account", "Account")
                        .WithMany("Users")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
