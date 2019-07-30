using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHunter.Data.Migrations
{
    public partial class OneToOneOfferRealEstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_RealEstates_RealEstateId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_RealEstateId",
                table: "Offers");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_RealEstateId",
                table: "Offers",
                column: "RealEstateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_RealEstates_RealEstateId",
                table: "Offers",
                column: "RealEstateId",
                principalTable: "RealEstates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_RealEstates_RealEstateId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_RealEstateId",
                table: "Offers");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_RealEstateId",
                table: "Offers",
                column: "RealEstateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_RealEstates_RealEstateId",
                table: "Offers",
                column: "RealEstateId",
                principalTable: "RealEstates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
