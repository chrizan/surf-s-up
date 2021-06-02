using HtmlAgilityPack;
using SurfsUp.DataProvider.Contract;
using SurfsUp.DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurfsUp.DataProvider.Data
{
    /// <summary>
    /// Magic seaweed screen scraping implementation of IDataProvider 
    /// </summary>
    public class MswDataProvider : IDataProvider
    {
        public SwellData GetSwellDataFromFile(string filePath)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.Load(filePath);
            return GetSwellData(htmlDoc);
        }

        public async Task<SwellData> GetSwellDataFromWeb(string spotUrl)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = await web.LoadFromWebAsync(spotUrl);
            return GetSwellData(htmlDoc);
        }

        private SwellData GetSwellData(HtmlDocument htmlDoc)
        {
            SwellData swellData = new SwellData();

            int forecastDay = 1;
            IEnumerable<HtmlNode> dayNodes = htmlDoc.DocumentNode.Descendants("tr").Where(tr => tr.GetAttributeValue("data-forecast-day", "-1") == forecastDay.ToString());

            do
            {
                swellData.Add(forecastDay, GetDailySwellData(dayNodes));
                forecastDay++;
                dayNodes = htmlDoc.DocumentNode.Descendants("tr").Where(tr => tr.GetAttributeValue("data-forecast-day", "-1") == forecastDay.ToString());

            } while (dayNodes.Any());

            return swellData;
        }

        private DailySwellData GetDailySwellData(IEnumerable<HtmlNode> dayNodes)
        {
            string unixTimeStamp = dayNodes.FirstOrDefault()?.GetAttributeValue("data-timestamp", "0");
            DailySwellData dailySwellData = new DailySwellData()
            {
                Date = DateTimeOffset.FromUnixTimeSeconds(long.Parse(unixTimeStamp)).DateTime.ToLocalTime()
            };

            for (int dayNode = 1; dayNode <= dayNodes.Count(); dayNode++)
            {
                dailySwellData.Add((ForecastHour)dayNode, GetHourlySwellData(dayNodes.ElementAt(dayNode - 1)));
            }
            return dailySwellData;
        }

        private HourlySwellData GetHourlySwellData(HtmlNode dayNode)
        {
            HourlySwellData hourlySwellData = new HourlySwellData();

            var listNodes = dayNode.Descendants("li");

            foreach (var listNode in listNodes)
            {
                string starClass = listNode.GetAttributeValue("class", string.Empty);
                switch (starClass)
                {
                    case "active ":
                        hourlySwellData.FullStars += 1;
                        break;
                    case "inactive ":
                        hourlySwellData.BlurredStars += 1;
                        break;
                    case "placeholder":
                        hourlySwellData.EmptyStars += 1;
                        break;
                    default:
                        break;
                }
            }
            return hourlySwellData;
        }
    }
}
