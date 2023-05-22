using System;
using System.Collections.Generic;
using UIAutomationClient;

namespace WinAppDriver.Generation.UiAutomationElements.Extensions
{
    /// <summary>
    ///     Supporting methods in regards to UiAutomationCom && AutomationElements
    /// </summary>
    public static class UiAutomationElementConditionExtensions
    {
        /// <summary>
        ///     Returns related windows
        /// </summary>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        public static List<IUIAutomationElement> GetElementWindows(this IUIAutomationElement automationElement)
        {
            var elementWindows = new List<IUIAutomationElement>();
            try
            {


                var targetAutomationElement = automationElement;

                while (true)
                {
                    var parentAutomationElement = UiAutomationCom.IUIAutomationTreeWalker.GetParentElement(targetAutomationElement);

                    if (parentAutomationElement == null)
                    {
                        break;
                    }

                    elementWindows.Insert(0, parentAutomationElement);

                    var result = UiAutomationCom.CUIAutomation.CompareElements(parentAutomationElement,
                        UiAutomationCom.RootAutomationElement);

                    if (result == 1)
                    {
                        break;
                    }

                    targetAutomationElement = parentAutomationElement;
                }
            }
            catch (Exception ex) { }
            return elementWindows;
        }
    }
}