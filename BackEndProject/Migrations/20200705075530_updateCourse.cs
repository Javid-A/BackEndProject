using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndProject.Migrations
{
    public partial class updateCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseContents_Courses_CourseId",
                table: "CourseContents");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFeatures_Courses_CourseId",
                table: "CourseFeatures");

            migrationBuilder.DropIndex(
                name: "IX_CourseFeatures_CourseId",
                table: "CourseFeatures");

            migrationBuilder.DropIndex(
                name: "IX_CourseContents_CourseId",
                table: "CourseContents");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseFeatures",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseContents",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseFeatures_CourseId",
                table: "CourseFeatures",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseContents_CourseId",
                table: "CourseContents",
                column: "CourseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseContents_Courses_CourseId",
                table: "CourseContents",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeatures_Courses_CourseId",
                table: "CourseFeatures",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseContents_Courses_CourseId",
                table: "CourseContents");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFeatures_Courses_CourseId",
                table: "CourseFeatures");

            migrationBuilder.DropIndex(
                name: "IX_CourseFeatures_CourseId",
                table: "CourseFeatures");

            migrationBuilder.DropIndex(
                name: "IX_CourseContents_CourseId",
                table: "CourseContents");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseFeatures",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseContents",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_CourseFeatures_CourseId",
                table: "CourseFeatures",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseContents_CourseId",
                table: "CourseContents",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseContents_Courses_CourseId",
                table: "CourseContents",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeatures_Courses_CourseId",
                table: "CourseFeatures",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
