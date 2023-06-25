using Microservices.Foundation.ContentTypes.Items;
using System.Collections.Specialized;
using System.Text.Json.Serialization;
using System.Web;

namespace Microservices.Foundation.ContentTypes.Types
{
    public class CommonEntityFields : BaseItem
    {
        public CommonEntityFields()
        {

        }
        public CommonEntityFields(BaseItem baseItem) : base(baseItem)
        {

        }

        [JsonIgnore]
        public string HelpLink
        {
            get
            {
                return GetSharedField<string>("__help_link");
            }
        }

        [JsonIgnore]
        public string IconImage
        {
            get
            {
                return GetSharedField<string>("__iconimage");
            }
        }

        [JsonIgnore]
        public bool IsDeletable
        {
            get
            {
                return GetSharedField<bool>("__isdeletable");
            }

        }

        [JsonIgnore]
        public string ItemCode
        {
            get
            {
                return GetSharedField<string>("__itemcode");
            }
        }

        [JsonIgnore]
        public string IconCSS
        {
            get
            {
                return GetSharedField<string>("__iconcss");
            }
        }

        [JsonIgnore]
        public string LocalizedName
        {
            get
            {
                return GetLanguageField<string>("__localizedname");
            }
        }

        [JsonIgnore]
        public string LongDescription
        {
            get
            {
                return GetLanguageField<string>("__long_description");
            }
        }

        [JsonIgnore]
        public string ShortDescription
        {
            get
            {
                return GetLanguageField<string>("__short_description");
            }
        }

        [JsonIgnore]
        public string NoRecordsFoundMessage
        {
            get
            {
                return GetLanguageField<string>("notfoundmessage");
            }
        }

        [JsonIgnore]
        public bool IsReadOnly
        {
            get
            {
                return !GetSharedField<bool>("__isupdatable");
            }
            set
            {
                SetSharedField("__isupdatable", value);
            }

        }

        [JsonIgnore]
        public bool IsHidden
        {
            get
            {
                return GetSharedField<bool>("__ishidden");
            }
        }


        [JsonIgnore]
        public int SortOrder
        {
            get
            {
                return GetSharedField<int>("__Sortorder");
            }
        }
        [JsonIgnore]
        public bool HasHelp
        {
            get
            {
                return !string.IsNullOrEmpty(ShortDescription);
            }
        }



        /// <summary>
        /// Validate if any parameters are set in the parameter of the Template/Field
        /// </summary>
        [JsonIgnore]
        public bool HasParameters
        {
            get
            {
                return HasSharedValue("parameters");
            }
        }


        /// <summary>
        /// Return string representation of the parameters value
        /// </summary>
        [JsonIgnore]
        private string Parameters
        {
            get
            {
                return GetSharedField<string>("parameters");
            }
        }



        /// <summary>
        /// NameValueCollection if any parameters are set
        /// </summary>
        [JsonIgnore]
        public NameValueCollection ParametersCollection
        {
            get
            {
                if (!HasParameters)
                {
                    return new NameValueCollection();
                }
                else
                {
                    return HttpUtility.ParseQueryString(Parameters);
                }
            }
        }

        [JsonIgnore]
        public string GuideActionsConfiguration
        {
            get
            {
                return GetSharedField<string>("__GuideActions");
            }
        }



        /// <summary>
        /// Validate is DataSource is set to load from a configuration entity
        /// </summary>
        [JsonIgnore]
        public bool IsLoadFromContentEntity
        {
            get
            {
                return ParametersCollection.AllKeys.Contains("contentEntitySourceField");
            }
        }
    }
}
