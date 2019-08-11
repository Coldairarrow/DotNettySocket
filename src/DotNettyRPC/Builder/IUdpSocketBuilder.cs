using System;
using System.Net;

namespace Coldairarrow.DotNettySocket
{
    public interface IUdpSocketBuilder : IBuilder<IUdpSocketBuilder, IUdpSocket>
    {
        IUdpSocketBuilder OnStarted(Action<IUdpSocket> action);
        IUdpSocketBuilder OnRecieve(Action<IUdpSocket, EndPoint, byte[]> action);
        IUdpSocketBuilder OnSend(Action<IUdpSocket, EndPoint, byte[]> action);
        IUdpSocketBuilder OnClose(Action<IUdpSocket> action);
    }
}
