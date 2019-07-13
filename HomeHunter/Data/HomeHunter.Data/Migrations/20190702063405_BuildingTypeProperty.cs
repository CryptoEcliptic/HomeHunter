using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHunter.Data.Migrations
{
    public partial class BuildingTypeProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FloorNumber",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuildingType",
                table: "RealEstates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingType",
                table: "RealEstates");

            migrationBuilder.AlterColumn<int>(
                name: "FloorNumber",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
