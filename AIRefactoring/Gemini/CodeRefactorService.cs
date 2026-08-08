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
			model = configuration["Gemini:ModelOptions:Model"] ?? "gemini-3.6-flash";
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

		public async Task<string> RefactorCodeAsync(string code)
		{
			if (string.IsNullOrWhiteSpace(code))
				return string.Empty;

			try
			{
				var title = await client.Models.GenerateContentAsync(model, code, titleConfig);

				var response = await client.Models.GenerateContentAsync(model, code, config);

				var validation = validator.Validate(code, response?.Text);

				if (!validation.IsValid)
				{
					throw new InvalidOperationException(
						validation.ErrorMessage);
				}

				return response?.Text?.Trim() ?? string.Empty;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
