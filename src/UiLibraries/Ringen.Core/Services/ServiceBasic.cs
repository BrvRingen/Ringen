using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Ioc;

namespace Ringen.Core.Services
{
    public static class ServiceBasic //TODO: Simple IOC durch Ninject erstezen!
    {
        private static readonly Dictionary<Type, object> m_InstancedServices = new Dictionary<Type, object>();

        public static void Register(Type @interface, Type @class)
        {
            //public void Register<TInterface, TClass>(bool createInstanceImmediately)

            var ioc = SimpleIoc.Default;
            var iocType = typeof(SimpleIoc);
            var methodInfo = iocType.GetMethods().Single(
                m =>
                    m.Name == nameof(SimpleIoc.Register) &&
                    m.GetGenericArguments().Length == 2 &&
                    m.GetParameters().Length == 1);

            var genericArguments = new Type[] { @interface, @class };

            var genericMethodInfo = methodInfo.MakeGenericMethod(genericArguments);

            genericMethodInfo.Invoke(ioc, new object[] { false });
        }

        public static void Register<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface
        {
            SimpleIoc.Default.Register<TInterface, TClass>();
        }

        public static void RegisterInstanced(Type @interface, object @object)
        {
            if (@interface == null) throw new ArgumentNullException(nameof(@interface));
            if (@object == null) throw new ArgumentNullException(nameof(@object));

            if (!@interface.IsInstanceOfType(@object))
            {
                throw new ArgumentException("object is not assignable to specified interface", nameof(@object));
            }

            if (m_InstancedServices.ContainsKey(@interface))
            {
                throw new ArgumentException("interface is already registered", nameof(@interface));
            }

            m_InstancedServices.Add(@interface, @object);
        }

        public static TService GetService<TService>() =>
            (TService)GetService(typeof(TService));

        public static object GetService(Type serviceInterface)
        {
            if (m_InstancedServices.TryGetValue(serviceInterface, out object instancedObject))
                return instancedObject;

            return SimpleIoc.Default.GetService(serviceInterface);
        }
    }
}
