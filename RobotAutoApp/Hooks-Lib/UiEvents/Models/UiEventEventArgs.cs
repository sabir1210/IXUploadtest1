using System;

namespace WinAppDriver.Generation.UiEvents.Models
{
    /// <summary>
    ///     UiEvent EventArgs
    /// </summary>
    public class UiEventEventArgs : EventArgs
    {
        public UiEvent UiEvent { get; set; }

        public TimeSpan UiEventDelay { get; set; }
    }
}