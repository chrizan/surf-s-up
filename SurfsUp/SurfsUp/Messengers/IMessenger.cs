using System.Threading.Tasks;

namespace SurfsUp.SurfsUp.Messengers
{
    public interface IMessenger
    {
        Task SendMessage(); 
    }
}
