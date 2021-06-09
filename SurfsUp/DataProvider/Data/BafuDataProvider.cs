using HtmlAgilityPack;
using SurfsUp.DataProvider.Contracts;
using SurfsUp.DataProvider.Models;
using System.Threading.Tasks;

namespace SurfsUp.DataProvider.Data
{
    public class BafuDataProvider : IBafuDataProvider
    {
        private const string XpathOutflow = "//body/div/div/div[3]/table/tbody/tr[1]/td[1]";
        private const string XPathTemperature = "//body/div/div/div[3]/table/tbody/tr[1]/td[3]";

        public async Task<BafuData> GetOutflowData(string bafuUrl)
        {
            HtmlWeb web = new();
            HtmlDocument htmlDoc = await web.LoadFromWebAsync(bafuUrl);
            return GetBafuData(htmlDoc);
        }

        private static BafuData GetBafuData(HtmlDocument htmlDoc)
        {
            BafuData bafuData = new()
            {
                Outflow = double.Parse(htmlDoc.DocumentNode.SelectSingleNode(XpathOutflow).InnerText),
                DegreeCelsius = double.Parse(htmlDoc.DocumentNode.SelectSingleNode(XPathTemperature).InnerText)
            };
            return bafuData;
        }
    }
}
