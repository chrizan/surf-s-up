using Microsoft.Extensions.Configuration;
using Quartz;
using SurfsUp.DataProvider.Contracts;
using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.Messengers;
using SurfsUp.SurfsUp.SwellAssessment.Bafu;
using SurfsUp.SurfsUp.SwellAssessment.Msw;
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
        private readonly IBafuDataProvider _bafuDataProvider;
        private readonly IMessenger _messenger;
        private readonly IMswEvaluator _mswEvaluator;
        private readonly IBafuEvaluator _bafuEvaluator;

        public Job(IConfiguration configuration, IMswDataProvider mswDataProvider, IBafuDataProvider bafuDataProvider, IMessenger messenger, IMswEvaluator mswEvaluator, IBafuEvaluator bafuEvaluator)
        {
            _mswDataProvider = mswDataProvider;
            _bafuDataProvider = bafuDataProvider;
            _messenger = messenger;
            _configuration = configuration;
            _mswEvaluator = mswEvaluator;
            _bafuEvaluator = bafuEvaluator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<Message> messages = new();
            await CheckFrenchSpots(messages);
            await CheckItalianSpots(messages);
            await CheckSpanishSpots(messages);
            await CheckSwissSpots(messages);
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
                ISet<DayOfWeek> dates = _mswEvaluator.EvaluateMswData(data, MswStrategy.Italy);
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
                ISet<DayOfWeek> dates = _mswEvaluator.EvaluateMswData(data, MswStrategy.France);
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
                ISet<DayOfWeek> dates = _mswEvaluator.EvaluateMswData(data, MswStrategy.Spain);
                if (dates.Count != 0)
                {
                    messages.Add(new Message() { Dates = dates, SpotName = spotName, SpotUrl = spotUrl });
                }
            }
        }

        private async Task CheckSwissSpots(List<Message> messages)
        {
            var spotsSwitzerland = _configuration.GetSection("BafuSpotsSwitzerland").GetChildren().ToDictionary(s => s.Key, s => s.Value);
            foreach (var spot in spotsSwitzerland)
            {
                string spotName = spot.Key;
                string spotUrl = spot.Value;
                BafuData data = await _bafuDataProvider.GetOutflowData(spotUrl);
                bool? isFiring = _bafuEvaluator.IsFiring(data, Map2Enum(spotName));
                if (isFiring.GetValueOrDefault())
                {
                    messages.Add(new Message() { Dates = data.Dates, SpotName = spotName, SpotUrl = spotUrl });
                }
            }
        }

        private static BafuStrategy Map2Enum(string spotName)
        {
            return spotName switch
            {
                "Reuss" => BafuStrategy.Reuss,
                "Birs" => BafuStrategy.Birs,
                "Thur" => BafuStrategy.Thur,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
