using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sklep_MVC_Projekt.Migrations
{
    public partial class _4567 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_AspNetUsers_Id",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Customer",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_AspNetUsers_Id",
                table: "Customer",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_AspNetUsers_Id",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Customer",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_AspNetUsers_Id",
                table: "Customer",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
