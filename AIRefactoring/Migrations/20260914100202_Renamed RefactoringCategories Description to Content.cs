using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIRefactoring.Migrations
{
    /// <inheritdoc />
    public partial class RenamedRefactoringCategoriesDescriptiontoContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "RefactoringCategories",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "RefactoringCategories",
                newName: "Description");
        }
    }
}
