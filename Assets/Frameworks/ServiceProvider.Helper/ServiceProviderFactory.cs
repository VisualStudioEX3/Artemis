using System;
using VisualStudioEX3.Artemis.Framework.ServiceProvider.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.ServiceProvider.Services.Containers;

namespace VisualStudioEX3.Artemis.Framework.ServiceProvider.Helper
{
    /// <summary>
    /// Factory class with functions to create <see cref="IServiceContainer"/> instances.
    /// </summary>
    public static class ServiceProviderFactory
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
            IServiceContainer container = ServiceProviderFactory.CreateServiceContainer();

            registerServices(container);

            return container;
        } 
        #endregion
    }
}
