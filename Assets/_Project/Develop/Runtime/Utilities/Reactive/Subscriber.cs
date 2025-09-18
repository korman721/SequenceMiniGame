using System;

namespace Assets._Project.Develop.Runtime.Utilities.Reactive
{
    public class Subscriber<T, K> : IDisposable
    {
        public event Action<T, K> _action;
        public event Action<Subscriber<T, K>> _onDispose;

        public Subscriber(Action<T, K> action, Action<Subscriber<T, K>> onDispose)
        {
            _action = action;
            _onDispose = onDispose;
        }

        public void Dispose() => _onDispose?.Invoke(this);

        public void Invoke(T arg1, K arg2) => _action?.Invoke(arg1, arg2);
    }
}
