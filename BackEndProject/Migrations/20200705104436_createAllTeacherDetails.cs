using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndProject.Migrations
{
    public partial class createAllTeacherDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherContacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MyProperty = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Skype = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    Pinterest = table.Column<string>(nullable: true),
                    Twitter = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fullname = table.Column<string>(maxLength: 50, nullable: false),
                    Profession = table.Column<string>(maxLength: 50, nullable: false),
                    About = table.Column<string>(nullable: false),
                    Degree = table.Column<string>(nullable: true),
                    Experience = table.Column<string>(nullable: true),
                    Hobbies = table.Column<string>(nullable: true),
                    Faculty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Language = table.Column<int>(nullable: false),
                    TeamLeader = table.Column<int>(nullable: false),
                    Development = table.Column<int>(nullable: false),
                    Design = table.Column<int>(nullable: false),
                    Innovation = table.Column<int>(nullable: false),
                    Communication = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSkills", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherContacts");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "TeacherSkills");
        }
    }
}
