using System;
using System.Collections.Generic;
using System.Text;

namespace SurfsUp.SurfsUp.Messengers
{
    public class Message
    {
        public ISet<DateTime> Dates { get; set; }

        public string SpotName { get; set; }

        public string SpotUrl { get; set; }

        public override string ToString()
        {
            return $"{SpotName} is firing @ {GetDates()}! {Environment.NewLine} Checkout {SpotUrl} ";
        }

        private string GetDates()
        {
            StringBuilder dates = new();
            foreach (var date in Dates)
            {
                dates.Append(date.ToString("dddd"));
                dates.Append(", ");
            }
            dates.Length--;
            dates.Length--;
            return dates.ToString();
        }
    }
}
