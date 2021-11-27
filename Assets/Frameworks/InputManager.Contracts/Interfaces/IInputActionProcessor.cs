using System.Collections.Generic;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Interfaces
{
    public interface IInputActionProcessor
    {
        /// <summary>
        /// Update the <see cref="InputAction"/> states.
        /// </summary>
        /// <param name="actions"><see cref="InputAction"/> list.</param>
        void Update(IEnumerable<InputAction> actions);
    }
}
