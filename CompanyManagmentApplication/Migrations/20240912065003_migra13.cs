using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManagmentApplication.Migrations
{
    /// <inheritdoc />
    public partial class migra13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_user_id",
                table: "Messages",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_users_user_id",
                table: "Messages",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_users_user_id",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_user_id",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Messages");
        }
    }
}
