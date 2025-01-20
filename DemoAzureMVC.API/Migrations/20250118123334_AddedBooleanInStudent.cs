using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoAzureMVC.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedBooleanInStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWizard",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWizard",
                table: "Students");
        }
    }
}
