var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecksUI(settings => { }).AddInMemoryStorage();
var app = builder.Build();

//configure paths for HC-UI
app.UseHealthChecksUI(config => { config.UIPath = "/hc-ui"; config.ApiPath = "/hc-api"; });

//app.MapGet("/", () => "Hello World!");
app.Run();
