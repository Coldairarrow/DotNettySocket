namespace Coldairarrow.DotNettySocket
{
    public interface ITcpSocketClientBuilder :
        IGenericClientBuilder<ITcpSocketClientBuilder, ITcpSocketClient, byte[]>,
        ICoderBuilder<ITcpSocketClientBuilder>
    {

    }
}
