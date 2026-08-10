namespace AIRefactoring.Models
{
	public class RefactorRequest
	{
		public string Prompt { get; set; } = string.Empty;
		public Guid? UserSessionId { get; set; }
		public Guid GuestIdentifier { get; set; }
	}
}
