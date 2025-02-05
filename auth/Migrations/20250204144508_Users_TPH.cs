using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace auth.Migrations
{
    /// <inheritdoc />
    public partial class Users_TPH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GraduationYear",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "credsToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_type",
                table: "Users",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraduationYear",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "credsToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "user_type",
                table: "Users");
        }
    }
}
