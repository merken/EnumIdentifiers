using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnumIdentifiers.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionLevels",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    PricePerMonth = table.Column<decimal>(nullable: false),
                    NumberOfSimultaneousDevices = table.Column<int>(nullable: false),
                    NumberDevicesWithDownloadCapability = table.Column<int>(nullable: false),
                    Quality = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionLevels", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Billing = table.Column<string>(nullable: false),
                    SubscriptionLevel = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_SubscriptionLevels_SubscriptionLevel",
                        column: x => x.SubscriptionLevel,
                        principalTable: "SubscriptionLevels",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_SubscriptionLevel",
                table: "Customers",
                column: "SubscriptionLevel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "SubscriptionLevels");
        }
    }
}
