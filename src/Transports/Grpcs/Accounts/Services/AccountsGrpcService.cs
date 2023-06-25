using Microservices.BuildingBlocks.EventBus.Abstractions;
using Microservices.BuildingBlocks.EventBus.IntegrationEvents.AaryaIntegerationEvent;
using Microservices.Foundation.ContentTypes.Items;

namespace Microservices.Grpc.Accounts.Services
{
    public class AccountsGrpcService : IAccountsGrpcService
    {
        private const string StoreName = "statestore";
        private readonly ILogger<AccountsGrpcService> _logger;
        private readonly IEventBus _eventBus;
        private readonly DaprClient _daprClient;

        public AccountsGrpcService(
           ILogger<AccountsGrpcService> logger,
           IEventBus eventBus,
            DaprClient daprClient
           )
        {
            _logger = logger;
            _daprClient = daprClient;
            _eventBus = eventBus;
        }

        /// <summary>
        /// Add/Updates a tenant user in Database
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> DemoTest(string request, CallContext context = default)
        {

            var document = new BaseItem(request);
            _logger.LogDebug("AccountsGrpcService : UpsertPartnerUser - Entering UpsertPartnerUser! for {@request}!", request);


            //Check for permission if true put in Cache
            try
            {
                await _daprClient.SaveStateAsync(StoreName, document.Id, document.ToDoc(true));
                await _eventBus.PublishAsync(new DemoRequestedIntegrationEvent
                {
                    Message = "Successfully Trigerred",
                    DuplicateMarker = Guid.NewGuid().ToString()
                });
                return "Successfully called grpc function";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AccountsGrpcService : UpsertPartnerUser - Failed to load data with {@message}", ex.Message);
                return "Failed to call grpc function";
            }
        }
    }
}

