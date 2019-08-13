using System.Net;

namespace Coldairarrow.DotNettySocket
{
    /// <summary>
    /// SocketConnection基接口
    /// </summary>
    /// <seealso cref="Coldairarrow.DotNettySocket.IClose" />
    public interface IBaseSocketConnection : IClose
    {
        /// <summary>
        /// 连接Id,不可更改
        /// </summary>
        /// <value>
        /// The connection identifier.
        /// </value>
        string ConnectionId { get; }

        /// <summary>
        /// 连接名,可更改
        /// </summary>
        /// <value>
        /// The name of the connection.
        /// </value>
        string ConnectionName { get; set; }

        /// <summary>
        /// 客户端地址
        /// </summary>
        /// <value>
        /// The client address.
        /// </value>
        IPEndPoint ClientAddress { get; }
    }
}
