using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SmartHomeAutomation.Web.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Accounts");

            migrationBuilder.EnsureSchema(
                name: "Devices");

            migrationBuilder.EnsureSchema(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(nullable: false),
                    AccountName = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(maxLength: 1, nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
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
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeviceCategoryName = table.Column<string>(nullable: false),
                    Status = table.Column<string>(maxLength: 1, nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
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
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ManufacturerName = table.Column<string>(maxLength: 50, nullable: true),
                    ManufacturerWebsiteAddress = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<string>(maxLength: 1, nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.ManufacturerId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Status = table.Column<string>(maxLength: 1, nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UserName = table.Column<string>(maxLength: 30, nullable: false)
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
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeviceCategoryId = table.Column<Guid>(nullable: false),
                    DeviceTypeName = table.Column<string>(nullable: false),
                    Status = table.Column<string>(maxLength: 1, nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
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
                name: "Device",
                schema: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeviceName = table.Column<string>(maxLength: 100, nullable: false),
                    DeviceTypeId = table.Column<Guid>(nullable: false),
                    ManufacturerId = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(maxLength: 1, nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
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
                name: "User",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "DeviceType",
                schema: "Devices");

            migrationBuilder.DropTable(
                name: "Manufacturer",
                schema: "Devices");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "DeviceCategory",
                schema: "Devices");
        }
    }
}
