using System;

namespace VisualStudioEX3.Artemis.Framework.ServiceInjector.Exceptions
{
    /// <summary>
    /// Exception for when trying to get a service that not is registered in the container.
    /// </summary>
    /// <remarks>Ensure that the service is registered in the container or check the container where you request the service.</remarks>
    public class ServiceNotFoundException : Exception
    {
        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="template"><see cref="Type"/> interface that throw the exception.</param>
        public ServiceNotFoundException(Type template)
            : base($"The service \"{template.Name}\" was not found.")
        {
        }
        #endregion
    }
}
