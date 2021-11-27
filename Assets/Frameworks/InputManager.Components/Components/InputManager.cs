using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;
using VisualStudioEX3.Artemis.Framework.InputManager.ServiceProvider;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Components
{
    public class InputManager : MonoBehaviour
    {
        private IKeyboardMouseProcessor _keboardMouseProcessor;

        public InputAction[] actions;

        private void Awake()
        {
            this._keboardMouseProcessor = InputManagerServiceFactory.Factory.GetService<IKeyboardMouseProcessor>();
        }

        void Start()
        {
        
        }

        void Update()
        {
        
        }
    }
}
