using System;

namespace VisualStudioEX3.Artemis.Framework.ServiceInjector.Contracts
{
    /// <summary>
    /// A container class to store services.
    /// </summary>
    /// <remarks>Use this class to register and store a group of related classes using their interfaces.</remarks>
    public interface IServiceContainer
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
        /// Gets an instance for the required generic service.
        /// </summary>
        /// <param name="template">Interface used when registered the required generic service.</param>
        /// <param name="implementations">Array with all classes that the generic service implements. They can be generic classes or other services registered in this container.</param>
        /// <returns>Returns a new instance of the generic service or the existing one if is a singleton service.</returns>
        object GetService(Type template, params Type[] implementations);

        /// <summary>
        /// Gets an instance for the required service.
        /// </summary>
        /// <param name="template">Interface used when registered the required service.</param>
        /// <returns>Returns a new instance of the service or the existing one if is a singleton service.</returns>
        object GetService(Type template);

        /// <summary>
        /// Gets an instance for the required service.
        /// </summary>
        /// <typeparam name="I">Interface used when registered the required service.</typeparam>
        /// <returns>Returns a new instance of the service or the existing one if is a singleton service.</returns>
        I GetService<I>();

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