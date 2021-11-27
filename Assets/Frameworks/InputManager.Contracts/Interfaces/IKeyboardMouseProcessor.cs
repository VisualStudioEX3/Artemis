using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Enums;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Interfaces
{
    /// <summary>
    /// Service used to check the game input values from keyboard and mouse.
    /// </summary>
    public interface IKeyboardMouseProcessor
    {
        /// <summary>
        /// Gets if is any key is pressed. Not include the mouse wheel steps.
        /// </summary>
        public bool IsAnyKeyPressed { get; }

        /// <summary>
        /// Gets if is any key is down. Not include the mouse wheel steps.
        /// </summary>
        public bool IsAnyKeyDown { get; }

        /// <summary>
        /// Gets if the key or mouse button is pressed.
        /// </summary>
        /// <param name="code">Keyboard or mouse button code to check.</param>
        /// <returns>Returns <see langword="true"/> if the key or mouse button is pressed.</returns>
        /// <remarks>If the code is <see cref="KeyboardMouseCodes.MouseWheelDown"/> or <see cref="KeyboardMouseCodes.MouseWheelUp"/>, then only returns <see langword="true"/> while the wheel steps (like if you uses <see cref="IsDown(KeyboardMouseCodes)"/> function).</remarks>
        bool IsPressed(KeyboardMouseCodes code);

        /// <summary>
        /// Gets if the key or mouse button is down.
        /// </summary>
        /// <param name="code">Keyboard or mouse button code to check.</param>
        /// <returns>Returns <see langword="true"/> if the key or mouse button is down.</returns>
        bool IsDown(KeyboardMouseCodes code);

        /// <summary>
        /// Gets if the key or mouse button is up or released.
        /// </summary>
        /// <param name="code">Keyboard or mouse button code to check.</param>
        /// <returns>Returns <see langword="true"/> if the key or mouse button is up or released.</returns>
        /// <remarks>If the code is <see cref="KeyboardMouseCodes.MouseWheelDown"/> or <see cref="KeyboardMouseCodes.MouseWheelUp"/>, then not returns <see langword="true"/> when the mouse wheel stops.</remarks>
        bool IsUp(KeyboardMouseCodes code);
    }
}