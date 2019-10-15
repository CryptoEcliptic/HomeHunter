using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHunter.Data.Migrations
{
    public partial class NonNullableBools : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Yard",
                table: "RealEstates",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ParkingPlace",
                table: "RealEstates",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Garage",
                table: "RealEstates",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Furnitures",
                table: "RealEstates",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Elevator",
                table: "RealEstates",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Celling",
                table: "RealEstates",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Basement",
                table: "RealEstates",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Yard",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "ParkingPlace",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Garage",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Furnitures",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Elevator",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Celling",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Basement",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
