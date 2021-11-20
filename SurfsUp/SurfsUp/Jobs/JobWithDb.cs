using Database.Contracts;
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
    public class JobWithDb : IJob
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabaseService _databaseService;
        private readonly IMswDataProvider _mswDataProvider;
        private readonly IBafuDataProvider _bafuDataProvider;
        private readonly IMessenger _messenger;
        private readonly IMswEvaluator _mswEvaluator;
        private readonly IBafuEvaluator _bafuEvaluator;

        public JobWithDb(IConfiguration configuration, IDatabaseService databaseService, IMswDataProvider mswDataProvider, IBafuDataProvider bafuDataProvider, IMessenger messenger, IMswEvaluator mswEvaluator, IBafuEvaluator bafuEvaluator)
        {
            _mswDataProvider = mswDataProvider;
            _databaseService = databaseService;
            _bafuDataProvider = bafuDataProvider;
            _messenger = messenger;
            _configuration = configuration;
            _mswEvaluator = mswEvaluator;
            _bafuEvaluator = bafuEvaluator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<Message> messages = new();
            messages.AddRange(await CheckMswSpots());
            messages.AddRange(await CheckBafuSpots());
            await _messenger.SendMessage(messages);
        }

        private async Task<List<Message>> CheckMswSpots()
        {
            var messages = new List<Message>();
            var mswSpots = await _databaseService.GetAllMswSurfSpotsAsync();
            foreach (var mswSpot in mswSpots)
            {
                string spotName = "";//mswSpot.Key;
                string spotUrl = "";//mswSpot.Value;
                SwellData data = await _mswDataProvider.GetSwellDataFromWeb(spotUrl);
                ISet<DayOfWeek> dates = _mswEvaluator.EvaluateMswData(data, MswStrategy.France);
                if (dates.Count != 0)
                {
                    messages.Add(new Message() { Dates = dates, SpotName = spotName, SpotUrl = spotUrl });
                }
            }
            return messages;
        }

        private async Task<List<Message>> CheckBafuSpots()
        {
            var messages = new List<Message>();
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
            return messages;
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
