using System.Collections.Generic;
using System.ComponentModel;

namespace WinAppDriver.Generation.Events.Keyboard.Models
{
    /// <summary>
    ///     Leveraged in UiEvent and is the main supporting class for sending keys
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KeyboardEvent
    {
        public KeyboardEvent()
        {
            KeyPresses = new List<KeyboardKeyPress>();
        }

        public KeyboardEvent(KeyboardState keyboardState)
        {
            KeyPresses = new List<KeyboardKeyPress>(keyboardState.KeyPresses);
        }

        /// <summary>
        ///     List of KeyPresses retrieved from KeyboardState + Generation
        /// </summary>
        public List<KeyboardKeyPress> KeyPresses { get; set; }

        /// <summary>
        ///     Generates a description of the keys pressed
        /// </summary>
        public string Description => KeyboardConstants.GetKeyboardDescription(KeyPresses);
    }
}