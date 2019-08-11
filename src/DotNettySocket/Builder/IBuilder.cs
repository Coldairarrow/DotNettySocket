using System;
using System.Threading.Tasks;

namespace Coldairarrow.DotNettySocket
{
    /// <summary>
    /// 构建者
    /// </summary>
    /// <typeparam name="TBuilder">特定构建者</typeparam>
    /// <typeparam name="TTarget">目标生成类</typeparam>
    public interface IBuilder<TBuilder, TTarget>
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="action">异常处理委托</param>
        /// <returns></returns>
        TBuilder OnException(Action<Exception> action);

        /// <summary>
        /// 构建目标生成类
        /// </summary>
        /// <returns></returns>
        Task<TTarget> BuildAsync();
    }
}
