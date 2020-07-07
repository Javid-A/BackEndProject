using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndProject.Migrations
{
    public partial class createAllCourseDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImagePath = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseContents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AboutCourse = table.Column<string>(nullable: false),
                    HTA = table.Column<string>(nullable: true),
                    Certification = table.Column<string>(nullable: true),
                    CourseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseContents_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Starts = table.Column<string>(maxLength: 20, nullable: false),
                    Duration = table.Column<string>(maxLength: 20, nullable: false),
                    ClassDuration = table.Column<string>(maxLength: 20, nullable: false),
                    SkillLevel = table.Column<string>(maxLength: 20, nullable: false),
                    Langugage = table.Column<string>(maxLength: 50, nullable: false),
                    Students = table.Column<int>(nullable: false),
                    Assesments = table.Column<string>(nullable: true),
                    Fee = table.Column<double>(nullable: false),
                    CourseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFeatures_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseContents_CourseId",
                table: "CourseContents",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFeatures_CourseId",
                table: "CourseFeatures",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseContents");

            migrationBuilder.DropTable(
                name: "CourseFeatures");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
