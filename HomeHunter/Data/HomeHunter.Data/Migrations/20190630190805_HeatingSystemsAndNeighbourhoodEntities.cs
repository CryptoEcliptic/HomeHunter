using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHunter.Data.Migrations
{
    public partial class HeatingSystemsAndNeighbourhoodEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Cities_CityId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_RealEstate_RealEstateId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Villages_VillageId",
                table: "Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RealEstate",
                table: "RealEstate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CentralHeatning",
                table: "RealEstate");

            migrationBuilder.DropColumn(
                name: "LicenceForExploatation",
                table: "RealEstate");

            migrationBuilder.RenameTable(
                name: "RealEstate",
                newName: "RealEstates");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_Address_VillageId",
                table: "Addresses",
                newName: "IX_Addresses_VillageId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_RealEstateId",
                table: "Addresses",
                newName: "IX_Addresses_RealEstateId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_CityId",
                table: "Addresses",
                newName: "IX_Addresses_CityId");

            migrationBuilder.AlterColumn<bool>(
                name: "ParkingPlace",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "CellingOrBasement",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Balcony",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<int>(
                name: "HeatingSystemId",
                table: "RealEstates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "RealEstates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NeighbourhoodId",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RealEstates",
                table: "RealEstates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "HeatingSystems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeatingSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Neighbourhoods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighbourhoods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_HeatingSystemId",
                table: "RealEstates",
                column: "HeatingSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_NeighbourhoodId",
                table: "Addresses",
                column: "NeighbourhoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Cities_CityId",
                table: "Addresses",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Neighbourhoods_NeighbourhoodId",
                table: "Addresses",
                column: "NeighbourhoodId",
                principalTable: "Neighbourhoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_RealEstates_RealEstateId",
                table: "Addresses",
                column: "RealEstateId",
                principalTable: "RealEstates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Villages_VillageId",
                table: "Addresses",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_HeatingSystems_HeatingSystemId",
                table: "RealEstates",
                column: "HeatingSystemId",
                principalTable: "HeatingSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Cities_CityId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Neighbourhoods_NeighbourhoodId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_RealEstates_RealEstateId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Villages_VillageId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_HeatingSystems_HeatingSystemId",
                table: "RealEstates");

            migrationBuilder.DropTable(
                name: "HeatingSystems");

            migrationBuilder.DropTable(
                name: "Neighbourhoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RealEstates",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_HeatingSystemId",
                table: "RealEstates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_NeighbourhoodId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "HeatingSystemId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "NeighbourhoodId",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "RealEstates",
                newName: "RealEstate");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_VillageId",
                table: "Address",
                newName: "IX_Address_VillageId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_RealEstateId",
                table: "Address",
                newName: "IX_Address_RealEstateId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CityId",
                table: "Address",
                newName: "IX_Address_CityId");

            migrationBuilder.AlterColumn<bool>(
                name: "ParkingPlace",
                table: "RealEstate",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "CellingOrBasement",
                table: "RealEstate",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Balcony",
                table: "RealEstate",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CentralHeatning",
                table: "RealEstate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LicenceForExploatation",
                table: "RealEstate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RealEstate",
                table: "RealEstate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Cities_CityId",
                table: "Address",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_RealEstate_RealEstateId",
                table: "Address",
                column: "RealEstateId",
                principalTable: "RealEstate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Villages_VillageId",
                table: "Address",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
