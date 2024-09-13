using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManagmentApplication.Migrations
{
    /// <inheritdoc />
    public partial class migra10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_message_Replays_Messages_message_id",
                table: "message_Replays");

            migrationBuilder.DropIndex(
                name: "IX_message_Replays_message_id",
                table: "message_Replays");

            migrationBuilder.DropColumn(
                name: "message_id",
                table: "message_Replays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "message_id",
                table: "message_Replays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_message_Replays_message_id",
                table: "message_Replays",
                column: "message_id");

            migrationBuilder.AddForeignKey(
                name: "FK_message_Replays_Messages_message_id",
                table: "message_Replays",
                column: "message_id",
                principalTable: "Messages",
                principalColumn: "message_id");
        }
    }
}
