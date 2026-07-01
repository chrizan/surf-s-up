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

        public MswSwellData GetMswSwellDataFromFile(string filePath)
        {
            HtmlDocument htmlDoc = new();
            htmlDoc.Load(filePath);
            long utcTimeZone = long.Parse(htmlDoc.DocumentNode.SelectSingleNode(XpathToTimezoneElement).GetAttributeValue("data-timezone", "0"));
            return GetMswSwellData(htmlDoc, utcTimeZone);
        }

        public async Task<MswSwellData> GetMswSwellDataFromWeb(string spotUrl)
        {
            HtmlWeb web = new();
            HtmlDocument htmlDoc = await web.LoadFromWebAsync(spotUrl);
            long utcTimeZone = int.Parse(htmlDoc.DocumentNode.SelectSingleNode(XpathToTimezoneElement).GetAttributeValue("data-timezone", "0"));
            return GetMswSwellData(htmlDoc, utcTimeZone);
        }

        private static MswSwellData GetMswSwellData(HtmlDocument htmlDoc, long utcTimeZone)
        {
            MswSwellData mswSwellData = new();

            int forecastDay = 1;
            IEnumerable<HtmlNode> dayNodes = htmlDoc.DocumentNode.Descendants("tr").Where(tr => tr.GetAttributeValue("data-forecast-day", "-1") == forecastDay.ToString());

            do
            {
                mswSwellData.Add(forecastDay, GetMswDailySwellData(dayNodes, utcTimeZone));
                forecastDay++;
                dayNodes = htmlDoc.DocumentNode.Descendants("tr").Where(tr => tr.GetAttributeValue("data-forecast-day", "-1") == forecastDay.ToString());

            } while (dayNodes.Any());

            return mswSwellData;
        }

        private static MswDailySwellData GetMswDailySwellData(IEnumerable<HtmlNode> dayNodes, long utcTimeZone)
        {
            MswDailySwellData dailySwellData = new();
            for (int dayNode = 1; dayNode <= dayNodes.Count(); dayNode++)
            {
                dailySwellData.Add((ForecastHour)dayNode, GetMswHourlySwellData(dayNodes.ElementAt(dayNode - 1), utcTimeZone));
            }
            return dailySwellData;
        }

        private static MswHourlySwellData GetMswHourlySwellData(HtmlNode dayNode, long utcTimeZone)
        {
            long unixTimeStamp = long.Parse(dayNode.GetAttributeValue("data-timestamp", "0"));
            MswHourlySwellData mswHourlySwellData = new()
            {
                Timestamp = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp + utcTimeZone).DateTime
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
