using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using WinAppDriver.Generation.Events.Keyboard;
using WinAppDriver.Generation.Events.Keyboard.Models;
using WinAppDriver.Generation.Events.Mouse.Models;
using WinAppDriver.Generation.PlaybackEvents.Models;
using WinAppDriver.Generation.UiEvents.Models;

namespace WinAppDriver.Generation.PlaybackEvents.Extensions
{
    public static class PlaybackEventExtensions
    {
        public static void Execute(this PlaybackEvent playbackEvent)
        {
            switch (playbackEvent.UiEvent.UiEventType)
            {
                case UiEventTypes.Keyboard_SendKeys:
                    KeyboardSendKeys(playbackEvent.WindowsElement, playbackEvent.UiEvent.KeyboardEvent);
                    break;

                case UiEventTypes.Mouse_LeftClick:
                    MouseLeftClick(playbackEvent.WindowsElement);
                    break;

                case UiEventTypes.Mouse_RightClick:
                    MouseRightClick(playbackEvent.WindowsDriver, playbackEvent.WindowsElement);
                    break;

                case UiEventTypes.Mouse_LeftDoubleClick:
                    MouseLeftDoubleClick(playbackEvent.WindowsDriver, playbackEvent.WindowsElement);
                    break;

                case UiEventTypes.Mouse_DragStop:
                    MouseDrag(playbackEvent.WindowsDriver, playbackEvent.WindowsElement, playbackEvent.UiEvent.MouseEvent);
                    break;

                case UiEventTypes.Mouse_Wheel:
                    MouseWheel(playbackEvent.WindowsDriver, playbackEvent.WindowsElement, playbackEvent.UiEvent.MouseEvent);
                    break;

                default:
                    throw new Exception("Playback has an event that is not handled.");
            }
        }

        /// <summary>
        /// Sends a Left Click
        /// </summary>
        /// <param name="element"></param>
        private static void MouseLeftClick(WindowsElement element)
        {
            element.Click();
        }

        /// <summary>
        /// Sends a Left Double Click
        /// </summary>
        /// <param name="windowsDriver"></param>
        /// <param name="element"></param>
        private static void MouseLeftDoubleClick(WindowsDriver<WindowsElement> windowsDriver, WindowsElement element)
        {
            //windowsDriver.Mouse.MouseMove(element.Coordinates);
            //windowsDriver.Mouse.DoubleClick(null);
        }

        /// <summary>
        /// Sends a Right Click
        /// </summary>
        /// <param name="windowsDriver"></param>
        /// <param name="element"></param>
        private static void MouseRightClick(WindowsDriver<WindowsElement> windowsDriver, WindowsElement element)
        {
            //windowsDriver.Mouse.MouseMove(element.Coordinates);
            //windowsDriver.Mouse.ContextClick(null);
        }

        /// <summary>
        /// Sends a Mouse Drag
        /// </summary>
        /// <param name="windowsDriver"></param>
        /// <param name="element"></param>
        /// <param name="mouseEvent"></param>
        private static void MouseDrag(WindowsDriver<WindowsElement> windowsDriver, WindowsElement element, MouseEvent mouseEvent)
        {
        //    windowsDriver.Mouse.MouseMove(element.Coordinates);
        //    windowsDriver.Mouse.MouseDown(null);
        //    windowsDriver.Mouse.MouseMove(element.Coordinates, mouseEvent.DragPosition.X, mouseEvent.DragPosition.Y);
        //    windowsDriver.Mouse.MouseUp(null);
            Thread.Sleep(TimeSpan.FromMilliseconds(250));
        }

        /// <summary>
        /// Sends a Mouse Scroll Wheel
        /// </summary>
        /// <param name="windowsDriver"></param>
        /// <param name="element"></param>
        /// <param name="mouseEvent"></param>
        private static void MouseWheel(WindowsDriver<WindowsElement> windowsDriver, WindowsElement element, MouseEvent mouseEvent)
        {
            var remoteTouchScreen = new RemoteTouchScreen(windowsDriver);
            remoteTouchScreen.Scroll(element.Coordinates, 0, mouseEvent.Wheel);
        }

        /// <summary>
        /// Sends Keyboard Keys
        /// </summary>
        /// <param name="element"></param>
        /// <param name="keyboardEvent"></param>
        /// <remarks>
        /// Current implementation does not account for CAPS LOCK / NUM LOCK
        /// </remarks>
        private static void KeyboardSendKeys(WindowsElement element, KeyboardEvent keyboardEvent)
        {
            var lastControlKey = KeyboardVirtualKeys.DEFAULT;

            foreach (var keyPress in keyboardEvent.KeyPresses)
            {
                var isControlKey = KeyboardConstants.IsControlSendKey(keyPress.VirtualKey);

                if (isControlKey && keyPress.IsDown)
                {
                    lastControlKey = keyPress.VirtualKey;
                }
                else if (isControlKey)
                {
                    lastControlKey = KeyboardVirtualKeys.DEFAULT;
                }
                else if (keyPress.IsDown)
                {
                    if (KeyboardConstants.IsSelenimumSendKey(keyPress.VirtualKey))
                    {
                        SendSeleniumKey(element, keyPress);
                    }
                    else
                    {
                        SendVirtualKey(element, keyPress, lastControlKey);
                    }
                }

                Thread.Sleep(30);
            }
        }

        /// <summary>
        ///     Sends Keyboard Key + Special Keys (Shift, Control)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="lastControlKey"></param>
        private static void SendVirtualKey(WindowsElement element, KeyboardKeyPress key, KeyboardVirtualKeys lastControlKey)
        {
            var hasVirtualKeyPair = KeyboardConstants.VirtualKeyPairs.ContainsKey((int)key.VirtualKey);

            if (!hasVirtualKeyPair)
            {
                return;
            }

            var virtualKeyPairValue = KeyboardConstants.VirtualKeyPairs[(int)key.VirtualKey];

            switch (lastControlKey)
            {
                case KeyboardVirtualKeys.VK_LSHIFT:
                case KeyboardVirtualKeys.VK_RSHIFT:
                case KeyboardVirtualKeys.VK_SHIFT:

                    element.SendKeys(Keys.Shift + virtualKeyPairValue.Default);

                    break;

                case KeyboardVirtualKeys.VK_LCONTROL:
                case KeyboardVirtualKeys.VK_RCONTROL:
                case KeyboardVirtualKeys.VK_CONTROL:

                    element.SendKeys(Keys.Control + virtualKeyPairValue.Default);
                    break;

                default:

                    element.SendKeys(virtualKeyPairValue.Default);
                    break;
            }
        }

        /// <summary>
        /// Sends special Keys.????
        /// </summary>
        /// <param name="element"></param>
        /// <param name="keyPress"></param>
        private static void SendSeleniumKey(WindowsElement element, KeyboardKeyPress keyPress)
        {
            switch (keyPress.VirtualKey)
            {
                case KeyboardVirtualKeys.VK_SPACE:
                    element.SendKeys(Keys.Space);
                    break;

                case KeyboardVirtualKeys.VK_RETURN:
                    element.SendKeys(Keys.Return);
                    break;

                case KeyboardVirtualKeys.VK_TAB:
                    element.SendKeys(Keys.Tab);
                    break;

                case KeyboardVirtualKeys.VK_BACK:
                    element.SendKeys(Keys.Backspace);
                    break;
            }
        }
    }
}