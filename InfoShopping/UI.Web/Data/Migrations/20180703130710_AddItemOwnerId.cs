using Microsoft.EntityFrameworkCore.Migrations;

namespace UI.Web.Data.Migrations
{
    public partial class AddItemOwnerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "LojaModel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "LojaModel");
        }
    }
}
