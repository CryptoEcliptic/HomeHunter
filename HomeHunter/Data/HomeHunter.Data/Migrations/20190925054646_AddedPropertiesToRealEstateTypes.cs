using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHunter.Data.Migrations
{
    public partial class AddedPropertiesToRealEstateTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaxReferenceNumber",
                table: "RealEstateTypes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MinReferenceNumber",
                table: "RealEstateTypes",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxReferenceNumber",
                table: "RealEstateTypes");

            migrationBuilder.DropColumn(
                name: "MinReferenceNumber",
                table: "RealEstateTypes");
        }
    }
}
