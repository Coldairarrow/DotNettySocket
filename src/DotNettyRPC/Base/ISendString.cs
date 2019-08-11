using System.Threading.Tasks;

namespace Coldairarrow.DotNettySocket
{
    public interface ISendString
    {
        Task Send(string msgStr);
    }
}