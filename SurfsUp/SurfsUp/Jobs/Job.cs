using Quartz;
using SurfsUp.DataProvider.Contract;
using SurfsUp.DataProvider.Models;
using System;
using System.Threading.Tasks;

namespace SurfsUp.SurfsUp.Jobs
{
    public class Job : IJob
    {
        private readonly IDataProvider _dataProvider;

        public Job(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            SwellData data = await _dataProvider.GetSwellDataFromWeb("https://de.magicseaweed.com/Levanto-Surf-Report/3571/");
            JobKey key = context.JobDetail.Key;
            await Console.Error.WriteLineAsync($"Instance {key} of Job executed. Nonsense {data[1]}");
        }
    }
}
