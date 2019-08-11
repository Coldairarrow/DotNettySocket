namespace Coldairarrow.DotNettySocket
{
    public interface IBaseTcpSocketClient : IClose
    {
        string Ip { get; }
        int Port { get; }
    }
}