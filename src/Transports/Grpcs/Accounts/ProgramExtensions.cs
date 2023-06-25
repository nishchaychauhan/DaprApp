using Microservices.BuildingBlocks.EventBus;
using Microservices.BuildingBlocks.EventBus.Abstractions;

namespace Microservices.Grpc.Accounts
{
    public static class ProgramExtensions
    {
        private const string AppName = "Accounts Grpc";

        public static void AddCustomConfiguration(this WebApplicationBuilder builder)
        {
            // Disabled temporarily until https://github.com/dapr/dotnet-sdk/issues/779 is resolved.
            //builder.Configuration.AddDaprSecretStore(
            //    "eshop-secretstore",
            //    new DaprClientBuilder().Build());
        }

        public static void AddCustomSerilog(this WebApplicationBuilder builder)
        {
            var seqServerUrl = builder.Configuration["SeqServerUrl"];
            var environmentName = builder.Configuration["ApplicationDetails:Env"];
            var businessEntityId = builder.Configuration["BusinessEntity:BusinessEntityId"];

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .WriteTo.Console()
                .WriteTo.Seq(seqServerUrl)
                .Enrich.WithProperty("ApplicationType", "GRPC")
                .Enrich.WithProperty("ApplicationName", AppName)
                .Enrich.WithProperty("BusinessEntityId", businessEntityId)
                        .Enrich.WithProperty("Env", environmentName)
                        .Enrich.WithMachineName()
                        .Enrich.WithExceptionDetails()
                        .Enrich.FromLogContext()
                .CreateLogger();

            builder.Host.UseSerilog();
        }
        public static void AddCustomSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = $"eShopOnDapr - {AppName}", Version = "v1" });
            });

            //    var identityUrlExternal = builder.Configuration.GetValue<string>("IdentityUrlExternal");

            //    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            //    {
            //        Type = SecuritySchemeType.OAuth2,
            //        Flows = new OpenApiOAuthFlows()
            //        {
            //            Implicit = new OpenApiOAuthFlow()
            //            {
            //                AuthorizationUrl = new Uri($"{identityUrlExternal}/connect/authorize"),
            //                TokenUrl = new Uri($"{identityUrlExternal}/connect/token"),
            //                Scopes = new Dictionary<string, string>()
            //                {
            //                    { "ordering", AppName }
            //                }
            //            }
            //        }
            //    });

            //    c.OperationFilter<AuthorizeCheckOperationFilter>();
            //});
        }


        public static void UseCustomSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppName} V1");
                //c.OAuthClientId("orderingswaggerui");
                //c.OAuthAppName("Ordering Swagger UI");
            });
        }
        public static void AddCustomMvc(this WebApplicationBuilder builder)
        {
            // TODO DaprClient good enough?
            builder.Services.AddControllers().AddDapr();
        }

        public static void AddCustomApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IEventBus, DaprEventBus>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<IAccountsGrpcService, AccountsGrpcService>();

            //builder.Services.AddClientAccessTokenManagement(options =>
            //{
            //    options.Clients.Add("identityserver", new ClientCredentialsTokenRequest
            //    {
            //        ClientId = builder.Configuration.GetValue<string>("IdentityServer:ClientId"),
            //        ClientSecret = builder.Configuration.GetValue<string>("IdentityServer:AccountWorkerClientSecret"),
            //        Address = builder.Configuration.GetValue<string>("IdentityServer:TokenEndPoint"),
            //        Scope = builder.Configuration.GetValue<string>("IdentityServer:Scope"),
                    
            //    });
            //});

            //builder.Services.AddClientAccessTokenHttpClient("IdentityApiClient", configureClient: client =>
            //{
            //    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("IdentityServer:ApiUrl"));
            //});
        }

        //public static void AddCustomDatabase(this WebApplicationBuilder builder) =>
        //    builder.Services.AddDbContext<OrderingDbContext>(
        //        options => options.UseSqlServer(builder.Configuration["ConnectionStrings:OrderingDB"]));

        //public static void ApplyDatabaseMigration(this WebApplication app)
        //{
        //    // Apply database migration automatically. Note that this approach is not
        //    // recommended for production scenarios. Consider generating SQL scripts from
        //    // migrations instead.
        //    using var scope = app.Services.CreateScope();

        //    var retryPolicy = CreateRetryPolicy(app.Configuration, Log.Logger);
        //    var context = scope.ServiceProvider.GetRequiredService<OrderingDbContext>();

        //    retryPolicy.Execute(context.Database.Migrate);
        //}

        //private static Policy CreateRetryPolicy(IConfiguration configuration, Serilog.ILogger logger)
        //{
        //    // Only use a retry policy if configured to do so.
        //    // When running in an orchestrator/K8s, it will take care of restarting failed services.
        //    if (bool.TryParse(configuration["RetryMigrations"], out bool retryMigrations))
        //    {
        //        return Policy.Handle<Exception>().
        //            WaitAndRetryForever(
        //                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
        //                onRetry: (exception, retry, timeSpan) =>
        //                {
        //                    logger.Warning(
        //                        exception,
        //                        "Exception {ExceptionType} with message {Message} detected during database migration (retry attempt {retry})",
        //                        exception.GetType().Name,
        //                        exception.Message,
        //                        retry);
        //                }
        //            );
        //    }

        //    return Policy.NoOp();
        //}
    }
}
