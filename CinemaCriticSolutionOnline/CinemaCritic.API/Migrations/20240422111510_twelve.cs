using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaCritic.API.Migrations
{
    /// <inheritdoc />
    public partial class twelve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Reviews",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Reviews");
        }
    }
}
