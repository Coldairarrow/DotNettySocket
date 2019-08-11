using System;

namespace Coldairarrow.DotNettySocket
{
    /// <summary>
    /// 泛型客户端构建者
    /// </summary>
    /// <typeparam name="TBuilder">特定构建者</typeparam>
    /// <typeparam name="TTarget">目标生成类</typeparam>
    /// <typeparam name="TData">数据类型</typeparam>
    /// <seealso cref="Coldairarrow.DotNettySocket.IBuilder{TBuilder, TTarget}" />
    public interface IGenericClientBuilder<TBuilder, TTarget, TData> : IBuilder<TBuilder, TTarget>
    {
        /// <summary>
        /// 客户端启动事件
        /// </summary>
        /// <param name="action">处理客户端启动事件</param>
        /// <returns></returns>
        TBuilder OnClientStarted(Action<TTarget> action);

        /// <summary>
        /// 接受数据事件
        /// </summary>
        /// <param name="action">处理接受数据事件</param>
        /// <returns></returns>
        TBuilder OnRecieve(Action<TTarget, TData> action);

        /// <summary>
        /// 发送数据事件
        /// </summary>
        /// <param name="action">处理发送数据事件</param>
        /// <returns></returns>
        TBuilder OnSend(Action<TTarget, TData> action);

        /// <summary>
        /// 客户端关闭事件
        /// </summary>
        /// <param name="action">处理客户端关闭事件</param>
        /// <returns></returns>
        TBuilder OnClientClose(Action<TTarget> action);
    }
}
