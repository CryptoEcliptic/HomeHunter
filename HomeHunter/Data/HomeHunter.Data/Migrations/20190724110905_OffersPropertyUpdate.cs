using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHunter.Data.Migrations
{
    public partial class OffersPropertyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_RealEstates_RealEstateId1",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_RealEstateId1",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "RealEstateId1",
                table: "Offers",
                newName: "ContactNumber");

            migrationBuilder.AlterColumn<string>(
                name: "RealEstateId",
                table: "Offers",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Offers",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "ContactNumber",
                table: "Offers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_RealEstates_RealEstateId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_RealEstateId",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "ContactNumber",
                table: "Offers",
                newName: "RealEstateId1");

            migrationBuilder.AlterColumn<int>(
                name: "RealEstateId",
                table: "Offers",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Offers",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "RealEstateId1",
                table: "Offers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_RealEstateId1",
                table: "Offers",
                column: "RealEstateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_RealEstates_RealEstateId1",
                table: "Offers",
                column: "RealEstateId1",
                principalTable: "RealEstates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
