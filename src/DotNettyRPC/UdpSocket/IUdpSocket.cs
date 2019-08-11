using System.Net;
using System.Threading.Tasks;

namespace Coldairarrow.DotNettySocket
{
    /// <summary>
    /// UdpSocket
    /// </summary>
    public interface IUdpSocket
    {
        /// <summary>
        /// 监听端口
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        int Port { get; }

        /// <summary>
        /// 发送字节
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="point">目标地址</param>
        /// <returns></returns>
        Task Send(byte[] bytes, EndPoint point);

        /// <summary>
        /// 发送字符串,UTF-8编码
        /// </summary>
        /// <param name="msgStr">字符串</param>
        /// <param name="point">目标地址</param>
        /// <returns></returns>
        Task Send(string msgStr, EndPoint point);
    }
}