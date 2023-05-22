namespace WinAppDriver.Generation.Events.Keyboard.Models
{
    /// <summary>
    ///     Supporting class for KeyboardEvent
    /// </summary>
    public class KeyboardKeyPress
    {
        public KeyboardVirtualKeys VirtualKey { get; set; }

        public bool IsDown { get; set; }

        public override string ToString()
        {
            return $"{VirtualKey.ToString()}";
        }
    }
}