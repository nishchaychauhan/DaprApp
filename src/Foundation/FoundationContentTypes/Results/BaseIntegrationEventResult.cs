using Microservices.Foundation.ContentTypes.Enums;
using Microservices.Foundation.ContentTypes.Items;
using System.Text.Json.Serialization;

namespace Microservices.Foundation.ContentTypes.Results
{
    public record BaseIntegrationEventResult
    {

        /// <summary>
        /// The duende id of the User or email of the user, sends the information to all device a user is connected to
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string DeviceGroup { get; set; }
        /// <summary>
        /// Id of the business to be notified of
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string BusinessGroup { get; set; }

        /// <summary>
        /// The  Data Object in case required
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public BaseItem Data { get; set; }

        /// <summary>
        /// The method to triggered at client end
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] 
        public string MethodName { get; set; }

        /// <summary>
        /// The type of result such as warning, error, success
        /// </summary>
        public EventResult Result { get; set; } = EventResult.Success;

        /// <summary>
        /// The Name of the App, the message is intended for
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Apps AppName { get; set; }

        /// <summary>
        /// The message for end user
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Message { get; set; }

        /// <summary>
        /// Id of the Message
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid Id { get; }

        /// <summary>
        /// Creation date of the message
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime CreationDate { get; }
        public BaseIntegrationEventResult()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
