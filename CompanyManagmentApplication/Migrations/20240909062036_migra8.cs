using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManagmentApplication.Migrations
{
    /// <inheritdoc />
    public partial class migra8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "replay_id",
                table: "message_Replays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "replay_id",
                table: "message_Replays");
        }
    }
}
