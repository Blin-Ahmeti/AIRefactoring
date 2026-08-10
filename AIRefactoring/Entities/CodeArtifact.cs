namespace AIRefactoring.Entities
{
	public class CodeArtifact
	{
		public Guid Id { get; set; }
		public Guid UserSessionId { get; set; }
		public UserSession UserSession { get; set; } = null!;
		public string OriginalCode { get; set; } = string.Empty;
		public string RefactoredCode { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
