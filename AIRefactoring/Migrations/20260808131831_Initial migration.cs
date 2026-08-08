using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIRefactoring.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuestIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Prompt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodeArtifacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefactoredCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeArtifacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeArtifacts_UserSessions_UserSessionId",
                        column: x => x.UserSessionId,
                        principalTable: "UserSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeArtifacts_UserSessionId",
                table: "CodeArtifacts",
                column: "UserSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_GuestIdentifier",
                table: "UserSessions",
                column: "GuestIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeArtifacts");

            migrationBuilder.DropTable(
                name: "UserSessions");
        }
    }
}
