using Microservices.Grpc.Accounts.Contracts.Interfaces;

namespace Microservices.Grpc.Accounts.Contracts.Client
{
    public class AccountsGrpcServiceClient : IAccountsGrpcService
    {
        private readonly IAccountsGrpcService _accountsService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountsGrpcServiceClient> _logger;


        public AccountsGrpcServiceClient(
            IConfiguration configuration,
            ILogger<AccountsGrpcServiceClient> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _accountsService = GetGrpcChannel().CreateGrpcService<IAccountsGrpcService>();
        }
        public async Task<string> DemoTest(string request, CallContext context = default)
        {
            _logger.LogDebug("AccountsGrpcServiceClient : UpsertPartnerUser - Entering UpsertPartnerUser! for {@request}!", request);
            try
            {
                _logger.LogDebug("AccountsGrpcServiceClient : UpsertPartnerUser - Exiting UpsertPartnerUser! for {@request}!", request);
                return await _accountsService.DemoTest(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AccountsGrpcServiceClient : UpsertPartnerUser - Failed to load data with {@message}", ex.Message);
                return "Unable to call grpc function";
            }
        }

        #region Helpers
        private GrpcChannel GetGrpcChannel()
        {
            var httpHandler = new HttpClientHandler();

            // Return `true` to allow certificates that are untrusted/invalid
            if (_configuration["ApplicationDetails:AppMode"]?.ToLower() == "server")
            {
                httpHandler.ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            }


            string baseUri = _configuration.GetSection("EndPoints").GetValue<string>(nameof(AccountsGrpcServiceClient));
            var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, httpHandler));

            var channel = GrpcChannel.ForAddress(baseUri, new GrpcChannelOptions { HttpClient = httpClient });

            return channel;
        }

        #endregion
    }
}
