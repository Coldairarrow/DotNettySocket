namespace Coldairarrow.DotNettySocket
{
    /// <summary>
    /// Socket构建者工厂
    /// </summary>
    public class SocketBuilderFactory
    {
        /// <summary>
        /// 获取TcpSocket客户端构建者
        /// </summary>
        /// <param name="ip">服务器Ip</param>
        /// <param name="port">服务器端口</param>
        /// <returns></returns>
        public static ITcpSocketClientBuilder GetTcpSocketClientBuilder(string ip, int port)
        {
            return new TcpSocketClientBuilder(ip, port);
        }

        /// <summary>
        /// 获取TcpSocket服务端构建者
        /// </summary>
        /// <param name="port">监听端口</param>
        /// <returns></returns>
        public static ITcpSocketServerBuilder GetTcpSocketServerBuilder(int port)
        {
            return new TcpSocketServerBuilder(port);
        }

        /// <summary>
        /// 获取WebSocket服务端构建者
        /// </summary>
        /// <param name="port">监听端口</param>
        /// <param name="path">路径,默认为"/"</param>
        /// <returns></returns>
        public static IWebSocketServerBuilder GetWebSocketServerBuilder(int port, string path = "/")
        {
            return new WebSocketServerBuilder(port, path);
        }

        /// <summary>
        /// 获取WebSocket客户端构建者
        /// </summary>
        /// <param name="ip">服务器Ip</param>
        /// <param name="port">服务器端口</param>
        /// <param name="path">路径,默认为"/"</param>
        /// <returns></returns>
        public static IWebSocketClientBuilder GetWebSocketClientBuilder(string ip, int port, string path = "/")
        {
            return new WebSocketClientBuilder(ip, port, path);
        }

        /// <summary>
        /// 获取Udpocket构建者
        /// 注:UDP服务端与客户端一样
        /// </summary>
        /// <param name="port">监听端口,作为客户端时可不设置</param>
        /// <returns></returns>
        public static IUdpSocketBuilder GetUdpSocketBuilder(int port = 0)
        {
            return new UdpSocketBuilder(port);
        }
    }
}
