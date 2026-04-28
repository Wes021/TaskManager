using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddPhonNumberToUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifiedUserType",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "phonenumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phonenumber",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserType",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
