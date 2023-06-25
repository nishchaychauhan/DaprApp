// Only use in this file to avoid conflicts with Microsoft.Extensions.Logging
using Microservices.BuildingBlocks.EventBus;
using Microservices.BuildingBlocks.EventBus.Abstractions;
using Microsoft.AspNetCore.Localization;
using Serilog;
using System.Globalization;

namespace Microservices.Worker.Accounts;

public static class ProgramExtensions
{
    private const string AppName = "Account Worker";

    public static void AddCustomSerilog(this WebApplicationBuilder builder)
    {
        var seqServerUrl = builder.Configuration["SeqServerUrl"]!;

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .WriteTo.Console()
            .WriteTo.Seq(seqServerUrl)
            .Enrich.WithProperty("ApplicationName", AppName)
            .CreateLogger();

        builder.Host.UseSerilog();
    }

    public static void AddCustomSwagger(this WebApplicationBuilder builder) =>
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = $"eShopOnDapr - {AppName}", Version = "v1" });
        });

    public static void UseCustomSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppName} V1");
        });
    }

    public static void AddCustomAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "accountsworker");
            });
        });
    }
    public static void AddCustomLocalization(this WebApplicationBuilder builder)
    {
        List<CultureInfo> supportedCultures = builder.Configuration.GetSection("SupportedCultures").Get<string[]>().Select(x => new CultureInfo(x)).ToList();
        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            // Add your custom culture provider back to the list
        });
    }
    public static void AddCustomApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEventBus, DaprEventBus>();



        //builder.Services.AddClientAccessTokenHttpClient("IdentityApiClient", configureClient: async client =>
        //{




        //    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Identity:ApiUrl"));
        //    await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        //    {
        //        ClientId = builder.Configuration.GetValue<string>("Identity:ClientId"),
        //        ClientSecret = builder.Configuration.GetValue<string>("Identity:AccountWorkerClientSecret"),
        //        Address = builder.Configuration.GetValue<string>("Identity:TokenEndPoint"),
        //        Scope = builder.Configuration.GetValue<string>("Identity:Scope")
        //    });

        //}).AddClientAccessTokenHandler();
    }


}
