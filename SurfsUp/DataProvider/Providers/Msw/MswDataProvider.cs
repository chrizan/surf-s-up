using HtmlAgilityPack;
using SurfsUp.DataProvider.Contracts;

namespace SurfsUp.DataProvider.Providers.Msw
{
    /// <summary>
    /// Magic seaweed screen scraping implementation of <see cref="IMswDataProvider"/> 
    /// </summary>
    public class MswDataProvider : IMswDataProvider
    {
        private const string XpathToTimezoneElement = "/html/body/div[1]/div/div[2]/div/div[2]/div[2]/div[2]/div/div/div[1]/div/header/h3/div[2]/span";
        private long _utcTimeZone = 0;

        public MswSwellData GetMswSwellDataFromFile(string filePath)
        {
            HtmlDocument htmlDoc = new();
            htmlDoc.Load(filePath);
            _utcTimeZone = long.Parse(htmlDoc.DocumentNode.SelectSingleNode(XpathToTimezoneElement).GetAttributeValue("data-timezone", "0"));
            return GetMswSwellData(htmlDoc);
        }

        public async Task<MswSwellData> GetMswSwellDataFromWeb(string spotUrl)
        {
            HtmlWeb web = new();
            HtmlDocument htmlDoc = await web.LoadFromWebAsync(spotUrl);
            _utcTimeZone = int.Parse(htmlDoc.DocumentNode.SelectSingleNode(XpathToTimezoneElement).GetAttributeValue("data-timezone", "0"));
            return GetMswSwellData(htmlDoc);
        }

        private MswSwellData GetMswSwellData(HtmlDocument htmlDoc)
        {
            MswSwellData mswSwellData = new();

            int forecastDay = 1;
            IEnumerable<HtmlNode> dayNodes = htmlDoc.DocumentNode.Descendants("tr").Where(tr => tr.GetAttributeValue("data-forecast-day", "-1") == forecastDay.ToString());

            do
            {
                mswSwellData.Add(forecastDay, GetMswDailySwellData(dayNodes));
                forecastDay++;
                dayNodes = htmlDoc.DocumentNode.Descendants("tr").Where(tr => tr.GetAttributeValue("data-forecast-day", "-1") == forecastDay.ToString());

            } while (dayNodes.Any());

            return mswSwellData;
        }

        private MswDailySwellData GetMswDailySwellData(IEnumerable<HtmlNode> dayNodes)
        {
            MswDailySwellData dailySwellData = new();
            for (int dayNode = 1; dayNode <= dayNodes.Count(); dayNode++)
            {
                dailySwellData.Add((ForecastHour)dayNode, GetMswHourlySwellData(dayNodes.ElementAt(dayNode - 1)));
            }
            return dailySwellData;
        }

        private MswHourlySwellData GetMswHourlySwellData(HtmlNode dayNode)
        {
            long unixTimeStamp = long.Parse(dayNode.GetAttributeValue("data-timestamp", "0"));
            MswHourlySwellData mswHourlySwellData = new()
            {
                Timestamp = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp + _utcTimeZone).DateTime
            };

            var listNodes = dayNode.Descendants("li");

            foreach (var listNode in listNodes)
            {
                string starClass = listNode.GetAttributeValue("class", string.Empty);
                switch (starClass)
                {
                    case "active ":
                        mswHourlySwellData.FullStars += 1;
                        break;
                    case "inactive ":
                        mswHourlySwellData.BlurredStars += 1;
                        break;
                    case "placeholder":
                        mswHourlySwellData.EmptyStars += 1;
                        break;
                    default:
                        break;
                }
            }
            return mswHourlySwellData;
        }
    }
}
