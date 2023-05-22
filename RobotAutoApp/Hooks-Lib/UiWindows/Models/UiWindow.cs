using System.Collections.Generic;
using System.ComponentModel;
using WinAppDriver.Generation.UiElements.Extensions;
using WinAppDriver.Generation.UiElements.Models;

namespace WinAppDriver.Generation.UiWindows.Models
{
    /// <summary>
    ///     Summary of related information regarding the UiElement [Which is the UiWindow]
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class UiWindow
    {
        public UiWindow()
        {
            UiWindowElements = new List<UiElement>();
        }

        public UiWindow(UiElement uiElement)
        {
            UiElement = uiElement;

            UiWindowElements = new List<UiElement>();
        }

        public UiElement UiElement { get; set; }

        /// <summary>
        ///     List of elements associated with the UiWindow
        /// </summary>
        public List<UiElement> UiWindowElements { get; set; }

        public override string ToString()
        {
            return $"{UiElement.GetUiElementToString()}";
        }
    }
}