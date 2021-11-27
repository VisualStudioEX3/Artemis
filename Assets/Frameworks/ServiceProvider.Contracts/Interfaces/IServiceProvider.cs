using System;

namespace VisualStudioEX3.Artemis.Framework.ServiceProvider.Contracts.Interfaces
{
    /// <summary>
    /// A service provider object.
    /// </summary>
    /// <remarks>Use this interface to implements an object to request already registered services in a <see cref="IServiceContainer"/>.</remarks>
    public interface IServiceProvider
    {
        #region Methods & Functions
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
        #endregion
    }
}
