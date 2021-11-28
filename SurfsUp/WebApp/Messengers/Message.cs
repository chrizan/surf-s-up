namespace SurfsUp.WebApp.Messengers
{
    public class Message
    {
        public ISet<DayOfWeek> Dates { get; set; }

        public string SpotName { get; set; }

        public string SpotUrl { get; set; }

        public string GetRepresentation()
        {
            return $"{SpotName} is firing @ {string.Join(", ", Dates)}! {Environment.NewLine}Checkout {SpotUrl} ";
        }
    }
}
