using System;
using static BaseMvvmToolKIt.TinyIoCContainer;

namespace BaseMvvmToolKIt
{
    public interface ITinyIOC
    {
        object Resolve(Type resolveType);
        RegisterOptions Register<RegisterType>(RegisterType instance) where RegisterType : class;
        IRegisterOptions Register<RegisterType>(RegisterType instance, string name) where RegisterType : class;
        ResolveType Resolve<ResolveType>() where ResolveType : class;
        ResolveType Resolve<ResolveType>(string name) where ResolveType : class;
        IRegisterOptions Register<RegisterType, RegisterImplementation>()
            where RegisterType : class
            where RegisterImplementation : class, RegisterType;
        void Unregister<RegisterType>();
        void Unregister<RegisterType>(string name);
    }
}

