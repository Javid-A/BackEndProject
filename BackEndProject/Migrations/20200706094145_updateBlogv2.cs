using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndProject.Migrations
{
    public partial class updateBlogv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ByWho",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Blogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AppUserId1",
                table: "Blogs",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_AppUserId1",
                table: "Blogs",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_AppUserId1",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_AppUserId1",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ByWho",
                table: "Blogs",
                nullable: false,
                defaultValue: "");
        }
    }
}
