namespace AIRefactoring.Entities
{
	public class UserSession
	{
		public Guid Id { get; set; }
		public Guid GuestIdentifier { get; set; }
		public string Title { get; set; } = "Untitled Session";
		public string Prompt { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public ICollection<CodeArtifact> CodeArtifacts { get; set; } = new List<CodeArtifact>();
	}
}
