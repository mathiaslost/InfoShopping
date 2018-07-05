using Microsoft.EntityFrameworkCore.Migrations;

namespace UI.Web.Data.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "ShoppingModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "EstadoModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "EnderecoModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "ClienteModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "CidadeModel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ShoppingModel");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "EstadoModel");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "EnderecoModel");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ClienteModel");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "CidadeModel");
        }
    }
}
