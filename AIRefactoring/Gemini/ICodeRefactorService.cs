using AIRefactoring.Models;

namespace AIRefactoring.Gemini
{
	public interface ICodeRefactorService
	{
		Task<RefactorResponse> RefactorCodeAsync(string code);
	}
}
