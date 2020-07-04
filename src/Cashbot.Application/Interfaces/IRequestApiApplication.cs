using System.Threading.Tasks;

namespace Cashbot.Application.Interfaces
{
    public interface IRequestApiApplication
    {
        Task<object> GetApiClient();
    }
}
