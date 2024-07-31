using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customer_manager.Migrations
{
    public partial class UpdateCustomerKeyRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInfos_AspNetUsers_CustomerKey",
                table: "CustomerInfos");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_CustomerKey",
                table: "AspNetUsers",
                column: "CustomerKey");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInfos_AspNetUsers_CustomerKey",
                table: "CustomerInfos",
                column: "CustomerKey",
                principalTable: "AspNetUsers",
                principalColumn: "CustomerKey",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInfos_AspNetUsers_CustomerKey",
                table: "CustomerInfos");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_CustomerKey",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInfos_AspNetUsers_CustomerKey",
                table: "CustomerInfos",
                column: "CustomerKey",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
