namespace SurfsUp.WebApp.Messengers
{
    public interface IMessenger
    {
        Task SendMessage(List<Message> messages); 
    }
}
