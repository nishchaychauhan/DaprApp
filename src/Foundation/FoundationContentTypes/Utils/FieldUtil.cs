using Microservices.Foundation.ContentTypes.Items;
using Serilog;

namespace Microservices.Foundation.ContentTypes.Utils
{
    public static class FieldUtil
    {
        public static string GetIds<T>(List<T> selectedItems) where T : BaseItem
        {
            string ids = string.Empty;
            foreach (var item in selectedItems)
            {
                if (selectedItems.LastOrDefault() == item)
                {
                    ids += item.Id;
                }
                else
                {
                    ids += item.Id + "|";
                }
            }
            return ids;
        }
        public static string GetIdString(List<string> ids)
        {
            var idsString = string.Empty;
            foreach (var id in ids)
            {
                if (ids.LastOrDefault() == id)
                {
                    idsString += id;
                }
                else
                {
                    idsString += id + "|";

                }
            }
            return idsString;
        }
        public static string[] GetIdsList(string Ids)
        {
            string[] ids = new string[] { };
            if (String.IsNullOrEmpty(Ids))
            {
                return ids;
            }
            return Ids.Split('|');
        }
        
        /// <summary>
        /// Remove an Id from a list of Ids stored as "|" separated and return updated id string
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string RemoveId(string Ids, string id)
        {
            var idList = GetIdsList(Ids).ToList();
            if (idList.Any(x => x == id))
            {
                idList.RemoveAll(x => x == id);
            }
            return GetIdString(idList);
        }

        public static string RemoveIds(string ids, List<string> removedIds)
        {
            var sourceIds = GetIdsList(ids).ToList();
            //We have something to match agains
            if(sourceIds.Any())
            {
                //Remove the Ids here
                sourceIds.RemoveAll(x => removedIds.Contains(x)); 
            }
            return GetIdString(sourceIds);
        }
        /// <summary>
        /// If a list of Ids contains the Id being supplied
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool HasId(string Ids, string id)
        {
            var idList = GetIdsList(Ids).ToList();
            if (idList.Any(x => x == id))
            {
                return true;
            }
            return false;
        }
        public static string AddId(string Ids, string id)
        {
            var idList = GetIdsList(Ids).ToList();
            //Only add if not already added
            if (idList?.Any() == false)
            {
                idList.Add(id);
            }
            else if (!idList.Any(x => x == id))
            {
                idList.Add(id);
            }
            else
            {
                return GetIdString(idList);
            }
            return GetIdString(idList);
        }
        public static string ConcateIds(string Ids, List<string> ids)
        {
            var idList = GetIdsList(Ids).ToList();
            //Only add if not already added

            if (idList?.Any() == true)
            {
                foreach (var id in ids)
                {
                    if (idList.Contains(id))
                    {
                        idList.RemoveAll(x => x == id);
                    }
                }
                idList.AddRange(ids);
            }
            else
            {
                return GetIdString(ids);
            }
            return GetIdString(idList);
        }
        public static bool Contains(BaseItem contentItem, string sourceFieldName, string targetFieldName)
        {
            bool result = false;
            //Valdiate if both field names are correct
            if (!contentItem.HasKey(sourceFieldName))
            {
                Log.Error($"Invaild sourcefieldName {sourceFieldName}", nameof(Contains));
                return result;
            }
            //Valdiate if both field names are correct
            if (!contentItem.HasKey(targetFieldName))
            {
                Log.Warning($"Invaild targetFieldName {targetFieldName}. If field is not cascading Ignore this!", nameof(Contains));
                return result;
            }

            //read values from both fields
            var sourceFieldItems = GetIdsList(contentItem.GetSharedField<string>(sourceFieldName)).ToList();
            var targetFieldItems = GetIdsList(contentItem.GetSharedField<string>(targetFieldName)).ToList();

            //Validate if any value is found return true
            result = sourceFieldItems.Intersect(targetFieldItems).Any();
            return result;
        }
        public static bool Contains(BaseItem contentItem, List<string> sourceItemIds, string targetFieldName)
        {
            bool result = false;
            //Valdiate if both field names are correct
            if (sourceItemIds != null && sourceItemIds.Any() == false)
            {
                Log.Information($"SourceIds are either empty or null.", nameof(Contains));
                return result;
            }
            //Valdiate if both field names are correct
            if (!contentItem.HasKey(targetFieldName))
            {
               Log.Warning($"Invaild targetFieldName {targetFieldName}. If field is not cascading Ignore this!", nameof(Contains));
                return result;
            }

            //read values from both fields

            var targetFieldItems = GetIdsList(contentItem.GetSharedField<string>(targetFieldName)).ToList();

            //Validate if any value is found return true
            result = sourceItemIds.Intersect(targetFieldItems).Any();
            return result;
        }
        public static string getCleanFieldKey(string fieldName)
        {
            return fieldName.Replace(" ", "_").ToLower();
        }
        public static string getCleanCultureFieldKey(string fieldName, string culture)
        {
            culture = CultureUtility.GetCultureName(culture);
            fieldName = getCleanFieldKey(fieldName);
            return $"{fieldName}_{culture}".ToLower();
        }
    }
}
