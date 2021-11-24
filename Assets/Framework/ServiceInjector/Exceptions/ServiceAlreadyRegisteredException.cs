using System;

namespace VisualStudioEX3.Artemis.Framework.ServiceInjector.Exceptions
{
    /// <summary>
    /// Exception for when trying to register a service using an already registered interface.
    /// </summary>
    /// <remarks>Each service must be using an unique interface as key.</remarks>
    public sealed class ServiceAlreadyRegisteredException : Exception
    {
        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="service"><see cref="Type"/> that throw the exception.</param>
        public ServiceAlreadyRegisteredException(Type service)
            : base($"The service \"{service.Name}\" is already registered.")
        {
        }
        #endregion
    }
}
