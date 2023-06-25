using Microservices.Foundation.ContentTypes.Items;
using Microservices.Foundation.ContentTypes.Types;

namespace Aarya.Foundation.ContentTypes.Types
{
    public class SectionItem : CommonEntityFields
    {
        public SectionItem()
        {

        }
        public SectionItem(BaseItem item, TemplateItem entityItem) : base(item)
        {
            TemplateItem = entityItem;
        }

        public List<FieldItem> Fields { get; set; } = new List<FieldItem>();

        #region Properties

        public bool ShowPartTitle
        {
            get
            {
                return GetSharedField<bool>("__showparttitle");
            }
            set
            {
                SetSharedField("__showparttitle", value);
            }
        }
        public bool BreakBySection
        {
            get
            {
                return GetSharedField<bool>("__breakbysection");
            }
            set
            {
                SetSharedField("__breakbysection", value);
            }
        }

        #endregion

        public List<ValidationRuleItem> Validation()
        {
            return new List<ValidationRuleItem>();
        }

        public List<FieldItem> GetFieldsToRenderAsParts()
        {
            return Fields.Where(x => x.RenderFieldAsPart).ToList();
        }

        public bool HasFieldsToRender(bool showAdvanced)
        {
            if (showAdvanced)
            {
                return Fields.Any(x => !x.IsHidden || x.IsAdvancedField);
            }
            else
            {
                var result = Fields.Any(x => !x.IsHidden && !x.IsAdvancedField);
                return result;
            }
        }
    }
}
