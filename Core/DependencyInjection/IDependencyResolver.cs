using System;

namespace Core.DependencyInjection
{
    public interface IDependencyResolver
    {
        T Resolve<T>();
    }
} 