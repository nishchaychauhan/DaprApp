var appName = "Accounts Grpc";
var builder = WebApplication.CreateBuilder(args);

//Register  Services
builder.AddCustomSerilog();
//builder.AddCustomAuthentication();
//builder.AddCustomAuthorization();
builder.AddCustomApplicationServices();
//builder.Services.AddDaprClient();
builder.AddCustomSwagger();
builder.Services.AddDaprClient();
builder.Services.AddHttpClient();
builder.Services.AddControllers();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));
builder.Services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = CompressionLevel.Optimal; });

builder.Services.AddCodeFirstGrpcReflection();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCustomSwagger();
}

// Configure the HTTP request pipeline.
app.UseCloudEvents();
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();

//app.UseRouting();
app.MapGet("/", () => Results.LocalRedirect("~/swagger"));

app.MapSubscribeHandler();

app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.UseCors();

app.MapGrpcService<AccountsGrpcService>().RequireCors("AllowAll");
app.MapCodeFirstGrpcReflectionService();
//app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

//app.MapDefaultControllerRoute();



app.MapControllers();

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






