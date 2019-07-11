using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHunter.Data.Migrations
{
    public partial class City_Neighbourhood_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Neighbourhoods",
                nullable: true);

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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
