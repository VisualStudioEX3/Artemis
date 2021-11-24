using System;
using VisualStudioEX3.Artemis.Framework.ServiceInjector.Contracts;
using VisualStudioEX3.Artemis.Framework.ServiceInjector.Containers;

namespace VisualStudioEX3.Artemis.Framework.ServiceInjector
{
    /// <summary>
    /// Root class with helper functions to create <see cref="IServiceContainer"/> instances.
    /// </summary>
    public static class ServiceProvider
    {
        #region Methods & Functions
        /// <summary>
        /// Creates a new <see cref="IServiceContainer"/> instance.
        /// </summary>
        /// <returns>Returns a new ready container to store services to request them when are needed.</returns>
        public static IServiceContainer CreateServiceContainer() => new ServiceContainer();

        /// <summary>
        /// Creates a new <see cref="IServiceContainer"/> instance.
        /// </summary>
        /// <param name="registerServices">Lambda method where setup and register the services for this container.</param>
        /// <returns>Returns a new ready container to store services to request them when are needed.</returns>
        public static IServiceContainer CreateServiceContainer(Action<IServiceContainer> registerServices)
        {
            IServiceContainer container = ServiceProvider.CreateServiceContainer();

            registerServices(container);

            return container;
        } 
        #endregion
    }
}
