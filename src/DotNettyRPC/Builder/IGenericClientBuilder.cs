using System;

namespace Coldairarrow.DotNettySocket
{
    public interface IGenericClientBuilder<TBuilder, TTarget, TData> : IBuilder<TBuilder, TTarget>
    {
        TBuilder OnClientStarted(Action<TTarget> action);

        TBuilder OnRecieve(Action<TTarget, TData> action);

        TBuilder OnSend(Action<TTarget, TData> action);

        TBuilder OnClientClose(Action<TTarget> action);
    }
}
