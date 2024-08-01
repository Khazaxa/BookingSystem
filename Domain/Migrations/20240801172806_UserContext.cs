using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class UserContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Desks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Desks_UserId",
                table: "Desks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Desks_Users_UserId",
                table: "Desks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desks_Users_UserId",
                table: "Desks");

            migrationBuilder.DropIndex(
                name: "IX_Desks_UserId",
                table: "Desks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Desks");
        }
    }
}
