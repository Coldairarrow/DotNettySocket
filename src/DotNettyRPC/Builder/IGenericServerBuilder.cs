using System;

namespace Coldairarrow.DotNettySocket
{
    /// <summary>
    /// 泛型服务端构建者
    /// </summary>
    /// <typeparam name="TBuilder">特定构建者</typeparam>
    /// <typeparam name="TTarget">目标生成类</typeparam>
    /// <typeparam name="IConnection">特定连接</typeparam>
    /// <typeparam name="TData">数据类型</typeparam>
    /// <seealso cref="Coldairarrow.DotNettySocket.IBuilder{TBuilder, TTarget}" />
    public interface IGenericServerBuilder<TBuilder, TTarget, IConnection, TData> : IBuilder<TBuilder, TTarget>
    {
        /// <summary>
        /// 服务启动事件
        /// </summary>
        /// <param name="action">处理服务启动事件</param>
        /// <returns></returns>
        TBuilder OnServerStarted(Action<TTarget> action);

        /// <summary>
        /// 收到新连接事件
        /// </summary>
        /// <param name="action">处理收到新连接事件</param>
        /// <returns></returns>
        TBuilder OnNewConnection(Action<TTarget, IConnection> action);

        /// <summary>
        /// 连接收到数据事件
        /// </summary>
        /// <param name="action">处理连接收到数据事件</param>
        /// <returns></returns>
        TBuilder OnRecieve(Action<TTarget, IConnection, TData> action);

        /// <summary>
        /// 连接发送数据事件
        /// </summary>
        /// <param name="action">处理接发送数据事件</param>
        /// <returns></returns>
        TBuilder OnSend(Action<TTarget, IConnection, TData> action);

        /// <summary>
        /// 连接关闭事件
        /// </summary>
        /// <param name="action">处理连接关闭事件</param>
        /// <returns></returns>
        TBuilder OnConnectionClose(Action<TTarget, IConnection> action);
    }
}
