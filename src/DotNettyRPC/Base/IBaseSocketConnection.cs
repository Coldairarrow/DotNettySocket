namespace Coldairarrow.DotNettySocket
{
    public interface IBaseSocketConnection : IClose
    {
        string ConnectionId { get; }
        string ConnectionName { get; set; }
    }
}
