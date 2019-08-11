using Coldairarrow.DotNettySocket;
using System;
using System.Text;
using System.Threading.Tasks;

namespace TcpSocket.Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var theServer = await SocketBuilderFactory.GetTcpSocketServerBuilder(6001)
                .SetLengthFieldEncoder(2)
                .SetLengthFieldDecoder(ushort.MaxValue, 0, 2, 0, 2)
                .OnConnectionClose((server, connection) =>
                {
                    Console.WriteLine($"连接关闭,连接名[{connection.ConnectionName}],当前连接数:{server.GetConnectionCount()}");
                })
                .OnException(ex =>
                {
                    Console.WriteLine($"服务端异常:{ex.Message}");
                })
                .OnNewConnection((server, connection) =>
                {
                    connection.ConnectionName = $"名字{connection.ConnectionId}";
                    Console.WriteLine($"新的连接:{connection.ConnectionName},当前连接数:{server.GetConnectionCount()}");
                })
                .OnRecieve((server, connection, bytes) =>
                {
                    Console.WriteLine($"服务端:数据{Encoding.UTF8.GetString(bytes)}");
                    connection.Send(bytes);
                })
                .OnSend((server, connection, bytes) =>
                {
                    Console.WriteLine($"向连接名[{connection.ConnectionName}]发送数据:{Encoding.UTF8.GetString(bytes)}");
                })
                .OnServerStarted(server =>
                {
                    Console.WriteLine($"服务启动");
                }).BuildAsync();

            Console.ReadLine();
        }
    }
}
