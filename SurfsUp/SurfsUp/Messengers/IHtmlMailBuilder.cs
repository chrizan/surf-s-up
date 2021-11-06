using System.Collections.Generic;

namespace SurfsUp.SurfsUp.Messengers
{
    public interface IHtmlMailBuilder
    {
        public string BuildHtmlMail(List<Message> messages);
    }
}