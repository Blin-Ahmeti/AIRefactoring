namespace AIRefactoring.Gemini
{
	public interface ICodeRefactorService
	{
		Task<string> RefactorCodeAsync(string code);
	}
}
