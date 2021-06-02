using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurfsUp.SurfsUp.Messengers
{
    public interface IMessenger
    {
        Task SendMessage(List<Message> messages); 
    }
}
