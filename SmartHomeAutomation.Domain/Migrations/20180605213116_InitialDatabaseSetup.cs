using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHomeAutomation.Domain.Migrations
{
    public partial class InitialDatabaseSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Accounts");

            migrationBuilder.EnsureSchema(
                name: "Devices");

            migrationBuilder.EnsureSchema(
                name: "Settings");

            migrationBuilder.EnsureSchema(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(nullable: false),
                    AccountName = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCategory",
                schema: "Devices",
                columns: table => new
                {
                    DeviceCategoryId = table.Column<Guid>(nullable: false),
                    DeviceCategoryName = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCategory", x => x.DeviceCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                schema: "Devices",
                columns: table => new
                {
                    ManufacturerId = table.Column<Guid>(nullable: false),
                    ManufacturerName = table.Column<string>(maxLength: 50, nullable: true),
                    ManufacturerWebsiteAddress = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.ManufacturerId);
                });

            migrationBuilder.CreateTable(
                name: "DeviceSetting",
                schema: "Settings",
                columns: table => new
                {
                    DeviceSettingId = table.Column<Guid>(nullable: false),
                    OwnedDeviceId = table.Column<Guid>(nullable: false),
                    DeviceSettingName = table.Column<string>(nullable: false),
                    DeviceSettingValue = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceSetting", x => x.DeviceSettingId);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                schema: "Settings",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(nullable: false),
                    RoomName = table.Column<string>(maxLength: 50, nullable: true),
                    RoomDescription = table.Column<string>(maxLength: 200, nullable: true),
                    AccountId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Accounts",
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceType",
                schema: "Devices",
                columns: table => new
                {
                    DeviceTypeId = table.Column<Guid>(nullable: false),
                    DeviceTypeName = table.Column<string>(nullable: false),
                    DeviceCategoryId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceType", x => x.DeviceTypeId);
                    table.ForeignKey(
                        name: "FK_DeviceType_DeviceCategory_DeviceCategoryId",
                        column: x => x.DeviceCategoryId,
                        principalSchema: "Devices",
                        principalTable: "DeviceCategory",
                        principalColumn: "DeviceCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnedDevice",
                schema: "Settings",
                columns: table => new
                {
                    OwnedDeviceId = table.Column<Guid>(nullable: false),
                    DeviceId = table.Column<Guid>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: true),
                    DeviceName = table.Column<string>(nullable: false),
                    DeviceDescription = table.Column<string>(nullable: true),
                    AccountId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnedDevice", x => x.OwnedDeviceId);
                    table.ForeignKey(
                        name: "FK_OwnedDevice_Room_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "Settings",
                        principalTable: "Room",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                schema: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(nullable: false),
                    DeviceName = table.Column<string>(maxLength: 100, nullable: false),
                    DeviceTypeId = table.Column<Guid>(nullable: false),
                    ManufacturerId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Device_DeviceType_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalSchema: "Devices",
                        principalTable: "DeviceType",
                        principalColumn: "DeviceTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Device_Manufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalSchema: "Devices",
                        principalTable: "Manufacturer",
                        principalColumn: "ManufacturerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Device_DeviceTypeId",
                schema: "Devices",
                table: "Device",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_ManufacturerId",
                schema: "Devices",
                table: "Device",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceType_DeviceCategoryId",
                schema: "Devices",
                table: "DeviceType",
                column: "DeviceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedDevice_RoomId",
                schema: "Settings",
                table: "OwnedDevice",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_User_AccountId",
                schema: "Users",
                table: "User",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Device",
                schema: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceSetting",
                schema: "Settings");

            migrationBuilder.DropTable(
                name: "OwnedDevice",
                schema: "Settings");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "DeviceType",
                schema: "Devices");

            migrationBuilder.DropTable(
                name: "Manufacturer",
                schema: "Devices");

            migrationBuilder.DropTable(
                name: "Room",
                schema: "Settings");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "DeviceCategory",
                schema: "Devices");
        }
    }
}
