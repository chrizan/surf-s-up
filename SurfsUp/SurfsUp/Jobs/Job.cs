using Microsoft.Extensions.Configuration;
using Quartz;
using SurfsUp.DataProvider.Contract;
using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.Messengers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SurfsUp.SurfsUp.Jobs
{
    public class Job : IJob
    {
        private readonly IDataProvider _dataProvider;
        private readonly IMessenger _messenger;
        private readonly IConfiguration _configuration;

        public Job(IConfiguration configuration, IDataProvider dataProvider, IMessenger messenger)
        {
            _dataProvider = dataProvider;
            _messenger = messenger;
            _configuration = configuration;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var spots = _configuration.GetSection("MswSpots").GetChildren().ToDictionary(s => s.Key, s => s.Value);
            foreach (var spot in spots)
            {
                string spotName = spot.Key;
                string spotUrl = spot.Value;
            }
            await _messenger.SendMessage();
            SwellData data = await _dataProvider.GetSwellDataFromWeb("https://de.magicseaweed.com/Levanto-Surf-Report/3571/");
            JobKey key = context.JobDetail.Key;
            await Console.Error.WriteLineAsync($"Instance {key} of Job executed. Nonsense {data[1]}");
        }
    }
}
