using Microservices.BuildingBlocks.EventBus.Abstractions;
using Microservices.BuildingBlocks.EventBus.Events;
using Microservices.BuildingBlocks.EventBus.IntegrationEvents.AaryaIntegerationEvent;
using Microservices.Foundation.ContentTypes.Items;

namespace Microservices.Worker.Accounts.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AccountsIntegrationEventController : ControllerBase
{
    private const string DAPR_PUBSUB_NAME = "pubsub";
    private readonly IEventBus _eventBus;
    private readonly ILogger<AccountsIntegrationEventController> _logger;

    //Since this variable is static,  there might be various threads reading and writing into it at the same time
    static ConcurrentDictionary
        <string, bool> processingQueue = new ConcurrentDictionary<string, bool>();


    public AccountsIntegrationEventController(
              IEventBus eventBus,
              ILogger<AccountsIntegrationEventController> logger
        )
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    #region Partner Document Operations

    [HttpPost("DemoRequested")]
    [Topic(DAPR_PUBSUB_NAME, nameof(DemoRequestedIntegrationEvent))]
    public async Task<bool> HandleDemoRequested(
       DemoRequestedIntegrationEvent @event)
    {
        _logger.LogDebug("AccountsIntegrationEventController : HandlePartnerUserUpdateRequested - Entering HandlePartnerUserUpdateRequested! for {@event}!", @event);
        bool result = true;
        return result;
    }

    #endregion

    #region Helpers
    private static void Remove(IntegrationEvent @event)
    {
        //Update the value in the queue
        processingQueue.TryUpdate(@event.DuplicateMarker, true, false);
        bool removed;
        processingQueue.TryRemove(@event.DuplicateMarker, out removed);
    }

    private bool AppendQueue(IntegrationEvent @event, BaseItem document)
    {
        //Remove from Queue any item which are  in  finished state
        List<string> dirtyItems = new List<string>();
        dirtyItems = processingQueue.Where(x => x.Value == true).Select(x => x.Key).ToList();
        foreach (var key in dirtyItems)
        {
            bool removed;
            processingQueue.TryRemove(key, out removed);
        }
        if (processingQueue.Any(x => x.Key == @event.DuplicateMarker))
        {
            if (document != null)
            {
                _logger.LogDebug("PublishingIntegrationEventController : Duplicate event for {@id} of {@document}!", document.Id, document.ClassName);
            }
            else
            {
                _logger.LogDebug($"PublishingIntegrationEventController : Duplicate event for {@event}");
            }
            return false;
        }
        else
        {
            //This is a new event, we should add to processing queue
            processingQueue.TryAdd(@event.DuplicateMarker, false);
            return true;
        }

    }

    #endregion
}
