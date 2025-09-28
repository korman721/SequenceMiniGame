using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Assets._Project.Develop.Runtime.Infrastructer.DI
{
    public class DIContainer
    {
        private readonly Dictionary<Type, Registration> _typeRegistration = new();
        
        private readonly List<Type> _requests = new();

        private DIContainer _parent;

        public DIContainer() : this(null) { }

        public DIContainer(DIContainer parent) => _parent = parent;

        public Registration RegisterAsSingle<T>(Func<DIContainer, T> creator)
        {
            if (IsAlreadyRegister<T>())
                throw new InvalidOperationException($"{typeof(T)} is already register");

            Registration registration = new Registration(container => creator.Invoke(container));
            _typeRegistration.Add(typeof(T), registration);
            return registration;
        }

        public bool IsAlreadyRegister<T>()
        {
            if (_typeRegistration.ContainsKey(typeof(T)))
                return true;

            if (_parent != null)
                return _parent.IsAlreadyRegister<T>();

            return false;
        }

        public T Resolve<T>()
        {
            if (_requests.Contains(typeof(T)))
                throw new InvalidOperationException($"Cycle resolve for {typeof(T)}");

            _requests.Add(typeof(T));

            try
            {
                if (_typeRegistration.TryGetValue(typeof(T), out Registration registration))
                    return (T)registration.CreateInstanceFrom(this);

                if (_parent != null)
                    return _parent.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T));
            }

            throw new InvalidOperationException($"Registration for {typeof(T)} not exists");
        }

        public void Initialize()
        {
            foreach (Registration registration in _typeRegistration.Values)
            {
                if (registration.IsNonLazy)
                    registration.CreateInstanceFrom(this);

                registration.OnInitialize();
            }
        }

        public void Dispose()
        {
            foreach (Registration registration in _typeRegistration.Values)
                registration.OnDispose();
        }
    }
}
