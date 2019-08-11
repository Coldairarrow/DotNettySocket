using System;
using System.Net;

namespace Coldairarrow.DotNettySocket
{
    /// <summary>
    /// UdpSocket构建者
    /// </summary>
    public interface IUdpSocketBuilder : IBuilder<IUdpSocketBuilder, IUdpSocket>
    {
        /// <summary>
        /// 启动事件
        /// </summary>
        /// <param name="action">处理启动事件</param>
        /// <returns></returns>
        IUdpSocketBuilder OnStarted(Action<IUdpSocket> action);

        /// <summary>
        /// 收到数据事件
        /// </summary>
        /// <param name="action">处理收到数据事件</param>
        /// <returns></returns>
        IUdpSocketBuilder OnRecieve(Action<IUdpSocket, EndPoint, byte[]> action);

        /// <summary>
        /// 发送数据事件
        /// </summary>
        /// <param name="action">处理发送数据事件</param>
        /// <returns></returns>
        IUdpSocketBuilder OnSend(Action<IUdpSocket, EndPoint, byte[]> action);

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="action">处理关闭事件</param>
        /// <returns></returns>
        IUdpSocketBuilder OnClose(Action<IUdpSocket> action);
    }
}
