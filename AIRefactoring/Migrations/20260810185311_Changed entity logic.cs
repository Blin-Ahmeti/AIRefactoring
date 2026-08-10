using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIRefactoring.Migrations
{
    /// <inheritdoc />
    public partial class Changedentitylogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "Prompt",
                table: "UserSessions");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CodeArtifacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CodeArtifacts");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Prompt",
                table: "UserSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
