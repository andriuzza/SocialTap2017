namespace SocialTap.DataAccess.ExtensionMethod
{
    public static class StringContains
    {
        public static bool ContainsStrOrNull(this string str, string search)
        {
            if (search == null)
            {
                return true;
            }
            if (str == null)
            {
                return false;
            }

            var lowerStr = search.ToLower();
            var lowerContainer = str.ToLower();

            return lowerContainer.Contains(lowerStr);
        }
    }
}