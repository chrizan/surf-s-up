using SurfsUp.DataProvider.Data;
using SurfsUp.DataProvider.Models;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://www.hydrodaten.admin.ch/de/2018.html";

            var provider = new BafuDataProvider();
            BafuData bafuData = provider.GetOutflowData(url).Result;

            Console.WriteLine($"Current Outflow is {bafuData.Outflow}m^3/s @ a temperature of {bafuData.DegreeCelsius}°");
        }
    }
}
