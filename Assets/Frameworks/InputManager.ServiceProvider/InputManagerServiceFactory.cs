using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.InputManager.Services.Processors;
using VisualStudioEX3.Artemis.Framework.ServiceProvider.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.ServiceProvider.Helper;

namespace VisualStudioEX3.Artemis.Framework.InputManager.ServiceProvider
{
    /// <summary>
    /// Input Manager factory services.
    /// </summary>
    public class InputManagerServiceFactory : AbstractServiceProvider<InputManagerServiceFactory>
    {
        #region Methods & Functions
        public override void RegisterServices(IServiceContainer serviceContainer)
        {
            serviceContainer.AddSingleton<IKeyboardMouseButtonProcessor, UnityLegacyInputKeyboardMouseButtonProcessor>();
            serviceContainer.AddSingleton<IMousePositionAxesProcessor, UnityLegacyInputMouseAxesProcessor>();
            serviceContainer.AddSingleton<IInputActionProcessor, UnityLegacyInputActionsProcessor>();
            serviceContainer.AddSingleton<IInputAxisProcessor, UnityLegacyInputAxisProcessor>();
        } 
        #endregion
    }
}
