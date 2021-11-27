using System;

namespace VisualStudioEX3.Artemis.Framework.ServiceProvider.Contracts.Exceptions
{
    /// <summary>
    /// Exception for when you trying to register an abstract class as service.
    /// </summary>
    /// <remarks>You can't register abstract classes as services.</remarks>
    public sealed class ServiceAbstractClassException : Exception
    {
        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="service"><see cref="Type"/> that throw the exception.</param>
        public ServiceAbstractClassException(Type service)
            : base($"The type \"{service.Name}\" must not to be an abstract class.")
        {
        }
        #endregion
    }
}
