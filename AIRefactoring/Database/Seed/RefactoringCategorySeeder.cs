using AIRefactoring.Entities;

namespace AIRefactoring.Database.Seed
{
    public class RefactoringCategorySeeder
    {
        public static void Seed(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            if (context.RefactoringCategories.Any())
                return;

            var categories = new List<RefactoringCategory>
            {
                //Refactoring Principles
                new()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Name = "dont-repeat-yourself",
                    Content = LoadContent("dont-repeat-yourself.html", environment),
                    ImageLink = "dont-repeat-yourself.png"
                },

                new()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Name = "boy-scout-rule",
                    Content = LoadContent("boy-scout-rule.html", environment),
                    ImageLink = "boy-scout-rule.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Name = "single-responsibility-principle",
                    Content = LoadContent("single-responsibility-principle.html", environment),
                    ImageLink = "single-responsibility-principle.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    Name = "keep-it-simple",
                    Content = LoadContent("keep-it-simple.html", environment),
                    ImageLink = "keep-it-simple.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    Name = "small-safe-changes",
                    Content = LoadContent("small-safe-changes.html", environment),
                    ImageLink = "small-safe-changes.png"
                },

                //Code Smells
                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    Name = "duplicated-code",
                    Content = LoadContent("duplicated-code.html", environment),
                    ImageLink = "duplicated-code.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    Name = "long-method",
                    Content = LoadContent("long-method.html", environment),
                    ImageLink = "long-method.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    Name = "large-class",
                    Content = LoadContent("large-class.html", environment),
                    ImageLink = "large-class.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                    Name = "long-parameter-list",
                    Content = LoadContent("long-parameter-list.html", environment),
                    ImageLink = "long-parameter-list.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                    Name = "god-object",
                    Content = LoadContent("god-object.html", environment),
                    ImageLink = "god-object.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                    Name = "dead-code",
                    Content = LoadContent("dead-code.html", environment),
                    ImageLink = "dead-code.png"
                },

                //Refactoring Catalog
                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                    Name = "code-simplification",
                    Content = LoadContent("code-simplification.html", environment),
                    ImageLink = "code-simplification.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                    Name = "removing-duplication",
                    Content = LoadContent("removing-duplication.html", environment),
                    ImageLink = "removing-duplication.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                    Name = "improving-naming",
                    Content = LoadContent("improving-naming.html", environment),
                    ImageLink = "improving-naming.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                    Name = "improving-structure",
                    Content = LoadContent("improving-structure.html", environment),
                    ImageLink = "improving-structure.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                    Name = "improving-abstraction",
                    Content = LoadContent("improving-abstraction.html", environment),
                    ImageLink = "improving-abstraction.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                    Name = "reducing-dependencies",
                    Content = LoadContent("reducing-dependencies.html", environment),
                    ImageLink = "reducing-dependencies.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                    Name = "improving-conditionals-and-control-flow",
                    Content = LoadContent("improving-conditionals-and-control-flow.html", environment),
                    ImageLink = "improving-conditionals-and-control-flow.png"
                },

                new() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000019"),
                    Name = "improving-architecture",
                    Content = LoadContent("improving-architecture.html", environment),
                    ImageLink = "improving-architecture.png"
                },
            };

            context.RefactoringCategories.AddRange(categories);
            context.SaveChanges();
        }

        private static string LoadContent(string fileName, IWebHostEnvironment environment)
        {
            var path = Path.Combine(
                environment.ContentRootPath,
                "Content",
                fileName
            );

            return File.ReadAllText(path);
        }
    }
}
