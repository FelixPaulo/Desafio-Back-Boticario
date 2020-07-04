using Cashbot.Application.Events;
using System.Threading.Tasks;

namespace Cashbot.Application.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
    }
}
