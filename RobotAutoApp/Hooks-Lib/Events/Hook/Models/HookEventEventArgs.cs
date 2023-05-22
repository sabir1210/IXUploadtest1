using System;

namespace WinAppDriver.Generation.Events.Hook.Models
{
    /// <summary>
    ///     HookEvent EventArgs
    /// </summary>
    public class HookEventEventArgs : EventArgs
    {
        public HookEvent HookEvent { get; set; }
    }
}