using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndProject.Migrations
{
    public partial class updateTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "TeacherSkills",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "TeacherContacts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSkills_TeacherId",
                table: "TeacherSkills",
                column: "TeacherId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherContacts_TeacherId",
                table: "TeacherContacts",
                column: "TeacherId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherContacts_Teachers_TeacherId",
                table: "TeacherContacts",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSkills_Teachers_TeacherId",
                table: "TeacherSkills",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherContacts_Teachers_TeacherId",
                table: "TeacherContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSkills_Teachers_TeacherId",
                table: "TeacherSkills");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSkills_TeacherId",
                table: "TeacherSkills");

            migrationBuilder.DropIndex(
                name: "IX_TeacherContacts_TeacherId",
                table: "TeacherContacts");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "TeacherSkills");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "TeacherContacts");
        }
    }
}
