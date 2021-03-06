using VisualStudioEX3.Artemis.Framework.Core.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.Core.Services;
using VisualStudioEX3.Artemis.Framework.ServiceProvider.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.ServiceProvider.Helper;

namespace VisualStudioEX3.Artemis.Framework.Core.ServiceProvider
{
    /// <summary>
    /// Core factory services.
    /// </summary>
    public class CoreServiceFactory : AbstractServiceProvider<CoreServiceFactory>
    {
        #region Methods & Functions
        public override void RegisterServices(IServiceContainer serviceContainer)
        {
            serviceContainer.AddSingleton<IMathHelper, MathHelper>();
            serviceContainer.AddGenericService(typeof(IUnityObjectPoolService<>), typeof(UnityObjectPoolService<>));
        } 
        #endregion
    }
}
