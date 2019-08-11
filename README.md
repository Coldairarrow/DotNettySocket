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
Tcp是面向连接的，所以服务端对连接的管理就至关重要，框架支持各种事件的处理、给连接设置连接名（身份标识）、通过连接名找到特定连接、连接收发数据、分包、粘包处理。
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
## WebSocket
WebSocket与TcpSocket接口基本保持一致，仅有的区别就是TcpSocket支持字节的收发并且需要自行处理分包粘包。而WebSocket直接收发字符串（UTF-8）编码，并且无需考虑分包粘包。框架目前没有支持WSS，建议解决方案是使用Nginx转发即可（相关资料一搜便有）
- 服务端
``` c#
using Coldairarrow.DotNettySocket;
using System;
using System.Threading.Tasks;

namespace WebSocket.Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var theServer = await SocketBuilderFactory.GetWebSocketServerBuilder(6002)
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
                .OnRecieve((server, connection, msg) =>
                {
                    Console.WriteLine($"服务端:数据{msg}");
                    connection.Send(msg);
                })
                .OnSend((server, connection, msg) =>
                {
                    Console.WriteLine($"向连接名[{connection.ConnectionName}]发送数据:{msg}");
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
- 控制台客户端
``` c#
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
```
- 网页客户端
``` javascript
<!DOCTYPE HTML>
<html>
<head>
    <meta charset="utf-8">
    <title>菜鸟教程(runoob.com)</title>

    <script type="text/javascript">
        function WebSocketTest() {
            if ("WebSocket" in window) {
                var ws = new WebSocket("ws://127.0.0.1:6002");

                ws.onopen = function () {
                    console.log('连上服务端');
                    setInterval(function () {
                        ws.send("111111");
                    }, 1000);
                };

                ws.onmessage = function (evt) {
                    var received_msg = evt.data;
                    console.log('收到' + received_msg);
                };

                ws.onclose = function () {
                    console.log("连接已关闭...");
                };
            }

            else {
                alert("您的浏览器不支持 WebSocket!");
            }
        }
    </script>

</head>
<body>
    <div id="sse">
        <a href="javascript:WebSocketTest()">运行 WebSocket</a>
    </div>
</body>
</html>
```
## UdpSocket
Udp天生便是收发一体的，以下分为服务端与客户端仅仅是为了方便理解
- 服务端
``` c#
using Coldairarrow.DotNettySocket;
using System;
using System.Text;
using System.Threading.Tasks;

namespace UdpSocket.Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var theServer = await SocketBuilderFactory.GetUdpSocketBuilder(6003)
                .OnClose(server =>
                {
                    Console.WriteLine($"服务端关闭");
                })
                .OnException(ex =>
                {
                    Console.WriteLine($"服务端异常:{ex.Message}");
                })
                .OnRecieve((server, point, bytes) =>
                {
                    Console.WriteLine($"服务端:收到来自[{point.ToString()}]数据:{Encoding.UTF8.GetString(bytes)}");
                    server.Send(bytes, point);
                })
                .OnSend((server, point, bytes) =>
                {
                    Console.WriteLine($"服务端发送数据:目标[{point.ToString()}]数据:{Encoding.UTF8.GetString(bytes)}");
                })
                .OnStarted(server =>
                {
                    Console.WriteLine($"服务端启动");
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

# 结尾
以上所有示例在源码中都有，若觉得不错请点赞加星星，希望能够帮助到大家。

有任何问题请及时反馈或加群交流

QQ群1:（已满） 

QQ群2:579202910
