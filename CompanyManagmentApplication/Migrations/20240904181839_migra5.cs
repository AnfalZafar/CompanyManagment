using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManagmentApplication.Migrations
{
    /// <inheritdoc />
    public partial class migra5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_users_user_id",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "Messages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "from_email",
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

            migrationBuilder.DropColumn(
                name: "from_email",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "to_email",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_users_user_id",
                table: "Messages",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
