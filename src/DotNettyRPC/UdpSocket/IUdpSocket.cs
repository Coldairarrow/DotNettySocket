using System.Net;
using System.Threading.Tasks;

namespace Coldairarrow.DotNettySocket
{
    public interface IUdpSocket
    {
        int Port { get; }
        Task Send(byte[] bytes, EndPoint point);
        Task Send(string msgStr, EndPoint point);
    }
}