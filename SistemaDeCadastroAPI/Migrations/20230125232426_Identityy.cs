using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeCadastroAPI.Migrations
{
    public partial class Identityy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ViajanteName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViajanteName",
                table: "AspNetUsers");
        }
    }
}
