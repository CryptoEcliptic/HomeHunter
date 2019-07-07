using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHunter.Data.Migrations
{
    public partial class AddedBuildingTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuildingType",
                table: "RealEstates",
                newName: "BuildingTypeId");

            migrationBuilder.CreateTable(
                name: "BuildingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_BuildingTypeId",
                table: "RealEstates",
                column: "BuildingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_BuildingTypes_BuildingTypeId",
                table: "RealEstates",
                column: "BuildingTypeId",
                principalTable: "BuildingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_BuildingTypes_BuildingTypeId",
                table: "RealEstates");

            migrationBuilder.DropTable(
                name: "BuildingTypes");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_BuildingTypeId",
                table: "RealEstates");

            migrationBuilder.RenameColumn(
                name: "BuildingTypeId",
                table: "RealEstates",
                newName: "BuildingType");
        }
    }
}
