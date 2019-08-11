using Coldairarrow.DotNettySocket;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UdpSocket.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var theClient = await SocketBuilderFactory.GetUdpSocketBuilder()
                .OnClose(server =>
                {
                    Console.WriteLine($"客户端关闭");
                })
                .OnException(ex =>
                {
                    Console.WriteLine($"客户端异常:{ex.Message}");
                })
                .OnRecieve((server, point, bytes) =>
                {
                    Console.WriteLine($"客户端:收到来自[{point.ToString()}]数据:{Encoding.UTF8.GetString(bytes)}");
                })
                .OnSend((server, point, bytes) =>
                {
                    Console.WriteLine($"客户端发送数据:目标[{point.ToString()}]数据:{Encoding.UTF8.GetString(bytes)}");
                })
                .OnStarted(server =>
                {
                    Console.WriteLine($"客户端启动");
                }).BuildAsync();

            while (true)
            {
                await theClient.Send(Guid.NewGuid().ToString(), new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6003));
                await Task.Delay(1000);
            }
        }
    }
}
