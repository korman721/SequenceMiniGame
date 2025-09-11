using System;

namespace Assets._Project.Develop.Runtime.Infrastructer.DI
{
    public class Registration
    {
        private object _cachedInstance;
        private Func<DIContainer, object> _creator;

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
    }
}
