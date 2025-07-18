using System;
using System.Collections.Generic;

namespace Core.DependencyInjection
{
    public enum ServiceLifetime
    {
        Singleton,
        Transient
    }

    public class SimpleDependencyResolver : IDependencyResolver
    {
        private readonly Dictionary<Type, object> _singletons = new();
        private readonly Dictionary<Type, Func<object>> _transients = new();

        public void Register<T>(T instance) => Register<T>(() => instance, ServiceLifetime.Singleton);

        public void Register<T>(Func<T> factory, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            if (lifetime == ServiceLifetime.Singleton)
                _singletons[typeof(T)] = factory();
            else
                _transients[typeof(T)] = () => factory();
        }

        public T Resolve<T>()
        {
            if (_singletons.TryGetValue(typeof(T), out var singleton))
                return (T)singleton;
            if (_transients.TryGetValue(typeof(T), out var factory))
                return (T)factory();
            throw new InvalidOperationException($"Service of type {typeof(T)} is not registered.");
        }
    }
} 