using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class UserDesk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeskId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Desks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DeskId",
                table: "Users",
                column: "DeskId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Desks_DeskId",
                table: "Users",
                column: "DeskId",
                principalTable: "Desks",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Desks_DeskId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DeskId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeskId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Desks");
        }
    }
}
