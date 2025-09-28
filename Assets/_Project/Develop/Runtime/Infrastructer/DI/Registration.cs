using System;

namespace Assets._Project.Develop.Runtime.Infrastructer.DI
{
    public class Registration : IRegistrationOptions
    {
        private object _cachedInstance;
        private Func<DIContainer, object> _creator;

        public bool IsNonLazy {  get; private set; } = false;

        public Registration(Func<DIContainer, object> creator) => _creator = creator;

        public object CreateInstanceFrom(DIContainer container)
        {
            if (_cachedInstance != null)
                return _cachedInstance;

            if (_creator == null)
                throw new InvalidOperationException("Creator not exist");

            _cachedInstance = _creator.Invoke(container);

            return _cachedInstance;
        }

        public void NonLazy() => IsNonLazy = true;

        public void OnInitialize()
        {
            if (_cachedInstance != null)
                if (_cachedInstance is IInitializable initializable)
                    initializable.Initialize();
        }

        public void OnDispose()
        {
            if (_cachedInstance != null)
                if (_cachedInstance is IDisposable disposable)
                    disposable.Dispose();
        }
    }
}
