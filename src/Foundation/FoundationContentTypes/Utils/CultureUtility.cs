using System.Globalization;

namespace Microservices.Foundation.ContentTypes.Utils
{
    public static class CultureUtility
    {
        public static string GetLocalizedTitle(this string titleString, string prefix = "")
        {
            string result = titleString;

            if (!string.IsNullOrEmpty(prefix))
            {
                if (CultureInfo.CurrentCulture.TextInfo.IsRightToLeft)
                {
                    result = $"{titleString} {prefix}";
                }
                else
                {
                    return $"{prefix} {titleString}";
                }
            }
            return result.Trim();
        }
        public static string GetCultureName(string culture)
        {
            string targetCulture = string.Empty;
            if (String.IsNullOrEmpty(culture))
            {
                targetCulture = CultureInfo.CurrentCulture.Name;
            }
            else
            {
                targetCulture = culture;
            }
            if (targetCulture.Contains("en") || String.IsNullOrEmpty(targetCulture))
            {
                targetCulture = "en";
            }
            return targetCulture;
        }
        public static bool IsBusinessEntityDefaultLangauge()
        {
            if (GetCultureName(null) == "en")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string GetDefaultBusinessEntityLangauge()
        {
            return "en";

        }

    }
}
