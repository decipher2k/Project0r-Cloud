using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Assistant_Server.Migrations
{
    /// <inheritdoc />
    public partial class projectinvite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInvited",
                table: "projects",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOwner",
                table: "projects",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInvited",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "IsOwner",
                table: "projects");
        }
    }
}
