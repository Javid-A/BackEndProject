using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndProject.Migrations
{
    public partial class addPolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Courses",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LogoPath",
                table: "Bios",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AboutPath",
                table: "Bios",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AppUserId",
                table: "Courses",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_AppUserId",
                table: "Courses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_AppUserId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_AppUserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "LogoPath",
                table: "Bios",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "AboutPath",
                table: "Bios",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
