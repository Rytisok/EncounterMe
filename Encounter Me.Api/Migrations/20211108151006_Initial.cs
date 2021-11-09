using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Encounter_Me.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoredSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserPhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ExperiencePoints = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "ExperiencePoints", "FirstName", "LastName", "Level", "Password", "StoredSalt", "UserName", "UserPhotoUrl" },
                values: new object[] { 1, "dz@mail.lt", 0.0, "Dominykas", "Zagreckas", 1, null, null, "obuolys", "https://i.pinimg.com/originals/83/6d/69/836d69f49e80af2825c7db264be44af0.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
