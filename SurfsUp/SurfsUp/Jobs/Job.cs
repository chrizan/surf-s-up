using Database.Contracts;
using Quartz;
using SurfsUp.DataProvider.Contracts;
using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.Messengers;
using SurfsUp.SurfsUp.SwellAssessment.Bafu;
using SurfsUp.SurfsUp.SwellAssessment.Msw;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurfsUp.SurfsUp.Jobs
{
    public class Job : IJob
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMswDataProvider _mswDataProvider;
        private readonly IBafuDataProvider _bafuDataProvider;
        private readonly IMessenger _messenger;
        private readonly IMswEvaluator _mswEvaluator;
        private readonly IBafuEvaluator _bafuEvaluator;

        public Job(IDatabaseService databaseService, 
            IMswDataProvider mswDataProvider,
            IBafuDataProvider bafuDataProvider,
            IMessenger messenger,
            IMswEvaluator mswEvaluator,
            IBafuEvaluator bafuEvaluator)
        {
            _mswDataProvider = mswDataProvider;
            _databaseService = databaseService;
            _bafuDataProvider = bafuDataProvider;
            _messenger = messenger;
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
                SwellData swellData = await _mswDataProvider.GetSwellDataFromWeb(mswSpot.Url);
                ISet<DayOfWeek> dates = _mswEvaluator.EvaluateMswData(swellData, mswSpot);
                if (dates.Count != 0)
                {
                    messages.Add(new Message() { Dates = dates, SpotName = mswSpot.Name, SpotUrl = mswSpot.Url });
                }
            }
            return messages;
        }

        private async Task<List<Message>> CheckBafuSpots()
        {
            var messages = new List<Message>();
            var bafuSpots = await _databaseService.GetAllBafuSurfSpotsAsync();
            foreach (var bafuSpot in bafuSpots)
            {
                BafuData bafuData = await _bafuDataProvider.GetOutflowData(bafuSpot.Url);
                if (_bafuEvaluator.IsFiring(bafuData, bafuSpot))
                {
                    messages.Add(new Message() { Dates = bafuData.Dates, SpotName = bafuSpot.Name, SpotUrl = bafuSpot.Url });
                }
            }
            return messages;
        }
    }
}
