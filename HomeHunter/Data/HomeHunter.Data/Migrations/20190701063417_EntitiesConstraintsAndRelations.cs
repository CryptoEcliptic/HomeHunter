using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHunter.Data.Migrations
{
    public partial class EntitiesConstraintsAndRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Municipalities_MunicipalityId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AspNetUsers_UserId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Villages_Municipalities_MunicipalityId",
                table: "Villages");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Offers",
                newName: "RealEstateId1");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_UserId",
                table: "Offers",
                newName: "IX_Offers_RealEstateId1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Villages",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MunicipalityId",
                table: "Villages",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FloorNumber",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RealEstateTypeId",
                table: "RealEstates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Offers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OfferType",
                table: "Offers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RealEstateId",
                table: "Offers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                table: "Offers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Neighbourhoods",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Municipalities",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Countries",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MunicipalityId",
                table: "Cities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "RealEstateTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    TypeName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_RealEstateTypeId",
                table: "RealEstates",
                column: "RealEstateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_AuthorId",
                table: "Offers",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Municipalities_MunicipalityId",
                table: "Cities",
                column: "MunicipalityId",
                principalTable: "Municipalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AspNetUsers_AuthorId",
                table: "Offers",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_RealEstates_RealEstateId1",
                table: "Offers",
                column: "RealEstateId1",
                principalTable: "RealEstates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_RealEstateTypes_RealEstateTypeId",
                table: "RealEstates",
                column: "RealEstateTypeId",
                principalTable: "RealEstateTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Villages_Municipalities_MunicipalityId",
                table: "Villages",
                column: "MunicipalityId",
                principalTable: "Municipalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Municipalities_MunicipalityId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AspNetUsers_AuthorId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_RealEstates_RealEstateId1",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_RealEstateTypes_RealEstateTypeId",
                table: "RealEstates");

            migrationBuilder.DropForeignKey(
                name: "FK_Villages_Municipalities_MunicipalityId",
                table: "Villages");

            migrationBuilder.DropTable(
                name: "RealEstateTypes");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_RealEstateTypeId",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_Offers_AuthorId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "RealEstateTypeId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OfferType",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "RealEstateId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "RealEstateId1",
                table: "Offers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_RealEstateId1",
                table: "Offers",
                newName: "IX_Offers_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Villages",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "MunicipalityId",
                table: "Villages",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "RealEstates",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FloorNumber",
                table: "RealEstates",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Neighbourhoods",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Municipalities",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Countries",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "MunicipalityId",
                table: "Cities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Municipalities_MunicipalityId",
                table: "Cities",
                column: "MunicipalityId",
                principalTable: "Municipalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AspNetUsers_UserId",
                table: "Offers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Villages_Municipalities_MunicipalityId",
                table: "Villages",
                column: "MunicipalityId",
                principalTable: "Municipalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
