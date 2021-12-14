using Microsoft.EntityFrameworkCore.Migrations;

namespace Imengur.Migrations
{
    public partial class identityRepairs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Userss_UserId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Userss_UserId",
                table: "Images",
                column: "UserId",
                principalTable: "Userss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Userss_UserId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Userss_UserId",
                table: "Images",
                column: "UserId",
                principalTable: "Userss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
