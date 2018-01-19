using GameManager.Domain.Utils;
using System.Threading.Tasks;

namespace GameManager.Domain.Contracts.Helpers
{
    public interface IEmailHelper
    {
        void Send(Email email);
        Task SendAsync(Email email);
    }
}
