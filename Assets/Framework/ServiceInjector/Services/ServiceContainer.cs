using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VisualStudioEX3.Artemis.Framework.ServiceInjector.Contracts;
using VisualStudioEX3.Artemis.Framework.ServiceInjector.Exceptions;

namespace VisualStudioEX3.Artemis.Framework.ServiceInjector
{
    public class ServiceContainer : IServiceContainer
    {
        #region Internal vars
        private readonly Dictionary<Type, ServiceModel> _services = new();
        #endregion

        #region Methods & Functions
        private void AddType(Type template, Type implementation, bool isSingleton)
        {
            if (!template.IsInterface)
                throw new InvalidServiceInterfaceException(template);

            if (!implementation.IsClass)
                throw new InvalidServiceClassException(implementation);

            if (implementation.IsAbstract)
                throw new ServiceAbstractClassException(implementation);

            if (!_services.TryAdd(template, new ServiceModel(implementation, isSingleton)))
                throw new ServiceAlreadyRegisteredException(implementation);
        }

        public void AddService<I, T>() where T : class
        {
            this.AddType(typeof(I), typeof(T), false);
        }

        public void AddSingleton<I, T>() where T : class
        {
            this.AddType(typeof(I), typeof(T), true);
        }

        public void AddGenericService(Type template, Type implementation)
        {
            this.AddType(template, implementation.GetGenericTypeDefinition(), false);
        }

        public void AddGenericSingleton(Type template, Type implementation)
        {
            this.AddType(template, implementation.GetGenericTypeDefinition(), true);
        }

        public void RemoveAll()
        {
            foreach (Type template in this._services.Keys)
                this.Remove(template);
        }

        public void Remove<I>() => this.Remove(typeof(I));

        public void Remove(Type template)
        {
            if (!template.IsInterface)
                throw new InvalidServiceInterfaceException(template);

            if (!_services.ContainsKey(template))
                throw new ServiceNotFoundException(template);

            if (template is IDisposable)
                (template as IDisposable).Dispose();

            this._services.Remove(template);
        }

        public I GetService<I>() => (I)this.GetService(typeof(I));

        public object GetService(Type template)
        {
            return !template.IsInterface
                ? throw new InvalidServiceInterfaceException(template)
                : this.ResolveInstance(template, (service) => this.ResolveServiceInstance(service));
        }

        public object GetService(Type template, params Type[] implementations)
        {
            return template.IsInterface
                ? this.ResolveInstance(template, (service) => Activator.CreateInstance(service.type.MakeGenericType(implementations)))
                : throw new InvalidServiceInterfaceException(template);
        }

        private object ResolveServiceInstance(ServiceModel service)
        {
            Type type = service.type;
            List<object> args = null;

            IEnumerable<ConstructorInfo> parametrizedConstructors =
                type.GetConstructors().Where(e => e.GetParameters().Length > 0);

            if (parametrizedConstructors.Any())
            {
                // Takes the first constructor:
                ParameterInfo[] parameters = parametrizedConstructors.First().GetParameters();

                foreach (ParameterInfo parameter in parameters)
                    this.ResolveConstructorServiceParameter(ref args, parameter);
            }

            return args is null
                ? Activator.CreateInstance(type)
                : Activator.CreateInstance(type, args.ToArray());
        }

        private void ResolveConstructorServiceParameter(ref List<object> args, ParameterInfo parameter)
        {
            if (args is null)
                args = new List<object>();

            if (parameter.ParameterType.IsGenericType)
            {
                Type genericTemplate = parameter.ParameterType.GetGenericTypeDefinition();
                Type[] genericTypeParameters = parameter.ParameterType.GetGenericArguments();

                args.Add(this.GetService(genericTemplate, genericTypeParameters));
            }
            else
                args.Add(this.GetService(parameter.ParameterType));
        }

        private object ResolveInstance(Type template, Func<ServiceModel, object> builder)
        {
            if (this._services.TryGetValue(template, out ServiceModel service))
                if (service.isSingleton)
                {
                    if (service.singletonInstance is null)
                        service.singletonInstance = builder(service);

                    return service.singletonInstance;
                }
                else
                    return builder(service);

            throw new ServiceNotFoundException(template);
        } 
        #endregion
    }
}
