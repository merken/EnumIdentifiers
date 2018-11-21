using Microsoft.EntityFrameworkCore.Migrations;

namespace EnumIdentifiers.Data.Migrations
{
    public partial class Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SubscriptionLevels",
                columns: new[] { "Name", "NumberDevicesWithDownloadCapability", "NumberOfSimultaneousDevices", "PricePerMonth", "Quality" },
                values: new object[] { "Basic", 1, 1, 7.99m, "Standard" });

            migrationBuilder.InsertData(
                table: "SubscriptionLevels",
                columns: new[] { "Name", "NumberDevicesWithDownloadCapability", "NumberOfSimultaneousDevices", "PricePerMonth", "Quality" },
                values: new object[] { "Standard", 2, 2, 10.99m, "HD" });

            migrationBuilder.InsertData(
                table: "SubscriptionLevels",
                columns: new[] { "Name", "NumberDevicesWithDownloadCapability", "NumberOfSimultaneousDevices", "PricePerMonth", "Quality" },
                values: new object[] { "Premium", 4, 4, 13.99m, "UHD" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubscriptionLevels",
                keyColumn: "Name",
                keyValue: "Basic");

            migrationBuilder.DeleteData(
                table: "SubscriptionLevels",
                keyColumn: "Name",
                keyValue: "Standard");

            migrationBuilder.DeleteData(
                table: "SubscriptionLevels",
                keyColumn: "Name",
                keyValue: "Premium");
        }
    }
}
