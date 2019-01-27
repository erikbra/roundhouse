using System;

namespace roundhouse.connections
{
    public interface IConnection<out T> : IDisposable
    {
        void open();
        void clear_pool();
        void close();
        T underlying_type();
    }
}