using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasks.Migrations
{
    /// <inheritdoc />
    public partial class AddAttachemnetSizeToAttachemntEntityFixColumnsNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaskAttachments_TasksId",
                table: "TaskAttachments");

            migrationBuilder.RenameColumn(
                name: "AttactmentType",
                table: "TaskAttachments",
                newName: "AttachmentType");

            migrationBuilder.RenameColumn(
                name: "AttacmentName",
                table: "TaskAttachments",
                newName: "AttachmentSize");

            migrationBuilder.RenameColumn(
                name: "AttachementPath",
                table: "TaskAttachments",
                newName: "AttachmentPath");

            migrationBuilder.AddColumn<string>(
                name: "AttachmentName",
                table: "TaskAttachments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachments_TasksId",
                table: "TaskAttachments",
                column: "TasksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaskAttachments_TasksId",
                table: "TaskAttachments");

            migrationBuilder.DropColumn(
                name: "AttachmentName",
                table: "TaskAttachments");

            migrationBuilder.RenameColumn(
                name: "AttachmentType",
                table: "TaskAttachments",
                newName: "AttactmentType");

            migrationBuilder.RenameColumn(
                name: "AttachmentSize",
                table: "TaskAttachments",
                newName: "AttacmentName");

            migrationBuilder.RenameColumn(
                name: "AttachmentPath",
                table: "TaskAttachments",
                newName: "AttachementPath");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachments_TasksId",
                table: "TaskAttachments",
                column: "TasksId",
                unique: true);
        }
    }
}
