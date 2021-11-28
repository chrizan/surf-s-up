namespace SurfsUp.WebApp.Messengers
{
    public interface IHtmlMailBuilder
    {
        public string BuildHtmlMail(List<Message> messages);
    }
}