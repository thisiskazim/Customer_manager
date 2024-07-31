using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customer_manager.Migrations
{
    public partial class UpdateNewPhotoPropertys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Portfolyo",
                newName: "PhotoPath");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "About",
                newName: "PhotoPath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "Portfolyo",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "About",
                newName: "Photo");
        }
    }
}
