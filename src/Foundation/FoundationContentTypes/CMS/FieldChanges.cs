using Microservices.Foundation.ContentTypes.Enums;

namespace Microservices.Foundation.ContentTypes.Types
{
    public class FieldChange
    {
        public FieldOperationType FieldOperationType { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public string Culture { get; set; }

    }
}
