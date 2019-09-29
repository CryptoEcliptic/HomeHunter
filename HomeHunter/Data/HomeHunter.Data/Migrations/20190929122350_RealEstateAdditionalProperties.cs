using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHunter.Data.Migrations
{
    public partial class RealEstateAdditionalProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MetroNearBy",
                table: "RealEstates",
                newName: "Garage");

            migrationBuilder.RenameColumn(
                name: "CellingOrBasement",
                table: "RealEstates",
                newName: "Furnitures");

            migrationBuilder.RenameColumn(
                name: "Balcony",
                table: "RealEstates",
                newName: "Elevator");

            migrationBuilder.AddColumn<bool>(
                name: "Basement",
                table: "RealEstates",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Celling",
                table: "RealEstates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Basement",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "Celling",
                table: "RealEstates");

            migrationBuilder.RenameColumn(
                name: "Garage",
                table: "RealEstates",
                newName: "MetroNearBy");

            migrationBuilder.RenameColumn(
                name: "Furnitures",
                table: "RealEstates",
                newName: "CellingOrBasement");

            migrationBuilder.RenameColumn(
                name: "Elevator",
                table: "RealEstates",
                newName: "Balcony");
        }
    }
}
