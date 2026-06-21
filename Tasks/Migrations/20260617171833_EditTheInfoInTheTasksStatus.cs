using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasks.Migrations
{
    /// <inheritdoc />
    public partial class EditTheInfoInTheTasksStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TasksStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Reopen");

            migrationBuilder.InsertData(
                table: "TasksStatus",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { 5, false, "Cancelled" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TasksStatus",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "TasksStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Cancelled");
        }
    }
}
