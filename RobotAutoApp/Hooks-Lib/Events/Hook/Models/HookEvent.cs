using System.Collections.Generic;
using WinAppDriver.Generation.Events.Keyboard.Models;
using WinAppDriver.Generation.Events.Mouse.Models;
using WinAppDriver.Generation.UiElements.Models;
using WinAppDriver.Generation.UiEvents.Models;

namespace WinAppDriver.Generation.Events.Hook.Models
{
    /// <summary>
    ///     Leveraged in Hook Event Handler and is the precursor to UiEvent
    /// </summary>
    public class HookEvent
    {
        public HookEvent()
        {
            UiEventType = UiEventTypes.Inspect;
            UiWindows = new List<UiElement>();
        }

        public HookEvent(UiElement uiElement, UiElement parentElement, List<UiElement> uiWindows)
        {
            UiEventType = UiEventTypes.Inspect;
            UiWindows = new List<UiElement>();

            UiElement = uiElement;
            UiWindows = uiWindows;
            ParentElement = parentElement;
        }

        public UiElement UiElement { get; set; }
        public UiElement ParentElement { get; set; }

        public List<UiElement> UiWindows { get; set; }

        public UiEventTypes UiEventType { get; set; }

        public MouseState MouseState { get; set; }

        public KeyboardState KeyboardState { get; set; }
    }
}