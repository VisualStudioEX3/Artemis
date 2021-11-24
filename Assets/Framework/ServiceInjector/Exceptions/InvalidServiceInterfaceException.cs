using System;

namespace VisualStudioEX3.Artemis.Framework.ServiceInjector.Exceptions
{
    /// <summary>
    /// Exception for when trying to register a service using type that not are an interface.
    /// </summary>
    /// <remarks>You only can register services using interfaces.</remarks>
    public sealed class InvalidServiceInterfaceException : Exception
    {
        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type"><see cref="Type"/> that throw the exception.</param>
        public InvalidServiceInterfaceException(Type type)
            : base($"The type \"{type.Name}\" must be an interface.")
        {
        }
        #endregion
    }
}
