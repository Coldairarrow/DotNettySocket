using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.DotNettySocket
{
    class TcpSocketConnection : BaseTcpSocketConnection<ITcpSocketServer, ITcpSocketConnection, byte[]>, ITcpSocketConnection
    {
        #region 构造函数

        public TcpSocketConnection(ITcpSocketServer server, IChannel channel, TcpSocketServerEvent<ITcpSocketServer, ITcpSocketConnection, byte[]> serverEvent)
            : base(server, channel, serverEvent)
        {

        }

        #endregion

        #region 私有成员

        #endregion

        #region 外部接口

        public async Task Send(byte[] bytes)
        {
            try
            {
                await _channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(bytes));
                await Task.Run(() =>
                {
                    _serverEvent.OnSend?.Invoke(_server, this, bytes);
                });
            }
            catch (Exception ex)
            {
                _serverEvent.OnException?.Invoke(ex);
            }
        }

        public async Task Send(string msgStr)
        {
            await Send(Encoding.UTF8.GetBytes(msgStr));
        }

        #endregion
    }
}
