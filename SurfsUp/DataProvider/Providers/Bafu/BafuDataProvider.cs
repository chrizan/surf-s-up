using HtmlAgilityPack;
using SurfsUp.DataProvider.Contracts;

namespace SurfsUp.DataProvider.Providers.Bafu
{
    /// <summary>
    /// Bafu screen scraping implementation of <see cref="IBafuDataProvider"/> 
    /// </summary>
    public class BafuDataProvider : IBafuDataProvider
    {
        private const string XpathOutflowCurrent = "//body/div/div/div[3]/table/tbody/tr[1]/td[1]";
        private const string XpathOutflowMax24hours = "//body/div/div/div[3]/table/tbody/tr[3]/td[1]";
        private const string XPathTemperature = "//body/div/div/div[3]/table/tbody/tr[1]/td[3]";

        public async Task<BafuData> GetOutflowData(string spotUrl)
        {
            HtmlWeb web = new();
            HtmlDocument htmlDoc = await web.LoadFromWebAsync(spotUrl);
            return GetBafuData(htmlDoc);
        }

        private static BafuData GetBafuData(HtmlDocument htmlDoc)
        {
            BafuData bafuData = new()
            {
                OutflowCurrent = double.Parse(htmlDoc.DocumentNode.SelectSingleNode(XpathOutflowCurrent).InnerText),
                OutflowMax24hours = double.Parse(htmlDoc.DocumentNode.SelectSingleNode(XpathOutflowMax24hours).InnerText),
                DegreeCelsius = double.Parse(htmlDoc.DocumentNode.SelectSingleNode(XPathTemperature).InnerText),
                Dates = new HashSet<DayOfWeek>() { DateTime.Today.DayOfWeek }
            };
            return bafuData;
        }
    }
}
