using Microservices.Foundation.ContentTypes.Enums;
using Microservices.Foundation.ContentTypes.Items;
using Microservices.Foundation.ContentTypes.Types;
using Serilog;
using System.Text.Json.Serialization;

namespace Aarya.Foundation.ContentTypes.Types
{
    public class FieldItem : CommonEntityFields
    {
        public FieldItem()
        {

        }
        public FieldItem(BaseItem item, TemplateItem entityItem, SectionItem sectionItem) : base(item)
        {
            TemplateItem = entityItem;
            SectionItem = sectionItem;
        }

        #region Propeties

        [JsonIgnore]
        public bool IsCascadingField
        {
            get
            {
                return HasValue("cascadeon");
            }

        }

        [JsonIgnore]
        public bool HasParentField
        {
            get
            {
                return HasValue("HasParentField");
            }

        }

        [JsonIgnore]
        public string FieldManager
        {
            get
            {
                return GetSharedField<string>("FieldManager");
            }
        }

        [JsonIgnore]
        public string GuideMessage
        {
            get
            {
                return GetLanguageField<string>("GuideMessage");
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
        public string Permissions
        {
            get
            {
                return GetSharedField<string>("Permissions");
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
        public bool AllowAddition
        {
            get
            {
                return !GetSharedField<bool>("__noaddition");
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
        public bool TagWithExpandedParent
        {
            get
            {
                return GetSharedField<bool>("TagWithExpandedParent");
            }
        }

        [JsonIgnore]
        public bool IsSystemGlobal
        {
            get
            {
                return GetSharedField<bool>("IsGlobal");
            }
        }

        [JsonIgnore]
        public string CascadeOn
        {
            get
            {
                return GetSharedField<string>("cascadeon");
            }
            set
            {
                SetSharedField("cascadeon", value);
            }
        }
        [JsonIgnore]
        public bool ShowFirstOptionPreselected
        {
            get
            {
                return GetSharedField<bool>("__ShowFirstOptionPreselected");
            }
            set
            {
                SetSharedField("__ShowFirstOptionPreselected", value);
            }
        }
        [JsonIgnore]
        public bool IsExpandedField
        {
            get
            {
                return GetSharedField<bool>("__isexpandedfield");
            }
            set
            {
                SetSharedField("__isexpandedfield", value);
            }
        }



        [JsonIgnore]
        public bool IsDefaultCardView
        {
            get
            {
                return GetSharedField<bool>("__IsDefaultCardView");
            }
            set
            {
                SetSharedField("__IsDefaultCardView", value);
            }
        }

        [JsonIgnore]
        /// <summary>
        /// If AutoSearch has to be enabled for the field
        /// </summary>
        public bool EnableAutoSearch
        {
            get
            {
                return GetSharedField<bool>("__EnableAutoSearch");
            }
        }

        [JsonIgnore]
        /// <summary>
        /// If AutoSearch has to be enabled for the field
        /// </summary>
        public int RenderInlineCount
        {
            get
            {
                return GetSharedField<int>("__RenderInlineCount");
            }
        }

        [JsonIgnore]
        /// <summary>
        /// Shows the search box if count is  higher than the count configured
        /// </summary>
        public int EnableSearchCount
        {
            get
            {
                return GetSharedField<int>("__EnableSearchCount");
            }

        }

        [JsonIgnore]
        public bool IsAdvancedField
        {
            get
            {
                return GetSharedField<bool>("__isadvancedfield");
            }
        }

        [JsonIgnore]
        public bool EnableGTMTracking
        {
            get
            {
                return GetSharedField<bool>("GTMTrack");
            }
        }

        [JsonIgnore]
        public bool EnableAMPTracking
        {
            get
            {
                return GetSharedField<bool>("AMPTrack");
            }
        }
        //public bool IsSearchFilter
        //{
        //    get
        //    {
        //        return Get<bool>("__issearchfilter");
        //    }
        //    set
        //    {
        //        Set("__issearchfilter", value);
        //    }
        //}
        [JsonIgnore]
        public bool RenderFieldAsPart
        {
            get
            {
                return GetSharedField<bool>("__RenderFieldAsPart");
            }
        }

        [JsonIgnore]
        public bool IsTranslated
        {
            get
            {
                return !GetSharedField<bool>("shared");
            }
        }

        [JsonIgnore]
        public string FieldType
        {
            get
            {
                return GetSharedField<string>("type");
            }
        }


        [JsonIgnore]
        public string SourceType
        {
            get
            {
                var sourceType = GetSharedField<string>("__sourcetype");
                if (string.IsNullOrEmpty(sourceType) && IsExpandedField)
                {
                    Log.Logger.Warning("Source Type is not set on field item {@id} with name {@name}", Id, Name);
                }
                return sourceType;
            }

        }

        [JsonIgnore]
        public string UrlPrefix
        {
            get
            {
                return GetSharedField<string>("__urlprefix");
            }

        }

        [JsonIgnore]
        public string ControlType
        {
            get
            {
                return GetSharedField<string>("__controlType");
            }

        }

        [JsonIgnore]
        public string PlaceholderText
        {
            get
            {
                return GetLanguageField<string>("__placeholderValue");
            }
        }
        [JsonIgnore]
        public string OpenTo
        {
            get
            {
                return GetSharedField<string>("opento");
            }
        }
        [JsonIgnore]
        public string GridMessage
        {
            get
            {
                return GetSharedField<string>("GridMessage");
            }
        }

        [JsonIgnore]
        public bool AmPm
        {
            get
            {
                return GetSharedField<bool>("ampm");
            }
        }

        [JsonIgnore]
        public ControlTypes FieldControl
        {
            get
            {
                return GetControlName(this);
            }
        }



        [JsonIgnore]
        public SectionItem SectionItem { get; set; }

        [JsonIgnore]
        public List<ValidationRuleItem> Validations { get; set; } = new List<ValidationRuleItem>();

        [JsonIgnore]
        public bool IsRequired
        {
            get
            {
                return Validations.Any(x => x.ValidationType == ValidationTypes.Required);
            }
        }

        [JsonIgnore]
        public bool HasMinDuration
        {
            get
            {
                if (GetMinDuration() > TimeSpan.Zero)
                    return true;
                return false;
            }
        }

        public bool IsInherited { get; set; }

        public TimeSpan GetMinDuration()
        {
            TimeSpan minDuration = TimeSpan.Zero;
            var minDurationValidation = Validations.Where(x => x.ValidationType == ValidationTypes.MinDuration).FirstOrDefault();
            if (minDurationValidation != null)
            {
                var mindurationstring = minDurationValidation.GetParameter("minduration");
                if (!string.IsNullOrEmpty(mindurationstring))
                {
                    TimeSpan duration;
                    if (TimeSpan.TryParse(mindurationstring, out duration))
                    {
                        minDuration = duration;
                    }
                }
            }
            return minDuration;
        }
        [JsonIgnore]
        public bool HasMinTime
        {
            get
            {
                if (GetMinTime() > TimeSpan.Zero)
                    return true;
                return false;
            }
        }

        public TimeSpan GetMinTime()
        {
            TimeSpan minTime = TimeSpan.Zero;
            var minTimeValidation = Validations.Where(x => x.ValidationType == ValidationTypes.MinTime).FirstOrDefault();
            if (minTimeValidation != null)
            {
                var mintimestring = minTimeValidation.GetParameter("mintime");
                if (!string.IsNullOrEmpty(mintimestring))
                {
                    TimeSpan minTimeValue;
                    if (TimeSpan.TryParse(mintimestring, out minTimeValue))
                    {
                        minTime = minTimeValue;
                    }
                }
            }
            return minTime;
        }
        [JsonIgnore]
        public bool HasMaxTime
        {
            get
            {
                if (GetMaxTime() > TimeSpan.Zero)
                    return true;
                return false;
            }
        }

        public TimeSpan GetMaxTime()
        {
            TimeSpan maxTime = TimeSpan.Zero;
            var maxTimeValidation = Validations.Where(x => x.ValidationType == ValidationTypes.MaxTime).FirstOrDefault();
            if (maxTimeValidation != null)
            {
                var maxtimestring = maxTimeValidation.GetParameter("maxtime");
                if (!string.IsNullOrEmpty(maxtimestring))
                {
                    TimeSpan maxTimeValue;
                    if (TimeSpan.TryParse(maxtimestring, out maxTimeValue))
                    {
                        maxTime = maxTimeValue;
                    }
                }
            }
            return maxTime;
        }

        [JsonIgnore]
        public bool HasMinDate
        {
            get
            {
                if (GetMinDate() != null)
                    return true;
                return false;
            }
        }
        [JsonIgnore]
        public bool HasMaxDate
        {
            get
            {
                if (GetMaxDate() != null)
                    return true;
                return false;
            }
        }
        /// <summary>
        /// Shows the minimum date till which user can select
        /// </summary>
        public DateTime? GetMinDate()
        {
            DateTime? minDate = null;

            var minDateValidation = Validations.Where(x => x.ValidationType == ValidationTypes.MinDate).FirstOrDefault();
            if (minDateValidation != null)
            {
                var mindatestring = minDateValidation.GetParameter("mindate");
                if (!string.IsNullOrEmpty(mindatestring))
                {
                    DateTime minDateValue;
                    if (DateTime.TryParse(mindatestring, out minDateValue))
                    {
                        minDate = minDateValue;
                    }
                }
            }

            return minDate;
        }
        //public bool HasMaxDate
        //{
        //    get
        //    {
        //        if (GetMaxDate() != null)
        //            return true;
        //        return false;
        //    }
        //}




        /// <summary>
        /// Shows the maximum date till which user can select
        /// </summary>
        public DateTime? GetMaxDate()
        {
            DateTime? maxDate = null;
            var maxDateValidation = Validations.Where(x => x.ValidationType == ValidationTypes.MaxDate).FirstOrDefault();
            if (maxDateValidation != null)
            {
                var maxdatestring = maxDateValidation.GetParameter("maxdate");
                if (!string.IsNullOrEmpty(maxdatestring))
                {
                    DateTime maxDateValue;
                    if (DateTime.TryParse(maxdatestring, out maxDateValue))
                    {
                        maxDate = maxDateValue;
                    }
                }
            }
            return maxDate;
        }


        [JsonIgnore]
        /// <summary>
        /// True, if field is List with and exclusive with other fields
        /// </summary>
        public bool IsExclusive
        {
            get
            {
                return HasSharedValue("ExclusiveWith");
            }

        }

        [JsonIgnore]
        /// <summary>
        /// True, if field is List with and exclusive with other fields
        /// </summary>
        public string ExclusiveWith
        {
            get
            {
                return GetSharedField<string>("ExclusiveWith");
            }

        }

        [JsonIgnore]
        /// <summary>
        /// True, if field is List with MultiSelection enabled
        /// </summary>
        public bool IsMultiSelect
        {
            get
            {
                return GetSharedField<bool>("__EnableMultiSelect");
            }

        }
        /// <summary>
        /// If Guide Actions are configured for a template or field instance
        /// </summary>
        /// <returns></returns>
        public bool HasGuideActions()
        {
            return HasSharedValue("__GuideActions");

        }
        [JsonIgnore]
        public List<ContentItem> Datasource { get; set; } = new List<ContentItem> { };
        public static ControlTypes GetControlName(FieldItem field)
        {
            ControlTypes controlType = ControlTypes.Undefined;
            try
            {

                //If field already has a control type define just return
                if (!string.IsNullOrEmpty(field.ControlType))
                {
                    //Validate if the field can be split in ,
                    if (field.ControlType.Contains(","))
                    {
                        return ControlTypes.Dynamic;
                    }
                    object result;
                    Enum.TryParse(typeof(ControlTypes), field.ControlType, true, out result);
                    if (result != null)
                    {
                        return (ControlTypes)result;
                    }
                    else
                    {
                        return controlType;
                    }
                }

                if (string.IsNullOrEmpty(field.FieldType))
                {
                    Log.Error($"FieldType is not Set for field {field.Name}", nameof(FieldItem));
                    return controlType;
                }

                string fieldTypeName = field.FieldType.Replace(" ", "-").Replace("-", "").ToLower();

                object fieldResult;
                Enum.TryParse(typeof(FieldTypes), fieldTypeName, true, out fieldResult);
                if (fieldResult == null)
                {
                    return controlType;
                }

                var fieldType = (FieldTypes)fieldResult;

                switch (fieldType)
                {
                    case FieldTypes.SingleLineText:
                        {
                            if (field.IsTranslated)
                            {
                                controlType = ControlTypes.LocalizedTextField;
                            }
                            else
                            {
                                controlType = ControlTypes.TextField;
                            }

                            break;
                        }
                    case FieldTypes.MultiLineText:
                        {
                            if (field.IsTranslated)
                            {
                                controlType = ControlTypes.LocalizedMultilineTextField;
                            }
                            else
                            {
                                controlType = ControlTypes.MultilineTextField;
                            }
                            break;
                        }

                    case FieldTypes.RichText:
                        {
                            if (field.IsTranslated)
                            {
                                controlType = ControlTypes.LocalizedRichTextField;
                            }
                            else
                            {
                                controlType = ControlTypes.RichTextField;
                            };
                            break;
                        }
                    case FieldTypes.Checkbox:
                        {

                            controlType = ControlTypes.Switch;
                            break;
                        }
                    case FieldTypes.Link:
                        {
                            controlType = ControlTypes.Link;
                            break;
                        }
                    case FieldTypes.MultiImageField:
                        {
                            controlType = ControlTypes.Media;
                            break;
                        }
                    case FieldTypes.MultilistWithSearch:
                        {
                            if (field.ControlType == "POSSelector")
                            {
                                controlType = ControlTypes.POSSelector;
                            }
                            else
                            {
                                controlType = ControlTypes.ListSearch;
                            }
                            break;
                        }
                    case FieldTypes.Multilist:
                        {
                            controlType = ControlTypes.List;
                            break;
                        }
                    case FieldTypes.Droplist:
                        {
                            controlType = ControlTypes.List;
                            break;
                        }
                    case FieldTypes.Treelist:
                        {
                            controlType = ControlTypes.List;
                            break;
                        }
                    case FieldTypes.Droplink:
                        {
                            controlType = ControlTypes.List;
                            break;
                        }
                    //case FieldTypes.YouTubeVideo:
                    //    {
                    //        controlType = ControlTypes.Media;
                    //        break;
                    //    }
                    case FieldTypes.Image:
                        {
                            controlType = ControlTypes.Media;
                            break;
                        }
                    case FieldTypes.Icon:
                        {
                            controlType = ControlTypes.Media;
                            break;
                        }
                    case FieldTypes.Number:
                        {
                            controlType = ControlTypes.Numeric;
                            break;
                        }
                    case FieldTypes.Time:
                        {
                            //field.ControlType = field.GetSharedField<string>("__controltype");
                            if (field.ControlType == "TimeRange")
                            {
                                controlType = ControlTypes.TimeRange;
                            }
                            else
                            {
                                controlType = ControlTypes.Time;
                            }
                            break;
                        }
                    case FieldTypes.Date:
                        {
                            if (field.ControlType == "RangeCalender")
                            {
                                controlType = ControlTypes.RangeCalender;
                            }
                            else
                            {
                                controlType = ControlTypes.Calendar;
                            }
                            break;
                        }
                    case FieldTypes.Datetime:
                        {
                            controlType = ControlTypes.Calendar;
                            break;
                        }
                    default:
                        controlType = ControlTypes.Undefined;
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message, nameof(FieldItem));
            }
            return controlType;
        }
        #endregion

    }
}
