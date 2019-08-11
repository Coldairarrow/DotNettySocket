using System;
using System.Threading.Tasks;

namespace Coldairarrow.DotNettySocket
{
    public interface IBuilder<TBuilder, TTarget>
    {
        TBuilder OnException(Action<Exception> action);

        Task<TTarget> BuildAsync();
    }
}
