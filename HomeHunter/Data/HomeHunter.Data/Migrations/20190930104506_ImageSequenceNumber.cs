using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHunter.Data.Migrations
{
    public partial class ImageSequenceNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIndexPicture",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "Images",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "Images");

            migrationBuilder.AddColumn<bool>(
                name: "IsIndexPicture",
                table: "Images",
                nullable: false,
                defaultValue: false);
        }
    }
}
