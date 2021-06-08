using Microsoft.Extensions.Configuration;
using Quartz;
using SurfsUp.DataProvider.Contracts;
using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.Messengers;
using SurfsUp.SurfsUp.SwellAssessment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurfsUp.SurfsUp.Jobs
{
    public class Job : IJob
    {
        private readonly IConfiguration _configuration;
        private readonly IMswDataProvider _mswDataProvider;
        private readonly IMessenger _messenger;
        private readonly IEvaluator _evaluator;

        public Job(IConfiguration configuration, IMswDataProvider mswDataProvider, IMessenger messenger, IEvaluator evaluator)
        {
            _mswDataProvider = mswDataProvider;
            _messenger = messenger;
            _configuration = configuration;
            _evaluator = evaluator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<Message> messages = new();
            await CheckFrenchSpots(messages);
            await CheckItalianSpots(messages);
            await CheckSpanishSpots(messages);
            await _messenger.SendMessage(messages);
        }

        private async Task CheckItalianSpots(List<Message> messages)
        {
            var spotsItaly = _configuration.GetSection("MswSpotsItaly").GetChildren().ToDictionary(s => s.Key, s => s.Value);
            foreach (var spot in spotsItaly)
            {
                string spotName = spot.Key;
                string spotUrl = spot.Value;
                SwellData data = await _mswDataProvider.GetSwellDataFromWeb(spotUrl);
                ISet<DayOfWeek> dates = _evaluator.Evaluate(data, Strategy.Italy);
                if (dates.Count != 0)
                {
                    messages.Add(new Message() { Dates = dates, SpotName = spotName, SpotUrl = spotUrl });
                }
            }
        }

        private async Task CheckFrenchSpots(List<Message> messages)
        {
            var spotsFrance = _configuration.GetSection("MswSpotsFrance").GetChildren().ToDictionary(s => s.Key, s => s.Value);
            foreach (var spot in spotsFrance)
            {
                string spotName = spot.Key;
                string spotUrl = spot.Value;
                SwellData data = await _mswDataProvider.GetSwellDataFromWeb(spotUrl);
                ISet<DayOfWeek> dates = _evaluator.Evaluate(data, Strategy.France);
                if (dates.Count != 0)
                {
                    messages.Add(new Message() { Dates = dates, SpotName = spotName, SpotUrl = spotUrl });
                }
            }
        }

        private async Task CheckSpanishSpots(List<Message> messages)
        {
            var spotsSpain = _configuration.GetSection("MswSpotsSpain").GetChildren().ToDictionary(s => s.Key, s => s.Value);
            foreach (var spot in spotsSpain)
            {
                string spotName = spot.Key;
                string spotUrl = spot.Value;
                SwellData data = await _mswDataProvider.GetSwellDataFromWeb(spotUrl);
                ISet<DayOfWeek> dates = _evaluator.Evaluate(data, Strategy.Spain);
                if (dates.Count != 0)
                {
                    messages.Add(new Message() { Dates = dates, SpotName = spotName, SpotUrl = spotUrl });
                }
            }
        }
    }
}
