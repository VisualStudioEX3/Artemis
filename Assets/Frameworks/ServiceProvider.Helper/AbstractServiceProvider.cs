using VisualStudioEX3.Artemis.Framework.ServiceProvider.Contracts.Interfaces;

namespace VisualStudioEX3.Artemis.Framework.ServiceProvider.Helper
{
    /// <summary>
    /// Abstract class thats implements a ready to use <see cref="IServiceProvider"/> implementation.
    /// </summary>
    /// <remarks>This class abstracts completely for the creation of a <see cref="IServiceContainer"/> and <see cref="IServiceProvider"/> base implementation.</remarks>
    public abstract class AbstractServiceProvider<T> : IServiceProvider //where T: class
    {
        #region Internal vars
        private IServiceContainer _services;
        private static T _factory;
        #endregion

        #region Properties
        public static T Factory
        {
            get
            {
                _factory ??= _factory = (T)System.Activator.CreateInstance(typeof(T));
                return _factory;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialized this <see cref="IServiceProvider"/> instance.
        /// </summary>
        public AbstractServiceProvider() => this._services = ServiceProviderFactory.CreateServiceContainer(this.RegisterServices);
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Abstract method where register the services for the internal <see cref="IServiceContainer"/>.
        /// </summary>
        /// <param name="serviceContainer">The internal <see cref="IServiceContainer"/> instance.</param>
        public abstract void RegisterServices(IServiceContainer serviceContainer);

        /// <summary>
        /// Gets an instance for the required generic service.
        /// </summary>
        /// <param name="template">Interface used when registered the required generic service.</param>
        /// <param name="implementations">Array with all classes that the generic service implements. They can be generic classes or other services registered in this container.</param>
        /// <returns>Returns a new instance of the generic service or the existing one if is a singleton service.</returns>
        public object GetService(System.Type template, params System.Type[] implementations) => this._services.GetService(template, implementations);

        /// <summary>
        /// Gets an instance for the required service.
        /// </summary>
        /// <param name="template">Interface used when registered the required service.</param>
        /// <returns>Returns a new instance of the service or the existing one if is a singleton service.</returns>
        public object GetService(System.Type template) => this._services.GetService(template);

        /// <summary>
        /// Gets an instance for the required service.
        /// </summary>
        /// <typeparam name="I">Interface used when registered the required service.</typeparam>
        /// <returns>Returns a new instance of the service or the existing one if is a singleton service.</returns>
        public I GetService<I>() => this._services.GetService<I>();
        #endregion
    }
}
