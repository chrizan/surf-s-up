using Quartz;
using SurfsUp.DataProvider.Contract;
using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.Messengers;
using System;
using System.Threading.Tasks;

namespace SurfsUp.SurfsUp.Jobs
{
    public class Job : IJob
    {
        private readonly IDataProvider _dataProvider;
        private readonly IMessenger _messenger;

        public Job(IDataProvider dataProvider, IMessenger messenger)
        {
            _dataProvider = dataProvider;
            _messenger = messenger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _messenger.SendMessage();
            SwellData data = await _dataProvider.GetSwellDataFromWeb("https://de.magicseaweed.com/Levanto-Surf-Report/3571/");
            JobKey key = context.JobDetail.Key;
            await Console.Error.WriteLineAsync($"Instance {key} of Job executed. Nonsense {data[1]}");
        }
    }
}
