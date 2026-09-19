namespace AIRefactoring.Entities
{
    public class RefactoringCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string ImageLink { get; set; } = null!;
    }
}
