﻿using System;

namespace BaseMvvmToolKIt
{
    /// <summary>
    /// Built in TinyIOC for ease of use
    /// </summary>
    public class FreshTinyIOCBuiltIn : ITinyIOC
    {
        public static TinyIoCContainer Current
        {
            get
            {
                return TinyIoCContainer.Current;
            }
        }
        public IRegisterOptions Register<RegisterType>(RegisterType instance, string name) where RegisterType : class
        {
            return TinyIoCContainer.Current.Register(instance, name);
        }

        public IRegisterOptions Register<RegisterType>(RegisterType instance) where RegisterType : class
        {
            return TinyIoCContainer.Current.Register(instance);
        }

        public ResolveType Resolve<ResolveType>(string name) where ResolveType : class
        {
            return TinyIoCContainer.Current.Resolve<ResolveType>(name);
        }

        public ResolveType Resolve<ResolveType>() where ResolveType : class
        {
            return TinyIoCContainer.Current.Resolve<ResolveType>();
        }

        public IRegisterOptions Register<RegisterType, RegisterImplementation>()
            where RegisterType : class
            where RegisterImplementation : class, RegisterType
        {
            return TinyIoCContainer.Current.Register<RegisterType, RegisterImplementation>();
        }

        public object Resolve(Type resolveType)
        {
            return TinyIoCContainer.Current.Resolve(resolveType);
        }

        public void Unregister<RegisterType>()
        {
            TinyIoCContainer.Current.Unregister<RegisterType>();
        }

        public void Unregister<RegisterType>(string name)
        {
            TinyIoCContainer.Current.Unregister<RegisterType>(name);
        }

        TinyIoCContainer.RegisterOptions ITinyIOC.Register<RegisterType>(RegisterType instance)
        {
            throw new NotImplementedException();
        }

    }
}

