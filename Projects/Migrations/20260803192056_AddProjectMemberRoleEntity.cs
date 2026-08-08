using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projects.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectMemberRoleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "ProjectMemberRoleId",
                table: "ProjectMember",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProjectMemberRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMemberRole", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_ProjectMemberRoleId",
                table: "ProjectMember",
                column: "ProjectMemberRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMember_ProjectMemberRole_ProjectMemberRoleId",
                table: "ProjectMember",
                column: "ProjectMemberRoleId",
                principalTable: "ProjectMemberRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMember_ProjectMemberRole_ProjectMemberRoleId",
                table: "ProjectMember");

            migrationBuilder.DropTable(
                name: "ProjectMemberRole");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMember_ProjectMemberRoleId",
                table: "ProjectMember");

            migrationBuilder.DropColumn(
                name: "ProjectMemberRoleId",
                table: "ProjectMember");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
