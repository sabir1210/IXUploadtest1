using System;
using UIAutomationClient;
using WinAppDriver.Generation.UiElements.Models;

namespace WinAppDriver.Generation.UiElements.Extensions
{
    /// <summary>
    ///     Supporting methods for UiElement
    /// </summary>
    /// <remarks>
    ///     Could be part of UiElement class but i decided to keep the model simple
    /// </remarks>
    public static class UiElementExtensions
    {
        /// <summary>
        ///     Automation COM to UiElement
        /// </summary>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        public static UiElement GetUiElementByIUIAutomationElement(this IUIAutomationElement automationElement)
        {
            var element = new UiElement();
            try
            {
                if (automationElement != null)
                {
                    element = new UiElement
                    {
                        LocalizedControl = automationElement.CurrentLocalizedControlType,
                        ClassName = automationElement.CurrentClassName,
                        Name = automationElement.CurrentName,
                        AutomationId = automationElement.CurrentAutomationId,
                        Value = (automationElement.GetCurrentPropertyValue(30045) as string).Trim()
                    };

                    var elementTagRectangle = automationElement.CurrentBoundingRectangle;

                    element.X = elementTagRectangle.left;
                    element.Y = elementTagRectangle.top;
                    element.Width = elementTagRectangle.right - elementTagRectangle.left;
                    element.Height = elementTagRectangle.bottom - elementTagRectangle.top;
                }
            }
            catch (Exception ex) {
            // TODO
            }
            return element;
        }

        /// <summary>
        ///     Builds ToString for UiElement
        /// </summary>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        public static string GetUiElementToString(this UiElement uiElement)
        {
            var buildName = string.Empty;

            if (!string.IsNullOrWhiteSpace(uiElement.Name))
            {
                buildName += $"{uiElement.Name}";
            }

            if (!string.IsNullOrWhiteSpace(uiElement.Name) && !string.IsNullOrWhiteSpace(uiElement.AutomationId))
            {
                buildName += ".";
            }

            if (!string.IsNullOrWhiteSpace(uiElement.AutomationId))
            {
                buildName += $"{uiElement.AutomationId}";
            }

            if (string.IsNullOrWhiteSpace(uiElement.Name) && string.IsNullOrWhiteSpace(uiElement.AutomationId))
            {
                buildName += $"{uiElement.ClassName}";
            }

            return buildName;
        }
    }
}