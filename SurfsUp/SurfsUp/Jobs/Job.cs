using Microsoft.Extensions.Configuration;
using Quartz;
using SurfsUp.DataProvider.Contract;
using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.Messengers;
using SurfsUp.SurfsUp.SwellAssessment;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurfsUp.SurfsUp.Jobs
{
    public class Job : IJob
    {
        private readonly IConfiguration _configuration;
        private readonly IDataProvider _dataProvider;
        private readonly IMessenger _messenger;
        private readonly IEvaluator _evaluator;

        public Job(IConfiguration configuration, IDataProvider dataProvider, IMessenger messenger, IEvaluator evaluator)
        {
            _dataProvider = dataProvider;
            _messenger = messenger;
            _configuration = configuration;
            _evaluator = evaluator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await CheckItalianSpots();
            await CheckFrenchSpots();
        }

        private async Task CheckItalianSpots()
        {
            var spotsItaly = _configuration.GetSection("MswSpotsItaly").GetChildren().ToDictionary(s => s.Key, s => s.Value);
            foreach (var spot in spotsItaly)
            {
                string spotName = spot.Key;
                string spotUrl = spot.Value;
                SwellData data = await _dataProvider.GetSwellDataFromWeb(spotUrl);
                ISet<string> dates = _evaluator.Evaluate(data, Strategy.Italy);
                if (dates.Count != 0)
                {
                    await _messenger.SendMessage(spotName, spotUrl, dates);
                }
            }
        }

        private async Task CheckFrenchSpots()
        {
            var spotsFrance = _configuration.GetSection("MswSpotsFrance").GetChildren().ToDictionary(s => s.Key, s => s.Value);
            foreach (var spot in spotsFrance)
            {
                string spotName = spot.Key;
                string spotUrl = spot.Value;
                SwellData data = await _dataProvider.GetSwellDataFromWeb(spotUrl);
                ISet<string> dates = _evaluator.Evaluate(data, Strategy.France);
                if (dates.Count != 0)
                {
                    await _messenger.SendMessage(spotName, spotUrl, dates);
                }
            }
        }
    }
}
