using AIRefactoring.Models;
using Google.GenAI;
using Google.GenAI.Types;

namespace AIRefactoring.Gemini
{
	public class CodeRefactorService : ICodeRefactorService
	{
		private readonly Client client;
		private readonly CodeRefactorValidator validator;
		private readonly string model;
		private readonly GenerateContentConfig config;
		private readonly GenerateContentConfig titleConfig;

		public CodeRefactorService(Client client, IConfiguration configuration,
			CodeRefactorValidator validator)
		{
			this.client = client;
			this.validator = validator;
			model = configuration["Gemini:ModelOptions:Model"] ?? "gemini-3.5-flash-lite";
			config = new GenerateContentConfig
			{
				Temperature = configuration.GetValue<double>("Gemini:ModelOptions:Temperature", 0.0),
				SystemInstruction = new Content
				{
					Parts = [new Part
					{
						Text = configuration["Gemini:ModelOptions:SystemInstruction"] ?? string.Empty
					}]
				}
			};
			titleConfig = new GenerateContentConfig
			{
				Temperature = configuration.GetValue<double>("Gemini:ModelOptions:Temperature", 0.0),
				SystemInstruction = new Content
				{
					Parts = [new Part
					{
						Text = configuration["Gemini:ModelOptions:TitleSystemInstruction"] ?? string.Empty
					}]
				}
			};
		}

		public async Task<RefactorResponse> RefactorCodeAsync(string prompt, bool includeTitle)
		{
			if (string.IsNullOrWhiteSpace(prompt))
				return new();

			var response = await client.Models.GenerateContentAsync(model, prompt, config);

			GenerateContentResponse? title = null;
			if (includeTitle)
				title = await client.Models.GenerateContentAsync(model, prompt, titleConfig);

			var validation = validator.Validate(prompt, response?.Text);

			if (!validation.IsValid)
			{
				throw new InvalidOperationException(
					validation.ErrorMessage);
			}

			return new()
			{
				Title = title?.Text?.Trim() ?? string.Empty,
				Code = response?.Text?.Trim() ?? string.Empty
			};
		}
	}
}
