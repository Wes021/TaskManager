using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projects.Migrations
{
    /// <inheritdoc />
    public partial class CheckLatestdbUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedDate",
                table: "ProjectMember",
                newName: "CreatedDate");

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedUser",
                table: "ProjectMember",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CreatedUser",
                table: "ProjectMember",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProjectMember",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProjectMember",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ProjectMember",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "ProjectMember");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectMember");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProjectMember");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ProjectMember");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "ProjectMember",
                newName: "AssignedDate");

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedUser",
                table: "ProjectMember",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
