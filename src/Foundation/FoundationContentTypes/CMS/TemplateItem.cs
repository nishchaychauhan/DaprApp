using Microservices.Foundation.ContentTypes.Items;
using Microservices.Foundation.ContentTypes.Types;
using Serilog;
using System.Text.Json.Serialization;

namespace Aarya.Foundation.ContentTypes.Types
{
    public class TemplateItem : CommonEntityFields
    {

        public TemplateItem()
        {

        }

        public TemplateItem(BaseItem item) : base(item)
        {

        }

        [JsonIgnore]
        public List<SectionItem> Sections { get; set; } = new List<SectionItem>();


        [JsonIgnore]
        public string BaseTemplates
        {
            get
            {
                return GetSharedField<string>("__base_template");
            }
        }


        #region StoreName
        [JsonIgnore]
        public string StoreName
        {
            get
            {
                var storeName = GetSharedField<string>("__StoreName");
                if (String.IsNullOrEmpty(storeName))
                {
                    Log.Logger.Error("There is no store configurued on class {@className} with id {@id}", this.ClassName, this.Id);
                    throw new Exception(nameof(StoreName));
                }
                return storeName;
            }
        }

        [JsonIgnore]
        public string IndexName
        {
            get
            {
                return GetSharedField<string>("__indexName");
            }
        }

        [JsonIgnore]
        public bool IsSeedable
        {
            get
            {
                return HasValue("__indexName");
            }
        }

        /// <summary>
        /// The Item is seedable but we don't want to seed it as content is too much,
        /// These are  candidates for progessive seeds
        /// </summary>
        [JsonIgnore]
        public bool AllowSeeding
        {
            get
            {
                return GetSharedField<bool>("allowSeeding");
            }
        }
        #endregion

        [JsonIgnore]
        public bool UseSystemSortOrder
        {
            get
            {
                return GetSharedField<bool>("__UseSystemSortOrder");
            }
        }

        [JsonIgnore]
        public Double NewDuration
        {
            get
            {
                if(HasSharedValue("NewDuration"))
                {
                    return GetSharedField<Double>("NewDuration");
                }
                else
                {
                    return 1;
                }
                
            }
        } 

        [JsonIgnore]
        public bool GenerateUniqueNamedItems
        {
            get
            {
                return GetSharedField<bool>("generateuniquenameditems");
            }
        }
        [JsonIgnore]
        public bool SeedNoUniqueIds
        {
            get
            {
                return GetSharedField<bool>("seednouniqueids");
            }
        }








        [JsonIgnore]
        public bool ExpandImage
        {
            get
            {
                return GetSharedField<bool>("expandImage");
            }
        }


        [JsonIgnore]
        public bool HideTitle
        {
            get
            {
                return GetSharedField<bool>("__hideTitle");
            }
        }

        [JsonIgnore]
        public bool BreakBySection
        {
            get
            {
                return GetSharedField<bool>("__breakBySection");
            }
        
        }

        [JsonIgnore]
        public bool IsListable
        {
            get
            {
                return GetSharedField<bool>("__isListable");
            }
        }

