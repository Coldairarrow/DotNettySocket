namespace Coldairarrow.DotNettySocket
{
    /// <summary>
    /// TcpSocket服务端构建者
    /// </summary>
    public interface ITcpSocketServerBuilder :
        IGenericServerBuilder<ITcpSocketServerBuilder, ITcpSocketServer, ITcpSocketConnection, byte[]>,
        ICoderBuilder<ITcpSocketServerBuilder>
    {

    }
}
