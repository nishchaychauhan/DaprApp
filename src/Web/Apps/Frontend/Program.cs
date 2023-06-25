using Microservices.Grpc.Accounts.Contracts.Client;
using Microservices.Grpc.Accounts.Contracts.Interfaces;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var options = new JsonSerializerOptions()
{
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
};

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDaprClient(client =>
{
    client.UseJsonSerializationOptions(options);
});
builder.Services.AddSingleton<IAccountsGrpcService, AccountsGrpcServiceClient>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
