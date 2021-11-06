using System;
using System.Collections.Generic;
using System.Text;

namespace SurfsUp.SurfsUp.Messengers
{
    public class HtmlMailBuilder : IHtmlMailBuilder
    {
        public string BuildHtmlMail(List<Message> messages)
        {
            var sb = new StringBuilder();
            
            sb.Append("<!DOCTYPE html>");
            sb.Append("<html>");
            sb.Append("<head>");
            sb.Append("<meta charset=\"utf-8\">");
            sb.Append("<title>Surf's Up!</title>");
            sb.Append("</head>");
            sb.Append("<body>");

            foreach (var message in messages)
            {
                sb.Append(GetHtmlRep(message));
            }

            sb.Append("</body>");
            return sb.ToString();
        }

        private static string GetHtmlRep(Message message)
        {
            return $"<p><a href={message.SpotUrl}>{message.SpotName}</a> is firing @ {GetDays(message.Dates)}!</p>";
        }

        private static string GetDays(IEnumerable<DayOfWeek> days)
        {
            var daysRep = new List<string>();
            foreach(var day in days)
            {
                if (IsWeekend(day))
                {
                    daysRep.Add($"<b>{day}</b>");
                }
                else
                {
                    daysRep.Add($"{day}");
                }
            }
            
            return string.Join(", ", daysRep);
        }

        private static bool IsWeekend(DayOfWeek dayOfWeek)
        {
            return dayOfWeek is DayOfWeek.Saturday || dayOfWeek is DayOfWeek.Sunday;
        }
    }
}