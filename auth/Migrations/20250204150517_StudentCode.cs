using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace auth.Migrations
{
    /// <inheritdoc />
    public partial class StudentCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "studentCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "studentCode",
                table: "Users");
        }
    }
}
