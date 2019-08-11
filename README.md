# DotNettyRPC
## 1.简介
DotNettyRPC是一个基于DotNetty的跨平台RPC框架，支持.NET45以及.NET Standard2.0
## 2.产生背景
传统.NET开发中遇到远程调用服务时，多以WCF为主。而WCF虽然功能强大，但是其配置复杂，不易于上手。而且未来必定是.NET Core的天下,WCF暂不支持.NET Core（只有客户端，无法建立服务端）。市面上的其他.NET的 RPC框架诸如gRPC、surging甚至微服务框架Orleans等，这些框架功能强大，性能也很好，并且比较成熟，但是使用起来不够简单。基于上述比较（无任何吹捧贬低的意思），鄙人不才撸了一个轮子DotNettyRPC，它的定位是一个跨平台(.NET45和.NET Standard)、简单却实用的RPC框架

## 3.使用方法
### 3.1引入DotNettyRPC
打开Nuget包管理器，搜索DotNettyRPC即可找到并使用

或输入Nuget命令：Install-Package DotNettyRPC
### 3.2定义服务接口
``` csharp

    public interface IHello
    {
        string SayHello(string msg);
    }
	
    public class Hello : IHello
    {
        public string SayHello(string msg)
        {
            return msg;
        }
    }
```
### 3.3服务端
``` c#
using Coldairarrow.DotNettyRPC;
using Common;
using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            RPCServer rPCServer = new RPCServer(9999);
            rPCServer.RegisterService<IHello, Hello>();
            rPCServer.Start();

            Console.ReadLine();
        }
    }
}

```
### 3.4客户端
``` c#
using Coldairarrow.DotNettyRPC;
using Common;
using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IHello client = RPCClientFactory.GetClient<IHello>("127.0.0.1", 9999);
            var msg = client.SayHello("Hello");
            Console.WriteLine(msg);
            Console.ReadLine();
        }
    }
}

```
### 3.5运行
先运行服务端,再运行客户端,即可在客户端输出Hello

## 4.结语
本机测试一次RPC请求平均0.4ms左右，性能不高，但是足以应对绝大多数业务场景，重在简单实用。可以优化的地方很多，还望大家多多支持。

GitHub地址：https://github.com/Coldairarrow/DotNettyRPC

QQ群：373144077


