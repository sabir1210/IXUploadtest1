using OpenQA.Selenium.Appium.Windows;
using WinAppDriver.Generation.States;
using WinAppDriver.Generation.UiElements.Models;
using WinAppDriver.Generation.UiEvents.Models;
using WinAppDriver.Generation.WindowsDrivers.Extensions;

namespace WinAppDriver.Generation.PlaybackEvents.Models
{
    /// <summary>
    ///     Leveraged in Playback Event Extensions and requires an UiEvent
    /// </summary>
    public class PlaybackEvent
    {
        public PlaybackEvent(UiEvent uiEvent)
        {
            UiEvent = uiEvent;

            InitializeAutomationElementWindows();
            InitializeAutomationElement();
        }

        /// <summary>
        ///     Precursor object required to setup and apply information for Playback Event
        /// </summary>
        public UiEvent UiEvent { get; set; }

        /// <summary>
        /// UiElement - WindowsElement
        /// </summary>
        public WindowsElement WindowsElement { get; set; }

        /// <summary>
        /// UiElement.ParentWindow - WindowsElement
        /// </summary>
        private WindowsElement WindowsElementWindow { get; set; }

        /// <summary>
        /// UiElement.ParentWindow Driver || Desktop Driver
        /// </summary>
        public WindowsDriver<WindowsElement> WindowsDriver { get; set; }

        /// <summary>
        ///     Initializes and associates windows
        /// </summary>
        private void InitializeAutomationElementWindows()
        {
            WindowsElementWindow = WindowsElementsExtensions.GetFirstElementByCondition(WindowsApplicationDriverState.WindowsApplicationDriverRootDriver, UiEvent.UiParentWindow.UiElement);

            if (UiEvent.UiParentWindow.UiElement.LocalizedControlType == LocalizedControlTypes.Window && WindowsElementWindow != null)
            {
                WindowsDriver = WindowsDriversExtensions.GetDriverByUiElement(WindowsApplicationDriverState.WindowsApplicationDriverRootDriver, UiEvent.UiParentWindow.UiElement);
            }
            else
            {
                WindowsDriver = WindowsApplicationDriverState.WindowsApplicationDriverRootDriver;
            }
        }

        /// <summary>
        ///     Initializes and associates the targeted element
        /// </summary>
        private void InitializeAutomationElement()
        {
            WindowsElement = WindowsElementsExtensions.GetAllElementByCondition(WindowsDriver, UiEvent.UiElement);
        }
    }
}