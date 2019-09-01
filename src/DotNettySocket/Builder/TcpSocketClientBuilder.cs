using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Threading.Tasks;

namespace Coldairarrow.DotNettySocket
{
    class TcpSocketClientBuilder : BaseGenericClientBuilder<ITcpSocketClientBuilder, ITcpSocketClient, byte[]>, ITcpSocketClientBuilder
    {
        public TcpSocketClientBuilder(string ip, int port)
            : base(ip, port)
        {

        }
        protected Action<IChannelPipeline> _setEncoder { get; set; }

        public ITcpSocketClientBuilder SetLengthFieldDecoder(int maxFrameLength, int lengthFieldOffset, int lengthFieldLength, int lengthAdjustment, int initialBytesToStrip, ByteOrder byteOrder = ByteOrder.BigEndian)
        {
            _setEncoder += x => x.AddLast(new LengthFieldBasedFrameDecoder(byteOrder, maxFrameLength, lengthFieldOffset, lengthFieldLength, lengthAdjustment, initialBytesToStrip, true));

            return this;
        }

        public ITcpSocketClientBuilder SetLengthFieldEncoder(int lengthFieldLength)
        {
            _setEncoder += x => x.AddLast(new LengthFieldPrepender(lengthFieldLength));

            return this;
        }

        public async override Task<ITcpSocketClient> BuildAsync()
        {
            TcpSocketClient tcpClient = new TcpSocketClient(_ip, _port, _event);

            var clientChannel = await new Bootstrap()
                .Group(new MultithreadEventLoopGroup())
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    _setEncoder?.Invoke(pipeline);
                    pipeline.AddLast(new CommonChannelHandler(tcpClient));
                })).ConnectAsync($"{_ip}:{_port}".ToIPEndPoint());

            tcpClient.SetChannel(clientChannel);

            return await Task.FromResult(tcpClient);
        }
    }
}
