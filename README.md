# 目录
- [简介](#简介)
- [产生背景](#产生背景)
- [使用方式](#使用方式)
	- [TcpSocket](#TcpSocket)
	- [WebSocket](#WebSocket)
	- [UdpSocket](#UdpSocket)
- [结尾](#结尾)

# 简介
DotNettySocket是一个.NET跨平台Socket框架（支持.NET4.5+及.NET Standard2.0+），同时支持TcpSocket、WebSocket和UdpSocket，其基于微软强大的DotNetty框架，力求为Socket通讯提供**简单**、**高效**、**优雅**的操作方式。

安装方式：Nuget安装**DotNettySocket**即可
项目地址：https://github.com/Coldairarrow/DotNettySocket

# 产生背景
两年前最开始接触物联网的时候，需要用到Tcp及Udp通讯，为了方便使用，将原始的Socket进行了简单的封装，基本满足了需求，并将框架开源。但是由于精力及实力有限，没有进一步优化原框架。后来发现了强大的DotNetty框架，DotNetty是微软Azure团队开源基于Java Netty框架的移植版，其性能优异、维护团队强大，许多.NET强大的框架都使用它。DotNetty功能强大，但是用起来还是不够简洁（或许是个人感觉），刚好最近项目需要用到WebSocket，因此鄙人抽时间基于DotNetty进行简单封装了下，撸出一个力求**简单、高效、优雅**的Socket框架。

# 使用方式

## TcpSocket

- 服务端
``` c#
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
```
- 客户端
``` c#
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

```
## 
