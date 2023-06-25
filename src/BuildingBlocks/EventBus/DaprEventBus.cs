using Microservices.BuildingBlocks.EventBus.Abstractions;
using Microservices.BuildingBlocks.EventBus.Events;
using Microservices.BuildingBlocks.EventBus.IntegrationEvents;
using Microservices.Foundation.ContentTypes.Enums;
using Microservices.Foundation.ContentTypes.Items;
using Microservices.Foundation.ContentTypes.Results;

namespace Microservices.BuildingBlocks.EventBus;

public class DaprEventBus : IEventBus
{
    private const string DAPR_PUBSUB_NAME = "pubsub";

    private readonly DaprClient _dapr;
    private readonly ILogger _logger;

    public DaprEventBus(DaprClient dapr, ILogger<DaprEventBus> logger)
    {
        _dapr = dapr;
        _logger = logger;
    }

    public async Task PublishAsync(IntegrationEvent integrationEvent)
    {

        var topicName = integrationEvent.GetType().Name;
        try
        {
            _logger.LogInformation(
            "Publishing event {@Event} to {PubsubName}.{TopicName}",
            integrationEvent,
            DAPR_PUBSUB_NAME,
            topicName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Large event size : {topicName}");
        }
        finally
        {
            _logger.LogInformation(
            "Publishing event {@topic} to {PubsubName}.{TopicName}",
            topicName,
            DAPR_PUBSUB_NAME,
            topicName);
        }

        // We need to make sure that we pass the concrete type to PublishEventAsync,
        // which can be accomplished by casting the event to dynamic. This ensures
        // that all event fields are properly serialized.
        await _dapr.PublishEventAsync(DAPR_PUBSUB_NAME, topicName, (object)integrationEvent);
    }

    public async Task PublishNotificationAsync(string methodName, Apps appName, BaseItem data, EventResult result, string message, string DeviceGroup = null, string BusinessGroup = null)
    {
        if (string.IsNullOrEmpty(methodName))
        {
            throw new ArgumentNullException(nameof(methodName));
        }
        if (data == null)
        {
            throw new ArgumentException("Either message or For must be provided!");
        }

        BaseIntegrationEventResult integrationEvent;

        integrationEvent = new NotifyPartnersIntegrationEvent();

        integrationEvent.Data = data;
        integrationEvent.DeviceGroup = DeviceGroup;
        integrationEvent.BusinessGroup = BusinessGroup;
        integrationEvent.AppName = appName;
        integrationEvent.MethodName = methodName;
        integrationEvent.Result = result;
        integrationEvent.Message = message;

        var topicName = integrationEvent.GetType().Name;
        try
        {
            _logger.LogInformation(
            "Publishing event {@Event} to {PubsubName}.{TopicName}",
            integrationEvent,
            DAPR_PUBSUB_NAME,
            topicName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Large event size : {topicName}");
        }
        finally
        {
            _logger.LogInformation(
            "Publishing event {@topic} to {PubsubName}.{TopicName}",
            topicName,
            DAPR_PUBSUB_NAME,
            topicName);
        }

        // We need to make sure that we pass the concrete type to PublishEventAsync,
        // which can be accomplished by casting the event to dynamic. This ensures
        // that all event fields are properly serialized.
        await _dapr.PublishEventAsync(DAPR_PUBSUB_NAME, topicName, (object)integrationEvent);
    }

}
