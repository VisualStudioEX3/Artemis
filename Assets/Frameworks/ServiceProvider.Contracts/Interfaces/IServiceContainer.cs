using System;

namespace VisualStudioEX3.Artemis.Framework.ServiceProvider.Contracts.Interfaces
{
    /// <summary>
    /// A container class to store services.
    /// </summary>
    /// <remarks>Use this class to register and store a group of related classes using their interfaces.</remarks>
    public interface IServiceContainer : IServiceProvider
    {
        #region Methods & Functions
        /// <summary>
        /// Adds a generic service.
        /// </summary>
        /// <param name="template">Generic interface to register this service.</param>
        /// <param name="implementation">Generic class used as implementation for this service.</param>
        void AddGenericService(Type template, Type implementation);

        /// <summary>
        /// Adds a generic singleton service.
        /// </summary>
        /// <param name="template">Generic interface to register this singleton service.</param>
        /// <param name="implementation">Generic class used as implementation for this service.</param>
        void AddGenericSingleton(Type template, Type implementation);

        /// <summary>
        /// Adds a service.
        /// </summary>
        /// <typeparam name="I">Interface to register this service.</typeparam>
        /// <typeparam name="T">Class used as implementation for this service.</typeparam>
        void AddService<I, T>() where T : class;

        /// <summary>
        /// Adds a singleton service.
        /// </summary>
        /// <typeparam name="I">Interface to register this singleton service.</typeparam>
        /// <typeparam name="T">Class used as implementation for this singleton service.</typeparam>
        void AddSingleton<I, T>() where T : class;

        /// <summary>
        /// Removes a registered service in this container.
        /// </summary>
        /// <param name="template">Interface used when registered the required service.</param>
        void Remove(Type template);

        /// <summary>
        /// Removes a registered service in this container.
        /// </summary>
        /// <typeparam name="I">Interface used when registered the required service.</typeparam>
        void Remove<I>();

        /// <summary>
        /// Removes all registered services in this container.
        /// </summary>
        void RemoveAll();
        #endregion
    }
}