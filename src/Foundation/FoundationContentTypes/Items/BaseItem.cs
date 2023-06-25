using Aarya.Foundation.ContentTypes.Types;
using Microservices.Foundation.ContentTypes.Enums;
using Microservices.Foundation.ContentTypes.Types;
using Microservices.Foundation.ContentTypes.Utils;
using Microservices.Foundation.Infrastructure;
using Serilog;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microservices.Foundation.ContentTypes.Items
{
    [Serializable]
    [DataContract]
    public class BaseItem : IEquatable<BaseItem>
    {

        #region ctor
        public BaseItem()
        {
            CreationTimeStamp = DateTime.UtcNow.Ticks;

        }
        public BaseItem(bool withInit = false) : base()
        {
            if (withInit)
            {
                PIMId = Guid.NewGuid().ToString();
                Id = PIMId;
                CreationTimeStamp = DateTime.UtcNow.Ticks;
            }
        }
        public BaseItem(string className, bool IsTranlsated)
        {
            CreationTimeStamp = DateTime.UtcNow.Ticks;
            ModificationTimestamp = CreationTimeStamp;
            PIMId = Guid.NewGuid().ToString();
            Id = PIMId;
            ClassName = className;
            IsTranslatedEntity = IsTranlsated;
        }
        public BaseItem(string className, string culture)
        {
            CreationTimeStamp = DateTime.UtcNow.Ticks;
            PIMId = Guid.NewGuid().ToString();
            ClassName = className;
            if (!String.IsNullOrEmpty(culture))
            {
                Id = $"{PIMId}_{culture}";
            }
        }
        public BaseItem(string pimId, string className, string culture = "en")
        {
            CreationTimeStamp = DateTime.UtcNow.Ticks;
            PIMId = pimId;
            Culture = culture;
            Id = $"{pimId}_{Culture}";
            ClassName = className;
        }
        public BaseItem(BaseItem item)
        {
            Init(item, true);
        }

        public BaseItem(string docString)
        {
            Init(this.ToBaseItem(docString));
        }



        public void Init(BaseItem item, bool withTemplate = false)
        {
            this.Id = item.Id;
            this.ClassName = item.ClassName;
            this.Culture = item.Culture;
            this.FullPath = item.FullPath;
            this.Name = item.Name;
            this.SetDisplayName(item.DisplayName, addMeta: false);
            this.CreationTimeStamp = item.CreationTimeStamp;
            this.ModificationTimestamp = item.ModificationTimestamp;
            this.ParentId = item.ParentId;
            this.ParentPath = item.ParentPath;
            this.PIMId = item.PIMId;
            this.CreatedBy = item.CreatedBy;
            this.UpdatedBy = item.UpdatedBy;
            this.OrganizationParent = item.OrganizationParent;
            this.SharedFields = new Dictionary<string, string>(item.SharedFields);
            this.LanguageFields = new Dictionary<string, string>(item.LanguageFields);
            this.IsTranslatedEntity = item.IsTranslatedEntity;
            this.TenantId = item.TenantId;
            if (withTemplate)
            {
                this.TemplateItem = item.TemplateItem;
            }
        }
        public BaseItem Copy(BaseItem item)
        {
            BaseItem copy = new BaseItem();
            copy.Id = item.Id;
            copy.ClassName = item.ClassName;
            copy.Culture = item.Culture;
            copy.FullPath = item.FullPath;
            copy.Name = item.Name;
            copy.SetDisplayName(item.DisplayName, addMeta: false);

            copy.CreationTimeStamp = item.CreationTimeStamp;
            copy.ModificationTimestamp = item.ModificationTimestamp;
            copy.ParentId = item.ParentId;
            copy.ParentPath = item.ParentPath;
            copy.PIMId = item.PIMId;
            copy.CreatedBy = item.CreatedBy;
            copy.UpdatedBy = item.UpdatedBy;
            copy.OrganizationParent = item.OrganizationParent;
            copy.SharedFields = new Dictionary<string, string>(item.SharedFields);
            copy.LanguageFields = new Dictionary<string, string>(item.LanguageFields);
            copy.TenantId = item.TenantId;
            copy.IsTranslatedEntity = item.IsTranslatedEntity;
            copy.RemoveDocumentMeta();
            return copy;
        }
        #endregion

        #region Properties Non - Serializable

       
        [JsonIgnore]
        public TemplateItem TemplateItem { get; set; }


        [JsonIgnore]
        private string _computedSearchField;

       
        [JsonIgnore]
        public OperationType OperationType { get; set; } = OperationType.Undefined;

        #endregion

        #region Properties
        [JsonPropertyName("_id")]
        [DataMember(Order = 1)]
        public string Id { get; set; }

        [DataMember(Order = 2)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("cn")]
        public string ClassName { get; set; }

        [DataMember(Order = 3)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("c")]
        public string Culture { get; set; }

        [DataMember(Order = 4)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("fp")]
        public string FullPath { get; set; }

        [DataMember(Order = 5)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("n")]
        public string Name { get; set; }

        [DataMember(Order = 6)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("ct")]
        public long CreationTimeStamp { get; set; }

        [DataMember(Order = 7)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("mt")]
        public long ModificationTimestamp { get; set; }

        [DataMember(Order = 8)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("pid")]
        public string ParentId { get; set; }

        [DataMember(Order = 9)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("pp")]
        public string ParentPath { get; set; }

        [DataMember(Order = 10)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("p")]
        public string PIMId { get; set; }

        [DataMember(Order = 11)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("cb")]
        public string CreatedBy { get; set; }

        [DataMember(Order = 12)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("ub")]
        public string UpdatedBy { get; set; }

        [DataMember(Order = 13)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("op")]
        public string OrganizationParent { get; set; }

        [DataMember(Order = 14)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("tid")]
        public string TenantId { get; set; }

        [JsonPropertyName("sf")]
        [DataMember(Order = 15)]
        public Dictionary<string, string> SharedFields { get; set; } = new Dictionary<string, string>();

        [JsonPropertyName("lf")]
        [DataMember(Order = 16)]
        public Dictionary<string, string> LanguageFields { get; set; } = new Dictionary<string, string>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("ite")]
        [DataMember(Order = 17)]
        public bool IsTranslatedEntity { get; set; }

        [DataMember(Order = 18)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("pd")]
        public long PublishDate { get; set; }


        /// <summary>
        /// Returns Creation Date from CreationTimeStamp
        /// </summary>
        [JsonIgnore]
        public DateTime CreationDate
        {
            get
            {
                return GetCreationDate();
            }
        }

       
        [JsonIgnore]
        public string ContentDeltaString
        {
            get
            {
                return GetSharedField<string>("ContentDeltaString");
            }
            set
            {
                SetSharedField("ContentDeltaString", value.ToString(), addMeta: false);
            }
        }

        [JsonIgnore]
        public virtual string DisplayName
        {

            get { return GetDisplayName(); }
        }

        
        [JsonIgnore]
        public DocumentMeta ContentDelta
        {

            get
            {
                if (!String.IsNullOrEmpty(ContentDeltaString))
                {
                    return JsonSerializer.Deserialize<DocumentMeta>(ContentDeltaString);
                }
                else
                {
                    return null;
                }

            }
            set
            {
                if (value != null)
                {
                    ContentDeltaString = JsonSerializer.Serialize(value);
                }
            }
        }



        [JsonIgnore]
        public string DisplayTitle
        {
            get
            {
                return GetDisplayTitle();
            }
            set
            {
                SetDisplayTitle(value);
            }
        }
        [JsonIgnore]
        public string TransliteratedName
        {
            get
            {
                return GetField<string>("transliteratedname", null);
            }
            set
            {
                SetField("transliteratedname", value.ToString(), null);
            }
        }

        [JsonIgnore]
       
        public string ComputedSearchField
        {
            get
            {
                if (String.IsNullOrEmpty(_computedSearchField))
                {
                    ComputeSearchField();
                }
                return _computedSearchField;
            }
        }


        #endregion

        #region Indexer

        public virtual string this[string key] => GetField<string>(key.ToLower(), null);

        #endregion


        private string ConcateFieldData(string fieldName)
        {
            var fields = GetLanguageFields(this, fieldName);
            var text = string.Empty;
            foreach (var field in fields)
            {
                text += " " + field.Value;
            }
            return text;
        }
        private void ComputeSearchField()
        {
            string searchText = this.ConcateFieldData("displayname");
            searchText += " " + this.Name;
            searchText += " " + this.ConcateFieldData("displayTitle");
            searchText += " " + this.TransliteratedName;
            _computedSearchField = searchText.ToLower();
        }

        #region Getter/Setter

        private void AddSharedKey(string key, object value)
        {
            if (value != null)
            {
                SharedFields.Add(key, value.ToString());
            }
        }

        private void AddLangaugeKey(string key, object value)
        {
            if (value != null)
            {
                LanguageFields.Add(key, value.ToString());
            }
        }

        public virtual T GetField<T>(string fieldName, string culture = null, bool EnableFallback = true)
        {
            fieldName = FieldUtil.getCleanFieldKey(fieldName);
            string value;
            if (SharedFields.TryGetValue(fieldName, out value))
            {
                if (!String.IsNullOrEmpty(value))
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
            culture = CultureUtility.GetCultureName(culture);
            var key = $"{fieldName}_{culture}".ToLower();
            if (LanguageFields.TryGetValue(key, out value))
            {
                if (!String.IsNullOrEmpty(value))
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
            //implement Langaue Fallback with English Hardcoded for now
            if (EnableFallback)
            {
                value = GetFallabackValue(fieldName);
                if (!String.IsNullOrEmpty(value))
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
            return default(T);
        }
        public virtual T GetLanguageField<T>(string fieldName, string culture = null)
        {
            string value = string.Empty;
            fieldName = FieldUtil.getCleanFieldKey(fieldName);

            //We need to support translated Entities but in a single language so we will check for is translated
            var key = fieldName;
            if (this.IsTranslatedEntity)
            {
                culture = CultureUtility.GetCultureName(culture);
                key = $"{fieldName}_{culture}".ToLower();
            }
            if (LanguageFields.TryGetValue(key, out value))
            {
                if (!String.IsNullOrEmpty(value))
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
            return default(T);
        }
        public virtual T GetSharedField<T>(string fieldName)
        {
            var key = FieldUtil.getCleanFieldKey(fieldName);
            string value;
            if (SharedFields.TryGetValue(key, out value))
            {
                if (!String.IsNullOrEmpty(value))
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
            return default(T);
        }
        public virtual void SetLanguageField(string fieldName, object value, string culture = null, bool addMeta = true)
        {
            var key = FieldUtil.getCleanFieldKey(fieldName);
            culture = CultureUtility.GetCultureName(culture);
            key = $"{key}_{culture}".ToLower();
            FieldOperationType fieldOperationType = FieldOperationType.Undefined;
            string OldValue = string.Empty;
            if (value != null)
            {
                if (LanguageFields.ContainsKey(key))
                {
                    fieldOperationType = FieldOperationType.UpdateField;
                    OldValue = LanguageFields[key];
                    LanguageFields.Remove(key);
                }
                else
                {
                    fieldOperationType = FieldOperationType.CreatedNewField;
                }
                AddLangaugeKey(key, value);
            }
            else
            {
                if (LanguageFields.ContainsKey(key))
                {
                    fieldOperationType = FieldOperationType.DeleteField;
                    OldValue = LanguageFields[key];
                    LanguageFields.Remove(key);
                }
            }
            if (addMeta)
            {
                AddFieldMeta(fieldOperationType, fieldName, OldValue, value?.ToString(), culture);
            }
        }
        public virtual void SetSharedField(string fieldName, object value, bool addMeta = true)
        {
            var key = FieldUtil.getCleanFieldKey(fieldName);
            FieldOperationType fieldOperationType = FieldOperationType.Undefined;
            string OldValue = string.Empty;
            if (value != null)
            {
                if (SharedFields.ContainsKey(key))
                {
                    fieldOperationType = FieldOperationType.UpdateField;
                    OldValue = SharedFields[key];
                    SharedFields.Remove(key);
                }
                else
                {
                    fieldOperationType = FieldOperationType.CreatedNewField;
                }
                AddSharedKey(key, value);
            }
            else
            {
                if (SharedFields.ContainsKey(key))
                {
                    fieldOperationType = FieldOperationType.DeleteField;
                    OldValue = SharedFields[key];
                    SharedFields.Remove(key);
                }
            }
            if (addMeta)
            {
                AddFieldMeta(fieldOperationType, key, OldValue, value?.ToString());
            }
        }
        public virtual void SetField(string fieldName, object value, string culture = null)
        {
            //In order to set a  field, user must either pass culture or Content Must have Template Assigned
            if (this.IsTranslatedEntity)
            {
                if (TemplateItem == null && string.IsNullOrEmpty(culture))
                {
                    Log.Error($"Can not set field for {fieldName} for item {this.Name} with id {this.Id} as one of either culture or template Item must be supplied for translated Entities. ", nameof(this.Name));
                    throw new ArgumentNullException(nameof(TemplateItem));
                }
                if (string.IsNullOrEmpty(culture))
                {
                    var fieldItem = TemplateItem.GetFieldDefinition(fieldName);
                    SetField(fieldItem, value, culture);
                }
                else
                {
                    SetLanguageField(fieldName, value, culture);
                }
            }
            else
            {
                SetSharedField(fieldName, value);
            }

        }
        public virtual void SetField(FieldItem fieldItem, object value, string culture)
        {
            var key = FieldUtil.getCleanFieldKey(fieldItem.Name);
            culture = CultureUtility.GetCultureName(culture);
            if (!this.IsTranslatedEntity || !fieldItem.IsTranslated)
            {
                SetSharedField(fieldItem.Name, value);
                return;
            }
            if (fieldItem.IsTranslated)
            {
                SetLanguageField(key, value, culture);
            }
        }

        #endregion

        #region Key Utilities


        public virtual bool RemoveSharedField(string fieldName)
        {
            var fieldname = FieldUtil.getCleanFieldKey(fieldName);
            if (HasSharedKey(fieldname))
            {
                SharedFields.Remove(fieldname);
                return true;
            }
            return false;
        }
        public virtual bool RemoveLanguageField(string fieldName, string culture)
        {
            var fieldname = FieldUtil.getCleanFieldKey(fieldName);
            if (!String.IsNullOrEmpty(culture)) //We are looking for specific culture only
            {
                if (HasLanguageKey(fieldname, culture))
                {
                    LanguageFields.Remove($"{fieldname}_{culture}".ToLower());
                    return true;
                }
            }
            else
            {
                if (HasLanguageKey(fieldname, culture))
                {
                    var key = $"{FieldUtil.getCleanFieldKey(fieldName)}_";

                    var candidateKeys = LanguageFields.Keys.Where(x => x.StartsWith(key)).ToList();
                    foreach (var languageKey in candidateKeys)
                    {
                        LanguageFields.Remove(languageKey);
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes a field for ContentItems dictionary collections
        /// </summary>
        /// <param name="fieldName"></param>
        public virtual bool RemoveField(string fieldName, string culture)
        {
            return RemoveSharedField(fieldName) || RemoveLanguageField(fieldName, culture);
        }


        public virtual bool HasSharedKey(string fieldName)
        {
            fieldName = FieldUtil.getCleanFieldKey(fieldName);
            return SharedFields.Keys.Contains(fieldName);
        }
        public virtual bool HasLanguageKey(string fieldName, string culture)
        {
            fieldName = FieldUtil.getCleanFieldKey(fieldName);

            if (!string.IsNullOrEmpty(culture))
            {
                fieldName = $"{fieldName}_{culture}".ToLower();
                return LanguageFields.ContainsKey(fieldName);
            }
            else
            {
                var key = $"{fieldName}_";
                return LanguageFields.Keys.Any(x => x.StartsWith(key));
            }
        }
        public virtual bool HasKey(string fieldName, string culture = null)
        {
            return HasSharedKey(fieldName) || HasLanguageKey(fieldName, culture);
        }
        public virtual bool HasSharedValue(string fieldName)
        {
            if (HasSharedKey(fieldName))
            {
                return !string.IsNullOrEmpty(GetSharedField<string>(fieldName));
            }
            return false;
        }

        public virtual bool HasLanguageValue(string fieldName, string culture)
        {
            if (!String.IsNullOrEmpty(culture))
            {
                if (HasLanguageKey(fieldName, culture))
                {
                    return !string.IsNullOrEmpty(GetLanguageField<string>(fieldName, culture));
                }
            }
            else
            {
                var foundLanguageKeys = HasLanguageKey(fieldName, culture);
                var key = $"{FieldUtil.getCleanFieldKey(fieldName)}_".ToLower();
                if (foundLanguageKeys)
                {
                    var candidateKeys = LanguageFields.Keys.Where(x => x.StartsWith(key)).ToList();
                    foreach (var languageKey in candidateKeys)
                    {
                        if (!String.IsNullOrEmpty(LanguageFields[languageKey]))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public virtual bool HasValue(string fieldName, string culture = null)
        {
            return HasSharedValue(fieldName) || HasLanguageValue(fieldName, culture);
        }

        public string GetLocalFallbackValue(BaseItem fallbackItem, string fieldName)
        {
            if (HasValue(fieldName))
            {
                return this.GetField<string>(fieldName);
            }
            var contextField = this.TemplateItem.GetFieldDefinition(fieldName);
            if (contextField.IsInherited)
            {
                return fallbackItem.GetField<string>(fieldName);
            }
            else return string.Empty;
        }

        #endregion

        #region Entity methods
        public DateTime GetCreationDate()
        {
            if (CreationTimeStamp == 0)
            {
                Log.Warning("Creation Timestamp is not on item of type {@className}:{@name} with {@id} for org {@orgId} to read CreationDate", this.ClassName, this.Name, this.Id, this.OrganizationParent);
                throw new ArgumentNullException("Creation Timestamp must be set");
            }
            var creationDate = new DateTime(CreationTimeStamp, DateTimeKind.Utc);
            return new DateTime(creationDate.Year,creationDate.Month,creationDate.Day);
        }

        public virtual string GetDisplayTitle(string culture = null)
        {
            culture = CultureUtility.GetCultureName(culture);
            //Read from Current Entity if It has been populated

            var key = $"displayTitle_{culture}".ToLower();
            var result = GetLanguageField<string>("displayTitle", culture);
            if (string.IsNullOrEmpty(result))
            {
                return GetDisplayName(culture);
            }
            else
            {
                return GetSantizedText(result);
            }


        }

        public void SetDisplayTitle(string value, string culture = "en")
        {

            //Set in Language Dictionary
            SetLanguageField("displayTitle", value, culture);

        }

        public virtual string GetDisplayName(string culture = null)
        {
            culture = CultureUtility.GetCultureName(culture);

            var result = GetField<string>("displayName", culture);
            if (string.IsNullOrEmpty(result))
            {
                return GetSantizedText(this.Name);
            }
            else
            {
                return GetSantizedText(result);
            }
        }

        public void SetDisplayName(string value, bool addMeta, string culture = null)
        {
            SetLanguageField("displayName", value, culture, addMeta);
        }


        /// <summary>
        /// Updates the Model with Incoming Values, Incoming Values are taken as truth
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public BaseItem UpdateFrom(BaseItem item)
        {

            //this.Id = updateValue(item.Id, this.Id);
            //this.ClassName = UpdateValue(item.ClassName, this.ClassName);
            //this.Culture = UpdateValue(item.Culture, this.Culture);
            //this.FullPath = UpdateValue(item.FullPath, this.FullPath);
            this.Name = UpdateValue(item.Name, this.Name);
            this.SetDisplayName(UpdateValue(item.DisplayName, this.DisplayName), false);
            if (this.CreationTimeStamp == 0)
            {
                this.CreationTimeStamp = item.CreationTimeStamp;
            }
            //this.CreationTimeStamp = UpdateCreationTimeStamp(item.CreationTimeStamp, this.CreationTimeStamp);
            this.ModificationTimestamp = UpdateCreationTimeStamp(DateTime.UtcNow.Ticks, this.CreationTimeStamp);
            //this.ParentId = UpdateValue(item.ParentId, this.ParentId);
            //this.ParentPath = UpdateValue(item.ParentPath, this.ParentPath);
            //this.CreatedBy = UpdateValue(item.CreatedBy, this.CreatedBy);
            this.UpdatedBy = UpdateValue(item.UpdatedBy, this.UpdatedBy);
            this.OrganizationParent = UpdateValue(item.OrganizationParent, this.OrganizationParent);
            this.TenantId = UpdateValue(item.TenantId, this.TenantId);
            //this.PIMId =  item.PIMId;

            this.LanguageFields = UpdateLanguageVersionItemField(item);
            this.SharedFields = UpdateSharedItemField(item);

            return this;
        }
        public virtual string UpdateValue(string newValue, string OldValue)
        {
            if (String.IsNullOrEmpty(newValue))
            {
                return OldValue;
            }
            else
            {
                return newValue;
            }
        }
        public virtual long UpdateCreationTimeStamp(long newValue, long OldValue)
        {
            if (newValue > 0)
            {
                return newValue;
            }
            else if (OldValue > 0)
            {
                return OldValue;
            }
            else
            {
                return DateTime.UtcNow.Ticks;
            }
        }

        public virtual Dictionary<string, string> UpdateSharedItemField(BaseItem item)
        {
            Dictionary<string, string> updatedSharedFields = new Dictionary<string, string>();

            var allKeys = item.SharedFields.Keys.Union(this.SharedFields.Keys);

            //Navigate over incoming dictionary compare with old values if any
            foreach (var key in allKeys)
            {
                var fieldValue = UpdateValue(item.SharedFields.ContainsKey(key) ? item.SharedFields[key] : null, this.SharedFields.ContainsKey(key) ? this.SharedFields[key] : null);
                if (!String.IsNullOrEmpty(fieldValue))
                {
                    //Add to dictionary
                    updatedSharedFields.Add(key.ToLower(), fieldValue);
                }
            }
            return updatedSharedFields;
        }
        public virtual Dictionary<string, string> UpdateLanguageVersionItemField(BaseItem item)
        {
            Dictionary<string, string> languageUpdatedfields = new Dictionary<string, string>();
            var allKeys = item.LanguageFields.Keys.Union(this.LanguageFields.Keys);
            //Navigate over incoming dictionary compare with old values if any
            foreach (var key in allKeys)
            {
                var fieldValue = UpdateValue(item.LanguageFields.ContainsKey(key) ? item.LanguageFields[key] : null, this.LanguageFields.ContainsKey(key) ? this.LanguageFields[key] : null);
                if (!String.IsNullOrEmpty(fieldValue))
                {
                    //Add to dictionary
                    languageUpdatedfields.Add(key.ToLower(), fieldValue);
                }
            }
            return languageUpdatedfields;
        }
        //This method is required for Grpc
        public virtual BaseItem ToBaseItem(bool withTemplate = false)
        {
            BaseItem item = new BaseItem();
            item.Init(this);
            return item;
        }

        /// <summary>
        /// The method is used to convert a BaseItems to a strongly typed object such as PriceItem so that we can invoke specialized methods from them
        /// </summary>
        /// <remarks>
        /// The specialized object must be in the name space Aarya.Foundation.ContentTypes.Types and must have names in the pattern PriceItem where Price is ClassName.
        /// </remarks>
        public virtual object ToSpecializedType(string fullyQualifiedTypeName = "")
        {
            if (string.IsNullOrEmpty(fullyQualifiedTypeName))
            {
                fullyQualifiedTypeName = $"Aarya.Foundation.ContentTypes.Types.{this.ClassName}Item, Aarya.Foundation.ContentTypes";
                Type itemType = Type.GetType(fullyQualifiedTypeName);
                if (itemType == null)
                {
                    Log.Debug($"Failed to find type {fullyQualifiedTypeName}", nameof(ToSpecializedType));
                    return null;
                }
                dynamic specializedObject = Activator.CreateInstance(itemType);
                if (specializedObject is BaseItem)
                {
                    specializedObject.Init(this);
                    return specializedObject;
                }
            }
            return null;
        }


        /// <summary>
        /// Merges fields of source into current item, 
        /// suitable to to merge fields values such as field overwritten at any level or fields from two different items
        /// </summary>
        /// <param name="sourceItem"></param>
        /// <returns></returns>
        public BaseItem MergeFields(BaseItem sourceItem)
        {
            this.TenantId = sourceItem.TenantId;
            this.OrganizationParent = sourceItem.OrganizationParent;
            //Merge Shared fields
            foreach (var sharedField in sourceItem.SharedFields)
            {
                if (this.SharedFields.ContainsKey(sharedField.Key))
                {
                    //Overwrite value 
                    this.SharedFields[sharedField.Key] = sharedField.Value;
                }
                else
                {
                    this.AddSharedKey(sharedField.Key, sharedField.Value);
                }
            }

            foreach (var languageField in sourceItem.LanguageFields)
            {
                if (this.LanguageFields.ContainsKey(languageField.Key))
                {
                    //Overwrite value 
                    this.LanguageFields[languageField.Key] = languageField.Value;
                }
                else
                {
                    this.AddLangaugeKey(languageField.Key, languageField.Value);
                }
            }

            //Merge Langauge Fields

            return this;
        }
        public virtual BaseItem ToTranslatedEntity()
        {
            //If the incoming entity is already Translated skip key mapping
            var translatedEntity = new BaseItem(this);
            if (translatedEntity.IsTranslatedEntity)
            {
                return translatedEntity;
            }

            Dictionary<string, string> languageUpdatedfields = new Dictionary<string, string>();
            foreach (var key in this.LanguageFields.Keys)
            {
                languageUpdatedfields.Add($"{key}_{Culture}".ToLower(), this.LanguageFields[key]);
            }
            translatedEntity.LanguageFields = languageUpdatedfields;
            translatedEntity.IsTranslatedEntity = true;
            return translatedEntity;
        }

        public virtual BaseItem ToTranslatedEntity(bool cleanId)
        {
            var result = ToTranslatedEntity();
            if (cleanId)
            {
                var id = result.Id.Split("_")[0];
                if (!string.IsNullOrEmpty(id))
                {
                    result.Id = id;
                }
            }
            return result;
        }

        public virtual BaseItem ToClassType()
        {
            //If the incoming entity is already Translated skip key mapping
            //var contentType = Type.GetType($"Aarya.Foundation.ContentTypes.Types.{}, Aarya.Foundation.ContentTypes");

            var translatedEntity = new BaseItem(this);
            if (translatedEntity.IsTranslatedEntity)
            {
                return translatedEntity;
            }

            Dictionary<string, string> languageUpdatedfields = new Dictionary<string, string>();
            foreach (var key in this.LanguageFields.Keys)
            {
                languageUpdatedfields.Add($"{key}_{Culture}".ToLower(), this.LanguageFields[key]);
            }
            translatedEntity.LanguageFields = languageUpdatedfields;
            translatedEntity.IsTranslatedEntity = true;
            return translatedEntity;
        }

        public virtual BaseItem ToLanguageVersion(string culture)
        {
            BaseItem item = new BaseItem();
            item.Init(this);
            item.Culture = culture;
            return item;
        }

        public void CopyLanguageField(BaseItem source, string fieldName)
        {

            var fields = source.LanguageFields.Where(x => x.Key.StartsWith($"{fieldName.ToLower()}")).ToDictionary(x => x.Key, x => x.Value);
            foreach (var field in fields)
            {
                if (this.LanguageFields.ContainsKey(field.Key))
                {
                    this.LanguageFields.Remove(field.Key);
                }
                this.LanguageFields.Add(field.Key, field.Value);
            }
        }
        public Dictionary<string, string> GetLanguageFields(BaseItem source, string fieldName)
        {
            var fields = source.LanguageFields.Where(x => x.Key.StartsWith($"{fieldName.ToLower()}"))?.ToDictionary(x => x.Key, x => x.Value);

            return fields;
        }

        public Dictionary<string, string> GetLanguageFields(string fieldName)
        {
            var fields = LanguageFields.Where(x => x.Key.StartsWith($"{fieldName.ToLower()}"))?.ToDictionary(x => x.Key, x => x.Value);

            return fields;
        }

        public virtual BaseItem GetLanguageVersion(string culture)
        {
            BaseItem item = new BaseItem();
            item.Init(this);
            item.Culture = culture;
            item.LanguageFields = RetainLanguageVersion(this, culture);
            return item;
        }

        private Dictionary<string, string> RetainLanguageVersion(BaseItem source, string culture)
        {
            return source.LanguageFields.Where(x => x.Key.Contains(culture.ToLower())).ToDictionary(x => x.Key, x => x.Value);
        }

        public virtual BaseItem AddLanguageVersion(BaseItem source)
        {
            //this.LanguageFields.Concat(source.LanguageFields);
            foreach (var key in source.LanguageFields.Keys)
            {
                if (!this.LanguageFields.ContainsKey(key))
                {
                    AddLangaugeKey(key, source.LanguageFields[key]);
                }
            }
            return this;
        }

        

        public string GetSantizedText(string value)
        {
            return WebUtility.HtmlDecode(value);
        }
        //public virtual BaseItem ToTranslatedEntity(bool Copy = false, bool withTemplate = false)
        //{
        //    if (this.TemplateItem == null)
        //    {
        //        AaryaLog.Error("The entity {@this.Name} with {@id} has no TemplateItem set. Can't be translated!", nameof(ToTranslatedEntity), this.Name, this.Id);
        //        return null;
        //    }

        //    if (this.IsTranslatedEntity)
        //    {
        //        AaryaLog.Warn("The entity {@this.Name} with {@id} is already translated", nameof(ToTranslatedEntity), this.Name, this.Id);
        //        return this;
        //    }
        //    if (string.IsNullOrEmpty(this.Id) || string.IsNullOrEmpty(PIMId) || string.IsNullOrEmpty(ClassName))
        //    {
        //        AaryaLog.Error("The entity {@this.Name} with {@id} is missing ID/PIMId or class Name. Invalid Entity Exiting!", nameof(ToTranslatedEntity), this.Name, this.Id);
        //        return null;
        //    }
        //    //Validate if Entity Culture is Set or not
        //    if (String.IsNullOrEmpty(this.Culture))
        //    {
        //        AaryaLog.Warn("The entity {@this.Name} with {@id} is has no culture set. Treating it as English!", nameof(ToTranslatedEntity), this.Name, this.Id);
        //    }
        //    // if copy is not true, we return a new object
        //    BaseItem translatedEntity = null;
        //    if (!Copy)
        //    {
        //        translatedEntity = new BaseItem(this.ClassName, true);
        //    }
        //    else
        //    {
        //        translatedEntity = new BaseItem();
        //        translatedEntity.Id = this.Id;
        //        translatedEntity.ClassName = this.ClassName;
        //        translatedEntity.PIMId = this.PIMId;
        //        translatedEntity.CreationDate = this.CreationDate;
        //        translatedEntity.ModificationDate = this.ModificationDate;
        //        translatedEntity.IsTranslatedEntity = true;
        //    }
        //    if (string.IsNullOrEmpty(this.Culture))
        //    {
        //        translatedEntity.Culture = "en";
        //    }
        //    else
        //    {
        //        translatedEntity.Culture = this.Culture;
        //    }

        //    //Provide the new Entity the Template Item

        //    translatedEntity.TemplateItem = this.TemplateItem;

        //    //Begin Translation Now
        //    translatedEntity.FullPath = this.FullPath;
        //    translatedEntity.Name = this.Name;
        //    translatedEntity.SetDisplayName(this.DisplayName, this.Culture);
        //    translatedEntity.ParentId = this.ParentId;
        //    translatedEntity.ParentPath = this.ParentPath;
        //    translatedEntity.OrganizationParent = this.OrganizationParent;
        //    translatedEntity.TenantId = this.TenantId;
        //    translatedEntity.CreatedBy = this.CreatedBy;
        //    translatedEntity.UpdatedBy = this.UpdatedBy;
        //    translatedEntity.fillTranslatedFields(translatedEntity, this.ContentItem);
        //    if (withTemplate)
        //    {
        //        translatedEntity.TemplateItem = this.TemplateItem;
        //    }
        //    //Finally remove the ContentItem Field
        //    translatedEntity.ContentItem = null;
        //    return translatedEntity;
        //}
        public virtual void SetDocumentModifier(string partnerUserId)
        {
            if (String.IsNullOrEmpty(this.CreatedBy))
            {
                this.CreatedBy = partnerUserId;
            }
            this.UpdatedBy = partnerUserId;
        }
        public virtual void UpdateTimeStamps()
        {
            this.ModificationTimestamp = System.DateTime.UtcNow.Ticks;
        }


        private void FillTranslatedFields(BaseItem translatedEntity, Dictionary<string, string> contentItem)
        {
            foreach (var key in contentItem.Keys)
            {
                try
                {
                    var fieldItem = this.TemplateItem.GetFieldDefinition(key);
                    if (fieldItem == null)
                    {
                        Log.Warning($"The field with {key} is not defined in tempalate {TemplateItem.Name}, putting the field in shared collection!", nameof(FillTranslatedFields));
                        if (Fields.SharedFields.Contains(key))
                        {
                            translatedEntity.SetSharedField(key, contentItem[key]);
                        }
                        else if (Fields.TranslatedFields.Contains(key))
                        {
                            translatedEntity.SetLanguageField(key, contentItem[key], translatedEntity.Culture);
                        }
                        else
                        {
                            Log.Warning($"The field with {key} is not defined in tempalate and not in Fields constants {TemplateItem.Name},Skipping the field! The field should be added in the template!", nameof(FillTranslatedFields));
                        }
                    }
                    else
                    {
                        translatedEntity.SetField(fieldItem, contentItem[key], translatedEntity.Culture);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Failed Filling Translated field", nameof(FillTranslatedFields));
                }

            }
        }

        #endregion

        #region Transactional Translated Entity
        public string GetFallabackValue(string fieldName, string fallbackCulture = "en")
        {
            string fallbackValue = String.Empty;
            fallbackValue = GetLanguageField<string>(fieldName, fallbackCulture);
            if (string.IsNullOrEmpty(fallbackValue))
            {
                //Let's match key and try to find any value
                if (LanguageFields.Keys.Any(x => x.StartsWith(fieldName)))
                {
                    fallbackValue = LanguageFields.FirstOrDefault(x => x.Key.StartsWith(fieldName)).Value;
                }
            }
            //if (string.IsNullOrEmpty(fallbackValue))
            //{
            //    AaryaLog.Warn($"No fallback value was found in for the field {fieldName} in the content {this.PIMId} for OraganizationParent {this.OrganizationParent} for tenant {this.TenantId}", nameof(BaseItem));
            //}
            return fallbackValue;
        }

        #endregion

        #region Object Overridings

        public override bool Equals(object o)
        {
            var other = o as BaseItem;
            return other?.Id == this.Id;
        }
        public override string ToString()
        {
            return this.DisplayName;
        }


        public string ToDoc(bool CleanMeta = false)
        {
            if (CleanMeta)
            {
                this.RemoveDocumentMeta();
            }

            return JsonSerializer.Serialize(this);
        }
        public BaseItem ToBaseItem(string baseItemString)
        {
            if (baseItemString == null)
            {
                return null;
            }
            return JsonSerializer.Deserialize<BaseItem>(baseItemString);
        }



        public bool Equals(BaseItem other)
        {
            return other?.Id == this.Id;
        }

        public override int GetHashCode() => this.DisplayName?.GetHashCode() ?? 0;
        #endregion

        #region Validity

        public bool IsValidDomainDocument(string raisedBy = "admin")
        {
            BaseItem domainEntity = this.ToBaseItem();
            bool isValid = true;
            if (String.IsNullOrEmpty(domainEntity.OrganizationParent))
            {
                Log.Error("Organization Parent can't be null: {@baseItem}", nameof(Items.BaseItem), domainEntity);
                return false;

            }
            if (String.IsNullOrEmpty(domainEntity.TenantId))
            {
                Log.Error("TenantId can't be null: {@baseItem}", nameof(Items.BaseItem), domainEntity);
                return false;

            }
            if (String.IsNullOrEmpty(domainEntity.Id))
            {
                Log.Error("Id can't be null: {@baseItem}", nameof(Items.BaseItem), domainEntity);
                return false;
            }
            if (String.IsNullOrEmpty(domainEntity.ClassName))
            {
                Log.Error("ClassName can't be null: {@baseItem}", nameof(Items.BaseItem), domainEntity);
                return false;

            }
            if (String.IsNullOrEmpty(domainEntity.UpdatedBy))
            {
                Log.Error("UpdatedBy can't be null: {@baseItem}", nameof(Items.BaseItem), domainEntity);
                return false;

            }
            if (this.TemplateItem != null)
            {
                this.TemplateItem = null;
            }
            return isValid;
        }


        public void AddFieldMeta(FieldOperationType operationType, string fieldName, string oldValue, string newValue, string culture = null)
        {
            DocumentMeta documentMeta = new DocumentMeta();
            if (this.ContentDelta != null)
            {
                documentMeta = this.ContentDelta;
            }
            if (documentMeta.FieldChanges == null)
            {
                documentMeta.FieldChanges = new List<FieldChange>();
            }

            FieldChange fieldChange = new FieldChange();
            fieldChange.FieldName = fieldName;
            fieldChange.OldValue = oldValue;
            fieldChange.NewValue = newValue;

            if (culture != null)
            {
                fieldChange.Culture = culture;
            }
            fieldChange.FieldOperationType = operationType;

            //Before Adding FieldMeta we must ensure it is not duplicate and should not update OperationType if it was the previous value is create
            updateFieldMeta(documentMeta, fieldChange);
            this.ContentDelta = documentMeta;

        }
        private void updateFieldMeta(DocumentMeta documentMeta, FieldChange fieldChange)
        {
            var existingChange = documentMeta.FieldChanges.FirstOrDefault(x => x.FieldName == fieldChange.FieldName);
            if (existingChange == null)
            {
                //just add
                documentMeta.FieldChanges.Add(fieldChange);
            }
            else
            {
                //We have this field already
                if (existingChange.FieldOperationType == FieldOperationType.CreatedNewField)
                {
                    //copy data from fieldChange to existing
                    existingChange.NewValue = fieldChange.NewValue;
                }
            }
        }

        public void RemoveDocumentMeta()
        {
            this.RemoveSharedField("ContentDeltaString");
        }

        //public bool GetImageDocumentContentDelta(BaseItem oldItem, FieldOperationType fieldOperationType)
        //{
        //    if (this.SharedFields["imagegallery"] != oldItem.SharedFields["imagegallery"])
        //    {
        //        AddFieldMeta(fieldOperationType, "imagegalleryexpanded", oldItem.SharedFields["imagegalleryexpanded"], this.SharedFields["imagegalleryexpanded"]);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;

        //    }
        //}
        public void EnrichDocumentMeta()
        {
            if (this.ContentDelta != null)
            {
                this.ContentDelta.OrganizationParent = this.OrganizationParent;
                this.ContentDelta.TenantId = this.TenantId;
                this.ContentDelta.ClassName = this.ClassName;
                this.ContentDelta.Id = this.Id;
                this.ContentDelta.UpdatedBy = this.UpdatedBy;
                this.ContentDelta.ModificationTimeStamp = this.ModificationTimestamp.ToString();
            }
        }
        #endregion
    }
}
