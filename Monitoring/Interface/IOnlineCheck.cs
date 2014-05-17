using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace lafe.ShutdownService.Monitoring.Interface
{
    public interface IOnlineCheck
    {
        string Address { get; }

        bool IsOnline();
    }
}