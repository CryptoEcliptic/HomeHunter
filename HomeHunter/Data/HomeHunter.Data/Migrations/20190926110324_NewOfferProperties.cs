using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHunter.Data.Migrations
{
    public partial class NewOfferProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgentName",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfferServiceInformation",
                table: "Offers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentName",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OfferServiceInformation",
                table: "Offers");
        }
    }
}
