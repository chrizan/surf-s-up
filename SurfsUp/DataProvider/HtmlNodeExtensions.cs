using HtmlAgilityPack;

namespace SurfsUp.DataProvider
{
    public static class HtmlNodeExtensions
    {
        // SelectSingleNode returns null instead of throwing when the xpath doesn't
        // match, which turns page structure changes into unhelpful NullReferenceExceptions
        // further down the call stack. This fails at the point of lookup with a message
        // naming the field, so scraping breakage is diagnosable from the log alone.
        public static HtmlNode SelectRequiredNode(this HtmlNode node, string xpath, string fieldDescription)
        {
            return node.SelectSingleNode(xpath)
                ?? throw new InvalidOperationException($"Could not find '{fieldDescription}' element (xpath: {xpath}). The page structure may have changed.");
        }
    }
}
