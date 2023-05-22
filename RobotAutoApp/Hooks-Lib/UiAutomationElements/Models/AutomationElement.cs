using UIAutomationClient;
using WinAppDriver.Generation.UiElements.Extensions;
using WinAppDriver.Generation.UiElements.Models;

namespace WinAppDriver.Generation.UiAutomationElements.Models
{
    /// <summary>
    ///     Supporting class for IUIAutomationElement and UiElement
    /// </summary>
    /// <remarks>
    ///     This isn't necessary but allows you to see which Automation Element Pointer your dealing w/
    /// </remarks>
    public class AutomationElement
    {
        public AutomationElement(IUIAutomationElement element)
        {
            IUIAutomationElement = element;

            if (IUIAutomationElement != null)
            {
                UiElement = element.GetUiElementByIUIAutomationElement();
            }
        }

        public IUIAutomationElement IUIAutomationElement { get; set; }

        public UiElement UiElement { get; set; }

        public bool HasUiElement => UiElement != null;
    }
}