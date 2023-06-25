using Microservices.Foundation.ContentTypes.Items;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Aarya.Foundation.ContentTypes.Types
{
    [Serializable]
    [DataContract]
    public class ContentItem : BaseItem
    {

        public ContentItem(string ClassName, bool IsTranslated) : base(ClassName, IsTranslated)
        {

        }

        public ContentItem() : base()
        {

        }
        public ContentItem(BaseItem item) : base(item)
        {

        }
        public ContentItem(string doc) : base(doc)
        {

        }
        public ContentItem(string className, string culture) : base(className, culture)
        {

        }

        public ContentItem(string pimId, string className, string culture = "en") : base(pimId, className, culture)
        {

        }

        [JsonIgnore]
        public string Description
        {
            get
            {
                return GetField<string>("description", null);
            }
        }

        [JsonIgnore]
        public string Summary
        {
            get
            {
                return GetField<string>("poiexcerpt", null);
            }
        }

        [JsonIgnore]
        public string ShortHelp
        {
            get
            {
                return GetField<string>("__short_description", null);
            }
        }

        [JsonIgnore]
        public List<string> SelectedLanguages { get; set; } = new List<string>();

        [JsonIgnore]
        public string IConSVG
        {
            get
            {
                return GetField<string>("iconsvg", null);
            }
        }
        [JsonIgnore]
        public string IConCSS
        {
            get
            {
                return GetField<string>("iconcss", null);
            }
        }

        [JsonIgnore]
        public bool ShowContextMenu
        {
            get
            {
                return GetSharedField<bool>("showContextMenu");
            }
            set
            {
                SetSharedField("showContextMenu", value);
            }
        }

        [JsonIgnore]
        public bool IsNew
        {
            get
            {
                return IsNewEntity();
            }
        }


        [JsonIgnore]
        public bool IsSelected { get; set; }

        [JsonIgnore]
        public bool IsReadOnly { get; set; }

        [JsonIgnore]
        public string ItemName
        {
            get
            {
                return GetSharedField<string>("itemname");
            }
            set
            {
                SetSharedField("itemname", value);
            }
        }

        [JsonIgnore]
        public string ItemValue
        {
            get
            {
                return GetLanguageField<string>("itemvalue");
            }
            set
            {
                SetLanguageField("itemvalue", value);
            }
        }

        [JsonIgnore]
        public string ItemCode
        {
            get
            {
                return GetField<string>("ItemCode", null);
            }
        }

        [JsonIgnore]
        public string ItemId
        {
            get
            {
                return GetSharedField<string>("itemid");
            }
            set
            {
                SetSharedField("itemid", value);
            }
        }

        [JsonIgnore]
        public string ShareableUrl { get; set; }

        [JsonIgnore]
        public string Url
        {
            get
            {
                return GetSharedField<string>("url");
            }
            set
            {
                SetSharedField("url", value);
            }
        }

        [JsonIgnore]
        public string Parent
        {
            get
            {
                return GetSharedField<string>("parent");
            }
            set
            {
                SetSharedField("parent", value);
            }
        }


        /// <summary>
        /// Todo this is only for testing as in real life our content will be translated and user can only re-order content in which is Translated
        /// </summary>
        [JsonIgnore]
        public int SortOrder
        {
            get
            {
                return GetSharedField<int>("__Sortorder");
            }
            set
            {
                SetSharedField("__Sortorder", value.ToString());
            }
        }
        [JsonIgnore]
        public bool IsDirty { get; set; } = false;


       

        private bool IsNewEntity()
        {

            if (this.TemplateItem != null)
            {
                DateTime creationDate = new DateTime(CreationTimeStamp, DateTimeKind.Utc);
                var newDate = creationDate.AddDays(this.TemplateItem.NewDuration);

                if (newDate > DateTime.UtcNow)
                {
                    return true;
                }

            }

            return false;
        }
    }
}
