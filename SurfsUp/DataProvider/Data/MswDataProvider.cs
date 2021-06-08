﻿using HtmlAgilityPack;
using SurfsUp.DataProvider.Contracts;
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
    public class MswDataProvider : IMswDataProvider
    {
        private const string _xPathToTimezoneElement = "/html/body/div[1]/div/div[2]/div/div[2]/div[2]/div[2]/div/div/div[1]/div/header/h3/div[2]/span";
        private long _utcTimeZone = 0;

        public SwellData GetSwellDataFromFile(string filePath)
        {
            HtmlDocument htmlDoc = new();
            htmlDoc.Load(filePath);
            _utcTimeZone = long.Parse(htmlDoc.DocumentNode.SelectSingleNode(_xPathToTimezoneElement).GetAttributeValue("data-timezone", "0"));
            return GetSwellData(htmlDoc);
        }

        public async Task<SwellData> GetSwellDataFromWeb(string spotUrl)
        {
            HtmlWeb web = new();
            HtmlDocument htmlDoc = await web.LoadFromWebAsync(spotUrl);
            _utcTimeZone = int.Parse(htmlDoc.DocumentNode.SelectSingleNode(_xPathToTimezoneElement).GetAttributeValue("data-timezone", "0"));
            return GetSwellData(htmlDoc);
        }

        private SwellData GetSwellData(HtmlDocument htmlDoc)
        {
            SwellData swellData = new();

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
            DailySwellData dailySwellData = new();
            for (int dayNode = 1; dayNode <= dayNodes.Count(); dayNode++)
            {
                dailySwellData.Add((ForecastHour)dayNode, GetHourlySwellData(dayNodes.ElementAt(dayNode - 1)));
            }
            return dailySwellData;
        }

        private HourlySwellData GetHourlySwellData(HtmlNode dayNode)
        {
            long unixTimeStamp = long.Parse(dayNode.GetAttributeValue("data-timestamp", "0"));
            HourlySwellData hourlySwellData = new() 
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
