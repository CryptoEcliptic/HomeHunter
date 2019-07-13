using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHunter.Data.Migrations
{
    public partial class Neighbourhoods_Addresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Neighbourhoods_Cities_CityId",
                table: "Neighbourhoods");

            migrationBuilder.DropIndex(
                name: "IX_Neighbourhoods_CityId",
                table: "Neighbourhoods");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Neighbourhoods");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Neighbourhoods",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Neighbourhoods_CityId",
                table: "Neighbourhoods",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Neighbourhoods_Cities_CityId",
                table: "Neighbourhoods",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
