using Microsoft.EntityFrameworkCore.Migrations;

namespace UI.Web.Data.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LojaModel_EnderecoModel_EnderecoId",
                table: "LojaModel");

            migrationBuilder.RenameColumn(
                name: "EnderecoId",
                table: "LojaModel",
                newName: "ShoppingId");

            migrationBuilder.RenameIndex(
                name: "IX_LojaModel_EnderecoId",
                table: "LojaModel",
                newName: "IX_LojaModel_ShoppingId");

            migrationBuilder.AddForeignKey(
                name: "FK_LojaModel_ShoppingModel_ShoppingId",
                table: "LojaModel",
                column: "ShoppingId",
                principalTable: "ShoppingModel",
                principalColumn: "ShoppingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LojaModel_ShoppingModel_ShoppingId",
                table: "LojaModel");

            migrationBuilder.RenameColumn(
                name: "ShoppingId",
                table: "LojaModel",
                newName: "EnderecoId");

            migrationBuilder.RenameIndex(
                name: "IX_LojaModel_ShoppingId",
                table: "LojaModel",
                newName: "IX_LojaModel_EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_LojaModel_EnderecoModel_EnderecoId",
                table: "LojaModel",
                column: "EnderecoId",
                principalTable: "EnderecoModel",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
