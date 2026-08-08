using AIRefactoring.Database;
using AIRefactoring.Gemini;
using Google.GenAI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddSingleton(sp =>
	new Client(apiKey: builder.Configuration["Gemini:ApiKey"])
);

builder.Services.AddScoped<ICodeRefactorService, CodeRefactorService>();
builder.Services.AddScoped<CodeRefactorValidator>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
