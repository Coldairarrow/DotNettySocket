namespace Coldairarrow.DotNettySocket
{
    public interface ITcpSocketServerBuilder :
        IGenericServerBuilder<ITcpSocketServerBuilder, ITcpSocketServer, ITcpSocketConnection, byte[]>,
        ICoderBuilder<ITcpSocketServerBuilder>
    {

    }
}
