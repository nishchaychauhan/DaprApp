using Aarya.Foundation.ContentTypes.Types;
using Microservices.Foundation.ContentTypes.Enums;
using Microservices.Foundation.ContentTypes.Items;
using System.Collections.Specialized;
using System.Net;
using System.Text.Json.Serialization;
using System.Web;

namespace Microservices.Foundation.ContentTypes.Types
{
    public class ValidationRuleItem : BaseItem
    {
        [JsonIgnore]
        public FieldItem ContextFieldItem { get; }
        public ValidationRuleItem(BaseItem item, FieldItem fieldItem) : base(item)
        {
            ContextFieldItem = fieldItem;
        }

        public ValidationRuleItem()
        {

        }

        private string Parameters
        {
            get
            {
                return GetSharedField<string>("parameters");
            }
        }
        private string Description
        {
            get
            {
                return GetLanguageField<string>("description", ContextFieldItem.Culture);
            }
        }
        public ValidationTypes ValidationType
        {
            get
            {
                return GetValidationType();
            }
        }
        public string ErrorMessage
        {
            get
            {
                return GetErrorMessage();
            }
        }
        public string SuccessMessage { get; set; }

        [JsonIgnore]
        public NameValueCollection ValidationParameters
        {
            get
            {
                return HttpUtility.ParseQueryString(Parameters);
            }
        }

        public string GetParameter(string paramName)
        {
            var result = string.Empty;
            var value = ValidationParameters[paramName];
            if (value != null)
            {
                result = value;
            }
            return result;
        }

        private ValidationTypes GetValidationType()
        {
            if (HasSharedValue("__validationType"))
            {
                var validationType = GetSharedField<string>("__validationType").ToLower();
                if (validationType == "required")
                {
                    return ValidationTypes.Required;
                }
                else if (validationType == "minlength")
                {
                    return ValidationTypes.Minlength;
                }
                else if (validationType == "maxlength")
                {
                    return ValidationTypes.Maxlength;
                }
                else if (validationType == "itemcount")
                {
                    return ValidationTypes.ItemCount;
                }
                else if (validationType == "regex")
                {
                    return ValidationTypes.RegEx;
                }
                else if (validationType == "mindate")
                {
                    return ValidationTypes.MinDate;
                }
                else if (validationType == "length")
                {
                    return ValidationTypes.Length;
                }
                else if (validationType == "maxdate")
                {
                    return ValidationTypes.MaxDate;
                }
                else if (validationType == "maxvalue")
                {
                    return ValidationTypes.MaxValue;
                }
                else if (validationType == "minvalue")
                {
                    return ValidationTypes.MinValue;
                }
                else if (validationType == "mintime")
                {
                    return ValidationTypes.MinTime;
                }
                else if (validationType == "maxtime")
                {
                    return ValidationTypes.MaxTime;
                }
                else if (validationType == "minduration")
                {
                    return ValidationTypes.MinDuration;
                }
                else if (validationType == "maxcount")
                {
                    return ValidationTypes.MaxCount;
                }

            }
            return ValidationTypes.None;
        }

        private string GetErrorMessage()
        {
            string errorMessage = "Field value is not valid";
            if (HasValue("description"))
            {
                switch (ValidationType)
                {
                    case ValidationTypes.Required:
                        {
                            errorMessage = string.Format(Description, ContextFieldItem.GetDisplayName(ContextFieldItem.Culture));
                            break;
                        }
                    case ValidationTypes.Maxlength:
                        {
                            errorMessage = string.Format(Description, ContextFieldItem.GetDisplayName(ContextFieldItem.Culture));
                            break;
                        }
                    case ValidationTypes.RegEx:
                        {
                            errorMessage = string.Format(Description, ContextFieldItem.GetDisplayName(ContextFieldItem.Culture));
                            break;
                        }
                    case ValidationTypes.Minlength:
                        {
                            errorMessage = Description;
                            break;
                        }
                    case ValidationTypes.MinDate:
                        {
                            errorMessage = string.Format(Description, ContextFieldItem.DisplayName);
                            break;
                        }
                    case ValidationTypes.MaxDate:
                        {
                            errorMessage = string.Format(Description, ContextFieldItem.DisplayName);
                            break;
                        }
                    case ValidationTypes.Length:
                        {
                            errorMessage = string.Format(Description, ContextFieldItem.GetDisplayName(ContextFieldItem.Culture));
                            break;
                        }
                    case ValidationTypes.ItemCount:
                        {
                            errorMessage = string.Format(Description, ContextFieldItem.DisplayName);
                            break;
                        }
                    case ValidationTypes.MinCount:
                        {
                            errorMessage = string.Format(Description, ContextFieldItem.DisplayName);
                            break;
                        }
                    case ValidationTypes.MaxCount:
                        {
                            errorMessage = string.Format(Description, ContextFieldItem.DisplayName);
                            break;
                        }
                    case ValidationTypes.MaxValue:
                        {
                            errorMessage = string.Format(Description, ContextFieldItem.DisplayName);
                            break;
                        }
                    case ValidationTypes.MinValue:
                        {
                            errorMessage = string.Format(Description, ContextFieldItem.DisplayName);
                            break;
                        }
                    case ValidationTypes.MinTime:
                        {
                            errorMessage = string.Format(Description, ContextFieldItem.DisplayName);
                            break;
                        }
                    case ValidationTypes.MaxTime:
                        {
                            errorMessage = string.Format(Description, ContextFieldItem.DisplayName);
                            break;
                        }
                    case ValidationTypes.MinDuration:
                        {
                            errorMessage = string.Format(Description, ContextFieldItem.DisplayName);
                            break;
                        }
                    case ValidationTypes.None:
                        {
                            errorMessage = Description;
                            break;
                        }
                }
            }
            return WebUtility.HtmlDecode(errorMessage);
        }
    }

}

