using System.Runtime.Serialization;

namespace SurfsUp.DataProvider.Providers.Msw
{
    [Serializable]
    public sealed class MswSwellData : Dictionary<int, MswDailySwellData>
    {
        public MswSwellData() { }

        private MswSwellData(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
