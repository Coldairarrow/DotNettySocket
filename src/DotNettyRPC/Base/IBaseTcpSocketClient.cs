namespace Coldairarrow.DotNettySocket
{
    /// <summary>
    /// TcpSocket客户端
    /// </summary>
    /// <seealso cref="Coldairarrow.DotNettySocket.IClose" />
    public interface IBaseTcpSocketClient : IClose
    {
        /// <summary>
        /// 服务器Ip
        /// </summary>
        /// <value>
        /// The ip.
        /// </value>
        string Ip { get; }

        /// <summary>
        /// 服务器端口
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        int Port { get; }
    }
}