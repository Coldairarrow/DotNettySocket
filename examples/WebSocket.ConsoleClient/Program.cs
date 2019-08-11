using Coldairarrow.DotNettySocket;
using System;
using System.Threading.Tasks;

namespace WebSocket.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var theClient = await SocketBuilderFactory.GetWebSocketClientBuilder("127.0.0.1", 6002)
                .OnClientStarted(client =>
                {
                    Console.WriteLine($"客户端启动");
                })
                .OnClientClose(client =>
                {
                    Console.WriteLine($"客户端关闭");
                })
                .OnException(ex =>
                {
                    Console.WriteLine($"异常:{ex.Message}");
                })
                .OnRecieve((client, msg) =>
                {
                    Console.WriteLine($"客户端:收到数据:{msg}");
                })
                .OnSend((client, msg) =>
                {
                    Console.WriteLine($"客户端:发送数据:{msg}");
                })
                .BuildAsync();

            while (true)
            {
                await theClient.Send(Guid.NewGuid().ToString());

                await Task.Delay(1000);
            }
        }
    }
}