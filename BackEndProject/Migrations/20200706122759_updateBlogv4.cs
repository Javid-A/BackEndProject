using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndProject.Migrations
{
    public partial class updateBlogv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CommentCount",
                table: "Blogs",
                nullable: false,
                defaultValue:0,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CommentCount",
                table: "Blogs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
