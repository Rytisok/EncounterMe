using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Encounter_Me.Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Diff = table.Column<int>(type: "int", nullable: false),
                    GeoJsonData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trailType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faction = table.Column<int>(type: "int", nullable: true),
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
                table: "Trails",
                columns: new[] { "Id", "Diff", "GeoJsonData", "Lat", "Length", "Lng", "trailType" },
                values: new object[] { 1, 2, "sample-data/test.geojson", 54.721400000000003, 2.3999999999999999, 25.255500000000001, 0 });

            migrationBuilder.InsertData(
                table: "Trails",
                columns: new[] { "Id", "Diff", "GeoJsonData", "Lat", "Length", "Lng", "trailType" },
                values: new object[] { 2, 3, "sample-data/test1.geojson", 54.685002361652998, 3.0, 25.240305662154999, 1 });

            migrationBuilder.InsertData(
                table: "Trails",
                columns: new[] { "Id", "Diff", "GeoJsonData", "Lat", "Length", "Lng", "trailType" },
                values: new object[] { 3, 1, "sample-data/test2.geojson", 54.686813299999997, 1.3999999999999999, 25.290559500000001, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trails");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