        [JsonIgnore]
        public bool IsCreatable
        {
            get
            {
                return GetSharedField<bool>("__isCreatable");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public bool HasPortalPage
        {
            get
            {
                return GetSharedField<bool>("__CreatePortalPage");
            }
        }

        [JsonIgnore]
        public bool Cacheable
        {
            get
            {
                return GetSharedField<bool>("__CanAddCache");
            }
        }

        [JsonIgnore]
        public bool IsTranslatable
        {
            get
            {
                return GetSharedField<bool>("__isTranslatable");
            }
        }

        [JsonIgnore]
        public bool IsPublished
        {
            get
            {
                return GetSharedField<bool>("__IsPublishable");
            }
        }

        [JsonIgnore]
        public string PluralName
        {
            get
            {
                return GetLanguageField<string>("__PluralName");
            }
        }


        [JsonIgnore]
        public string PublishedDocument
        {
            get
            {
                return GetSharedField<string>("__PublishedDocument");
            }
        }

        [JsonIgnore]
        public string EntityManager
        {
            get
            {
                return GetSharedField<string>("EntityManager");
            }
        }

        [JsonIgnore]
        public bool PublishedTemplateItems
        {
            get
            {
                return GetSharedField<bool>("PublishTemplateItems");
            }
        }
        public List<string> GetBaseTemplateIds()
        {
            List<string> Ids = new List<string>();
            if (HasBaseTemplates())
            {
                var templateIds = BaseTemplates.Split("|").ToList();
                if (templateIds.Any())
                {
                    Ids = templateIds;
                }
            }
            return Ids;
        }


        public bool HasBaseTemplates()
        {
            if (string.IsNullOrEmpty(this.BaseTemplates))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        [JsonIgnore]
        public bool HasDefaultImage
        {
            get
            {
                return !string.IsNullOrEmpty(this.DefaultImagePath);
            }
        }

        [JsonIgnore]
        private string DefaultImagePath
        {
            get
            {
                return GetSharedField<string>("__default_image");
            }
           
        }

        /// <summary>
        /// Finds a Cascading field on the basis of name of the parent field, assuming there is only one
        /// </summary>
        /// <param name="fieldName">Name of the parent field on which the child cascade</param>
        /// <returns></returns>
        public FieldItem GetCascadingField(string fieldName)
        {
            FieldItem fieldItem = null;
            foreach (var section in Sections)
            {
                fieldItem = section.Fields.Where(x => x.IsCascadingField && x.CascadeOn.ToLower() == fieldName.ToLower()).FirstOrDefault();
                if (fieldItem != null)
                {
                    Log.Information($"Found Cascading field for {fieldName}. The cascaded field is {fieldItem.Name}", nameof(GetCascadingField));
                    break;
                }
            }
            return fieldItem;
        }

        /// <summary>
        /// Finds a Cascading field on the basis of name of the parent field, assuming there is only one
        /// </summary>
        /// <param name="fieldName">Name of the parent field on which the child cascade</param>
        /// <returns></returns>
        public bool IsCascadingMaster(string fieldName)
        {
            var fieldItem = GetCascadingField(fieldName);
            if (fieldItem != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        [JsonIgnore]
        public List<ContentItem> ConfiguredCardsActions { get; set; }

        [JsonIgnore]

        public List<string> FullCardActions { get; set; } = new List<string>();

        public bool HasAction(string actionName)
        {
            return FullCardActions.Contains(actionName.ToLower());
        }
        [JsonIgnore]
        public List<TemplateItem> BaseEntities { get; set; } = new List<TemplateItem>();
        public FieldItem GetFieldDefinition(string fieldName)
        {
            FieldItem fieldItem = null;
            foreach (var section in Sections)
            {
                fieldItem = section.Fields.Where(x => x.Name.ToLower() == fieldName.ToLower()).FirstOrDefault();
                if (fieldItem != null)
                {
                    break;
                }
            }

            if (fieldItem == null)
            {
                Log.Warning($"The field {fieldName} is not found in the template {this.Name}", nameof(GetFieldDefinition));
            }

            return fieldItem;
        }

        public bool HasFieldDefinition(string fieldName)
        {
            FieldItem fieldItem = null;
            foreach (var section in Sections)
            {
                fieldItem = section.Fields.Where(x => x.Name.ToLower() == fieldName.ToLower()).FirstOrDefault();
                if (fieldItem != null)
                {
                    return true;
                }
            }
            return false;
        }

        public List<FieldItem> GetExapandedFields(string soureType)
        {
            List<FieldItem> expandedFields = new List<FieldItem>();
            foreach (var section in Sections)
            {
                expandedFields.AddRange(section.Fields.Where(x => x.IsExpandedField && x.SourceType?.ToLower() == soureType.ToLower()).ToList());
            }
            return expandedFields;
        }

        public List<FieldItem> GetPublishedFields()
        {
            List<FieldItem> publishedFields = new List<FieldItem>();
            foreach (var section in Sections)
            {
                publishedFields.AddRange(section.Fields.Where(x => x.IsPublished).ToList());
            }
            return publishedFields;
        }

        public bool HasExpandedSourceField(string soureType)
        {
            bool hasExpandedField = false;
            foreach (var section in Sections)
            {
                if (section.Fields.Any(x => x.IsExpandedField && x.SourceType?.ToLower() == soureType.ToLower()))
                {
                    hasExpandedField = true;
                    break;
                }
            }
            return hasExpandedField;
        }

        public FieldItem GetFieldDefinitionById(string id)
        {
            FieldItem fieldItem = new();
            foreach (var section in Sections)
            {
                fieldItem = section.Fields.Where(x => x.Id.ToLower() == id.ToLower()).FirstOrDefault();
                if (fieldItem != null)
                {
                    break;
                }
            }
            return fieldItem;
        }

        public FieldItem GetFieldDefinition(SectionItem section, string fieldName)
        {
            FieldItem fieldItem = null;
            fieldItem = section.Fields.Where(x => x.Name == fieldName).FirstOrDefault();
            return fieldItem;
        }
        public SectionItem GetSectionDefinition(string sectionName)
        {
            return Sections.Where(x => x.Name == sectionName).FirstOrDefault();
        }

        public ContentItem Create(string culture = "en")
        {
            ContentItem contentItem = new ContentItem();
            contentItem.Id = Guid.NewGuid().ToString();
            contentItem.Culture = culture;
            contentItem.CreationTimeStamp = DateTime.UtcNow.Ticks;
            return contentItem;
        }

        public bool HasBaseTemplate(string baseTemplateId)
        {
            if (this.HasBaseTemplates())
            {
                var ids = this.GetBaseTemplateIds();
                if (ids?.Any() == true)
                {
                    if (ids?.Any(x => x == baseTemplateId) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasPublishField()
        {
            foreach (var section in this.Sections)
            {
                foreach (var field in section.Fields)
                {
                    if (!String.IsNullOrEmpty(field.PublishedDocument))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool HasFieldPublishDocumentStore(List<string> stores)
        {
            foreach (var section in this.Sections)
            {
                foreach (var field in section.Fields)
                {
                    if (!string.IsNullOrEmpty(field.PublishedDocument))
                    {
                        return stores.Any(x => x.ToLower() == field.PublishedDocument.ToLower());
                    }
                }
            }
            return false;
        }
        [JsonIgnore]
        public string Permissions
        {
            get
            {
                return GetSharedField<string>("Permissions");
            }
        }
        public bool IsPermissionsConfigured()
        {
            return HasSharedValue("Permissions");
        }

        /// <summary>
        /// If Guide Actions are configured for a template or field instance
        /// </summary>
        /// <returns></returns>
        public bool HasGuideActions()
        {
            return GetSharedField<bool>("HasGuideAction");

        }

        /// <summary>
        /// If any Guide Action is Configured on Field or Template collectively
        /// </summary>
        /// <returns></returns>
        public bool HasAnyGuideActions()
        {
            return HasGuideActions() || HasFieldGuideActions();
        }

        public bool HasFieldGuideActions()
        {
            foreach (var section in this.Sections)
            {
                foreach (var field in section.Fields)
                {
                    return field.HasGuideActions();
                }
            }
            return false;
        }
    }
}
