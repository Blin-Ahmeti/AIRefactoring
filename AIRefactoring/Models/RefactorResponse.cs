namespace AIRefactoring.Models
{
	public class RefactorResponse
	{
        public RefactorResponse()
        {
            Title = "";
            Code = "";
        }

        public string Title { get; set; } = null!;
        public string Code { get; set; } = null!;
	}
}
