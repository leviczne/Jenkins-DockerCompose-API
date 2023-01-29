using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeCadastroAPI.Migrations
{
    public partial class AddRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdAspNetUsers",
                table: "UsuariosViagem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IdAspNetUsersNavigationId",
                table: "UsuariosViagem",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosViagem_IdAspNetUsersNavigationId",
                table: "UsuariosViagem",
                column: "IdAspNetUsersNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosViagem_AspNetUsers_IdAspNetUsersNavigationId",
                table: "UsuariosViagem",
                column: "IdAspNetUsersNavigationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosViagem_AspNetUsers_IdAspNetUsersNavigationId",
                table: "UsuariosViagem");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosViagem_IdAspNetUsersNavigationId",
                table: "UsuariosViagem");

            migrationBuilder.DropColumn(
                name: "IdAspNetUsers",
                table: "UsuariosViagem");

            migrationBuilder.DropColumn(
                name: "IdAspNetUsersNavigationId",
                table: "UsuariosViagem");
        }
    }
}
