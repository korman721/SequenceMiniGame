using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.Reactive
{
    public class ReactiveVariable<T> : IReadonlyVariable<T> where T : IEquatable<T>
    {
        private T _value;

        private List<Subscriber<T, T>> _subscribers = new();
        private List<Subscriber<T, T>> _toAdd = new();
        private List<Subscriber<T, T>> _toRemove = new();

        public ReactiveVariable() => _value = default;

        public ReactiveVariable(T value) => _value = value;

        public T Value
        {
            get => _value;
            set
            {
                T oldValue = _value;

                _value = value;

                if (oldValue.Equals(_value) != true)
                    Invoke(oldValue, _value);
            }
        }

        public IDisposable Subscribe(Action<T, T> action)
        {
            Subscriber<T, T> subscriber = new Subscriber<T, T>(action, Remove);
            _toAdd.Add(subscriber);

            return subscriber;
        }

        private void Remove(Subscriber<T, T> subscriber) => _toRemove.Add(subscriber);

        private void Invoke(T oldValue, T value)
        {
            if (_toAdd.Count > 0)
            {
                _subscribers.AddRange(_toAdd);
                _toAdd.Clear();
            }

            if (_toRemove.Count > 0)
            {
                foreach (Subscriber<T, T> subscriber in _toRemove)
                    _subscribers.Remove(subscriber);

                _toRemove.Clear();
            }

            foreach (Subscriber<T, T> subscriber in _subscribers)
                subscriber.Invoke(oldValue, value);
        }
    }
}
