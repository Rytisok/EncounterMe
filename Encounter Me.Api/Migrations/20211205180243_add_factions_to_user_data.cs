using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Encounter_Me.Api.Migrations
{
    public partial class add_factions_to_user_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("90b94449-31aa-4caa-8e9c-69c46a7128f5"));

            migrationBuilder.AddColumn<int>(
                name: "Faction",
                table: "Users",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Faction",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "ExperiencePoints", "FirstName", "LastName", "Level", "Password", "StoredSalt", "UserName", "UserPhotoUrl" },
                values: new object[] { new Guid("90b94449-31aa-4caa-8e9c-69c46a7128f5"), "dz@mail.lt", 0.0, "Dominykas", "Zagreckas", 1, null, null, "obuolys", "https://i.pinimg.com/originals/83/6d/69/836d69f49e80af2825c7db264be44af0.jpg" });
        }
    }
}
