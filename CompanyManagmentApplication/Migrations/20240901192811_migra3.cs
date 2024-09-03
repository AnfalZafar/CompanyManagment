using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManagmentApplication.Migrations
{
    /// <inheritdoc />
    public partial class migra3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "message_verify",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "to_email",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "your_email",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "your_name",
                table: "Messages",
                newName: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Messages",
                newName: "your_name");

            migrationBuilder.AddColumn<string>(
                name: "message_verify",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "to_email",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "your_email",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
