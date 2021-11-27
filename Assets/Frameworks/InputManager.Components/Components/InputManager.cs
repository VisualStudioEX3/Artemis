using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Components
{
    public class InputManager : MonoBehaviour
    {
        private IKeyboardMouseProcessor _keboardMouseProcessor;

        public InputAction[] actions;

        private void Awake()
        {
            
        }

        void Start()
        {
        
        }

        void Update()
        {
        
        }
    }
}
