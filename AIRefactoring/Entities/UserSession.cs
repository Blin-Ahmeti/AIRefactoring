namespace AIRefactoring.Entities
{
	public class UserSession
	{
		public Guid Id { get; set; }
		public Guid GuestIdentifier { get; set; }
		public string Title { get; set; } = "Untitled Session";

		public ICollection<CodeArtifact> CodeArtifacts { get; set; } = new List<CodeArtifact>();
	}
}
