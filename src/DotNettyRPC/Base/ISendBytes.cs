using System.Threading.Tasks;

namespace Coldairarrow.DotNettySocket
{
    public interface ISendBytes
    {
        Task Send(byte[] bytes);
    }
}