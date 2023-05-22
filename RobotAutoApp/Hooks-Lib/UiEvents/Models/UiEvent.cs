using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WinAppDriver.Generation.Events.Hook.Models;
using WinAppDriver.Generation.Events.Keyboard.Models;
using WinAppDriver.Generation.Events.Mouse.Models;
using WinAppDriver.Generation.UiElements.Models;
using WinAppDriver.Generation.UiWindows.Models;

namespace WinAppDriver.Generation.UiEvents.Models
{
    /// <summary>
    ///     The top level object generated from Hook Event Handler
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class UiEvent
    {
        private List<UiWindow> _uiWindows = new List<UiWindow>();

        public UiEvent()
        {
        }

        public UiEvent(HookEvent hookEvent)
        {
            UiEventType = hookEvent.UiEventType;

            UiElement = hookEvent.UiElement;
            ParentElement = hookEvent.ParentElement;

            MouseEvent = new MouseEvent(hookEvent.MouseState);

            KeyboardEvent = new KeyboardEvent(hookEvent.KeyboardState);

            var hookEventUiWindowList = new List<UiWindow>();

            hookEvent.UiWindows.ForEach(uiWindow => { hookEventUiWindowList.Add(new UiWindow(uiWindow)); });

            UiWindows = hookEventUiWindowList;
        }

        public UiEventTypes UiEventType { get; set; }

        public MouseEvent MouseEvent { get; set; }

        public KeyboardEvent KeyboardEvent { get; set; }

        public UiElement UiElement { get; set; }
        public UiElement ParentElement { get; set; }

        public List<UiWindow> UiWindows { get; set; }
        public string Value { get; set; }

        public UiWindow UiParentWindow => UiWindows.FirstOrDefault();

        public bool HasChildrenWindows
        {
            get
            {
                if (UiWindows.Count > 1)
                {
                    return true;
                }

                return false;
            }
        }

        public override string ToString()
        {
            if (HasChildrenWindows)
            {
                return $"{UiParentWindow.UiElement} | {UiWindows.Last().UiElement}";
            }

            return $"{UiParentWindow.UiElement}";
        }
    }
}