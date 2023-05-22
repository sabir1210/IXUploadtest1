using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using WinAppDriver.Generation.UiElements.Models;

namespace WinAppDriver.Generation.WindowsDrivers.Extensions
{
    public static class WindowsElementsExtensions
    {
        /// <summary>
        /// Returns the first Windows Element that best fits the conditions
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        internal static WindowsElement GetFirstElementByCondition(WindowsDriver<WindowsElement> driver, UiElement uiElement)
        {
            WindowsElement conditionElement = null;

            if (uiElement.ConditionType.HasFlag(ConditionTypes.AutomationId))
            {
                conditionElement = GetElementByAutomationId(driver, uiElement);
            }

            if (uiElement.ConditionType.HasFlag(ConditionTypes.Name) && conditionElement == null)
            {
                conditionElement = GetElementByName(driver, uiElement);
            }

            if (uiElement.ConditionType.HasFlag(ConditionTypes.ClassName) && conditionElement == null)
            {
                conditionElement = GetElementByClassName(driver, uiElement);
            }

            return conditionElement;
        }

        /// <summary>
        /// Returns a Windows Element if only one element is found that best fits the conditions
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        internal static WindowsElement GetAllElementByCondition(WindowsDriver<WindowsElement> driver, UiElement uiElement)
        {
            WindowsElement conditionElement = null;
            ICollection<WindowsElement> conditionElements = FindAllElementByCondition(driver, uiElement);

            // No elements found
            if (conditionElements != null)
            {
                // Single element returned (best case scenario)
                if (conditionElements.Count == 1)
                {
                    conditionElement = conditionElements.SingleOrDefault();
                }
                else if (conditionElements.Count > 1)
                {
                    throw new Exception("UiElement Failed | Windows Application Driver Error ");
                }
            }

            return conditionElement;
        }

        private static ICollection<WindowsElement> FindAllElementByCondition(WindowsDriver<WindowsElement> driver, UiElement uiElement)
        {
            ICollection<WindowsElement> conditionElements = new List<WindowsElement>();

            if (uiElement.ConditionType.HasFlag(ConditionTypes.AutomationId))
            {
                conditionElements = GetElementsByAutomationId(driver, uiElement);
            }

            if (uiElement.ConditionType.HasFlag(ConditionTypes.Name) && conditionElements.Count == 0)
            {
                conditionElements = GetElementsByName(driver, uiElement);
            }

            if (uiElement.ConditionType.HasFlag(ConditionTypes.ClassName) && conditionElements.Count == 0)
            {
                conditionElements = GetElementsByClassName(driver, uiElement);
            }

            if (uiElement.ConditionType.HasFlag(ConditionTypes.ElementType) && conditionElements.Count == 0)
            {
                conditionElements = GetElementsByElementType(driver, uiElement);
            }

            return conditionElements;
        }

        private static WindowsElement GetElementByAutomationId(WindowsDriver<WindowsElement> driver, UiElement uiElement)
        {
            WindowsElement windowsElement = null;

            try
            {
                windowsElement = driver.FindElementByAccessibilityId(uiElement.AutomationId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return windowsElement;
        }

        private static ICollection<WindowsElement> GetElementsByAutomationId(WindowsDriver<WindowsElement> driver, UiElement uiElement)
        {
            List<WindowsElement> windowsElements = new List<WindowsElement>();

            try
            {
                windowsElements = driver.FindElementsByAccessibilityId(uiElement.AutomationId).ToList();
                windowsElements = windowsElements.Where(c => c.Displayed == true).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return windowsElements;
        }

        private static WindowsElement GetElementByName(WindowsDriver<WindowsElement> driver, UiElement uiElement)
        {
            WindowsElement windowsElement = null;

            try
            {
                windowsElement = driver.FindElementByName(uiElement.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return windowsElement;
        }

        private static ICollection<WindowsElement> GetElementsByName(WindowsDriver<WindowsElement> driver, UiElement uiElement)
        {
            List<WindowsElement> windowsElements = new List<WindowsElement>();

            try
            {
                windowsElements = driver.FindElementsByName(uiElement.Name).ToList();
                windowsElements = windowsElements.Where(c => c.Displayed == true).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return windowsElements;
        }

        private static WindowsElement GetElementByClassName(WindowsDriver<WindowsElement> driver, UiElement uiElement)
        {
            WindowsElement windowsElement = null;

            try
            {
                windowsElement = driver.FindElementByClassName(uiElement.ClassName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return windowsElement;
        }

        private static ICollection<WindowsElement> GetElementsByClassName(WindowsDriver<WindowsElement> driver, UiElement uiElement)
        {
            List<WindowsElement> windowsElements = new List<WindowsElement>();

            try
            {
                windowsElements = driver.FindElementsByClassName(uiElement.ClassName).ToList();
                windowsElements = windowsElements.Where(c => c.Displayed == true).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return windowsElements;
        }

        private static ICollection<WindowsElement> GetElementsByElementType(WindowsDriver<WindowsElement> driver, UiElement uiElement)
        {
            List<WindowsElement> windowsElements = new List<WindowsElement>();

            try
            {
                windowsElements = driver.FindElementsByTagName(uiElement.LocalizedControlType.ToString()).ToList();
                windowsElements = windowsElements.Where(c => c.Displayed == true).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return windowsElements;
        }
    }
}