namespace AIRefactoring.Gemini
{
	public sealed class CodeRefactorValidator
	{
		public ValidationResult Validate(string? originalCode, string? refactoredCode)
		{
			if (string.IsNullOrWhiteSpace(originalCode))
				return ValidationResult.Failure("Original code cannot be empty.");

			if (string.IsNullOrWhiteSpace(refactoredCode))
				return ValidationResult.Failure("Refactored code cannot be empty.");

			if (refactoredCode.Contains("```"))
				return ValidationResult.Failure(
					"The response contains Markdown code fences.");

			if (refactoredCode.Contains("Here is the refactored code",
					StringComparison.OrdinalIgnoreCase))
				return ValidationResult.Failure(
					"The response contains explanatory text.");

			if (refactoredCode.Length > 100_000)
				return ValidationResult.Failure(
					"The refactored code exceeds the maximum allowed size.");

			return ValidationResult.Success();
		}
	}

	public sealed record ValidationResult(
		bool IsValid,
		string? ErrorMessage)
	{
		public static ValidationResult Success() =>
			new(true, null);

		public static ValidationResult Failure(string message) =>
			new(false, message);
	}
}
