using Microservices.Foundation.ContentTypes.Enums;
using System.Text.Json.Serialization;

namespace Microservices.Foundation.ContentTypes.Types
{
    public class DocumentMeta
    {
        [JsonPropertyName("dot")]
        public OperationType DocumentOperationType { get; set; }

        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("cn")]
        public string ClassName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("tid")]
        public string TenantId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("op")]
        public string OrganizationParent { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("ub")]
        public string UpdatedBy { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("mt")]
        public string ModificationTimeStamp { get; set; }

        [JsonPropertyName("fc")]
        public List<FieldChange> FieldChanges { get; set; }

    }
}
