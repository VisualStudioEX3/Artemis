using System;

namespace VisualStudioEX3.Artemis.Framework.ServiceProvider.Contracts.Exceptions
{
    /// <summary>
    /// Exception for when trying to register a type as service implementation that not is a class.
    /// </summary>
    /// <remarks>You only can register classes as service implementation.</remarks>
    public sealed class InvalidServiceClassException : Exception
    {
        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="service"><see cref="Type"/> that throw the exception.</param>
        public InvalidServiceClassException(Type service)
            : base($"The type \"{service.Name}\" must be a class.")
        {
        }
        #endregion
    }
}
