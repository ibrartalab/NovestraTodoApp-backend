using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovestraTodo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NovestraTodos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Usrers_UserId",
                table: "Todos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usrers",
                table: "Usrers");

            migrationBuilder.RenameTable(
                name: "Usrers",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "lName",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "fName",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Users_UserId",
                table: "Todos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_UserId",
                table: "Todos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Usrers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Usrers",
                newName: "lName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Usrers",
                newName: "fName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usrers",
                table: "Usrers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Usrers_UserId",
                table: "Todos",
                column: "UserId",
                principalTable: "Usrers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
