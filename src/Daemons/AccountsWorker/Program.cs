using Serilog;


var appName = "Account Worker";
var builder = WebApplication.CreateBuilder(args);
builder.AddCustomSerilog();
builder.Services.AddHttpClient();
//builder.AddCustomAuthentication();
//builder.AddCustomAuthorization();
builder.AddCustomSwagger();
builder.AddCustomApplicationServices();
builder.Services.AddDaprClient();
builder.Services.AddControllers();





var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCustomSwagger();
}

app.UseCloudEvents();
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => Results.LocalRedirect("~/swagger"));
app.MapControllers();
app.MapSubscribeHandler();

try
{
    app.Logger.LogInformation("Initializing Stores for ({ApplicationName})...", appName);
    app.Logger.LogInformation("Starting web host ({ApplicationName})...", appName);
    app.Run();

}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Host terminated unexpectedly ({ApplicationName})...", appName);
}
finally
{
    Serilog.Log.CloseAndFlush();
}
