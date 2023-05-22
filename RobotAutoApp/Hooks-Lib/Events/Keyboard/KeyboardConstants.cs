using System.Collections.Generic;
using WinAppDriver.Generation.Events.Keyboard.Models;

namespace WinAppDriver.Generation.Events.Keyboard
{
    /// <summary>
    ///     Keyboard support methods in regards to virtual keys
    /// </summary>
    public static class KeyboardConstants
    {
        internal static Dictionary<int, KeyboardKeyPair> VirtualKeyPairs = new Dictionary<int, KeyboardKeyPair>();

        /// <summary>
        ///     Initializes Virtual Key Pairs
        /// </summary>
        /// <remarks>
        ///     Binds Virtual Keys to corresponding English characters
        /// </remarks>
        internal static void Initialize()
        {
            if (VirtualKeyPairs.Count > 0)
            {
                return;
            }

            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_SPACE, new KeyboardKeyPair(" ", " "));

            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_NUMPAD0, new KeyboardKeyPair("0", "0"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_NUMPAD1, new KeyboardKeyPair("1", "1"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_NUMPAD2, new KeyboardKeyPair("2", "2"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_NUMPAD3, new KeyboardKeyPair("3", "3"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_NUMPAD4, new KeyboardKeyPair("4", "4"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_NUMPAD5, new KeyboardKeyPair("5", "5"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_NUMPAD6, new KeyboardKeyPair("6", "6"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_NUMPAD7, new KeyboardKeyPair("7", "7"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_NUMPAD8, new KeyboardKeyPair("8", "8"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_NUMPAD9, new KeyboardKeyPair("9", "9"));

            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_0, new KeyboardKeyPair("0", ")"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_1, new KeyboardKeyPair("1", "!"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_2, new KeyboardKeyPair("2", "@"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_3, new KeyboardKeyPair("3", "#"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_4, new KeyboardKeyPair("4", "$"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_5, new KeyboardKeyPair("5", "%"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_6, new KeyboardKeyPair("6", "^"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_7, new KeyboardKeyPair("7", "&"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_8, new KeyboardKeyPair("8", "*"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_9, new KeyboardKeyPair("9", "("));

            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_OEM_3, new KeyboardKeyPair("`", "~"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_OEM_MINUS, new KeyboardKeyPair("-", "_"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_OEM_PLUS, new KeyboardKeyPair("=", "+"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_OEM_4, new KeyboardKeyPair("[", "{"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_OEM_6, new KeyboardKeyPair("]", "}"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_OEM_5, new KeyboardKeyPair("\\", "|"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_OEM_1, new KeyboardKeyPair(";", ":"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_OEM_7, new KeyboardKeyPair("'", "\""));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_OEM_PERIOD, new KeyboardKeyPair(".", ">"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_OEM_2, new KeyboardKeyPair("/", "?"));

            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_A, new KeyboardKeyPair("a", "A"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_B, new KeyboardKeyPair("b", "B"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_C, new KeyboardKeyPair("c", "C"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_D, new KeyboardKeyPair("d", "D"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_E, new KeyboardKeyPair("e", "E"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_F, new KeyboardKeyPair("f", "F"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_G, new KeyboardKeyPair("g", "G"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_H, new KeyboardKeyPair("h", "H"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_I, new KeyboardKeyPair("i", "I"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_J, new KeyboardKeyPair("j", "J"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_K, new KeyboardKeyPair("k", "K"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_L, new KeyboardKeyPair("l", "L"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_M, new KeyboardKeyPair("m", "M"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_N, new KeyboardKeyPair("n", "N"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_O, new KeyboardKeyPair("o", "O"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_P, new KeyboardKeyPair("p", "P"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_Q, new KeyboardKeyPair("q", "Q"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_R, new KeyboardKeyPair("r", "R"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_S, new KeyboardKeyPair("s", "S"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_T, new KeyboardKeyPair("t", "T"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_U, new KeyboardKeyPair("u", "U"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_V, new KeyboardKeyPair("v", "V"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_W, new KeyboardKeyPair("w", "W"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_X, new KeyboardKeyPair("x", "X"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_Y, new KeyboardKeyPair("y", "Y"));
            VirtualKeyPairs.Add((int)KeyboardVirtualKeys.VK_Z, new KeyboardKeyPair("z", "Z"));
        }

        /// <summary>
        ///     Leveraged in HookEventHandler for the purpose of adding IsDown = True|False
        /// </summary>
        /// <param name="virtualKey"></param>
        /// <returns></returns>
        internal static bool IsControlSendKey(KeyboardVirtualKeys virtualKey)
        {
            switch (virtualKey)
            {
                case KeyboardVirtualKeys.VK_LSHIFT:
                case KeyboardVirtualKeys.VK_RSHIFT:
                case KeyboardVirtualKeys.VK_SHIFT:
                case KeyboardVirtualKeys.VK_LCONTROL:
                case KeyboardVirtualKeys.VK_RCONTROL:
                case KeyboardVirtualKeys.VK_CONTROL:

                    return true;
            }

            return false;
        }

        /// <summary>
        /// Leveraged in PlaybackEventExtensions to determine if it requires a special send key
        /// </summary>
        /// <param name="virtualKey"></param>
        /// <returns></returns>
        internal static bool IsSelenimumSendKey(KeyboardVirtualKeys virtualKey)
        {
            switch (virtualKey)
            {
                case KeyboardVirtualKeys.VK_SPACE:
                case KeyboardVirtualKeys.VK_RETURN:
                case KeyboardVirtualKeys.VK_TAB:
                case KeyboardVirtualKeys.VK_BACK:

                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Leveraged in GetKeyboardDescription for the purpose of creating a description to the end user
        /// </summary>
        /// <param name="virtualKey"></param>
        /// <returns></returns>
        private static bool IsControlDescriptionKey(KeyboardVirtualKeys virtualKey)
        {
            switch (virtualKey)
            {
                case KeyboardVirtualKeys.VK_LSHIFT:
                case KeyboardVirtualKeys.VK_RSHIFT:
                case KeyboardVirtualKeys.VK_SHIFT:
                case KeyboardVirtualKeys.VK_CAPITAL:

                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Creates an English description of the Keyboard KeyPresses
        /// </summary>
        /// <remarks>
        ///     This generates successfully in most scenarios but does not cover all cases
        /// </remarks>
        /// <param name="keyPresses"></param>
        /// <returns></returns>
        public static string GetKeyboardDescription(List<KeyboardKeyPress> keyPresses)
        {
            var buildDescription = string.Empty;
            var lastControlKey = KeyboardVirtualKeys.DEFAULT;

            foreach (var keyPress in keyPresses)
            {
                var isControlKey = IsControlDescriptionKey(keyPress.VirtualKey);

                if (isControlKey && keyPress.IsDown)
                {
                    if (lastControlKey != keyPress.VirtualKey)
                    {
                        lastControlKey = keyPress.VirtualKey;
                    }
                    else
                    {
                        lastControlKey = KeyboardVirtualKeys.DEFAULT;
                    }
                }
                else if (isControlKey)
                {
                    lastControlKey = KeyboardVirtualKeys.DEFAULT;
                }
                else if (keyPress.IsDown)
                {
                    switch (lastControlKey)
                    {
                        case KeyboardVirtualKeys.VK_LSHIFT:
                        case KeyboardVirtualKeys.VK_RSHIFT:
                        case KeyboardVirtualKeys.VK_SHIFT:

                            if (VirtualKeyPairs.ContainsKey((int)keyPress.VirtualKey))
                            {
                                var keyPair = VirtualKeyPairs[(int)keyPress.VirtualKey];
                                buildDescription += $"{keyPair.Special}";
                            }

                            break;

                        case KeyboardVirtualKeys.VK_CAPITAL:

                            if (VirtualKeyPairs.ContainsKey((int)keyPress.VirtualKey))
                            {
                                var keyPair = VirtualKeyPairs[(int)keyPress.VirtualKey];

                                if (keyPress.VirtualKey >= KeyboardVirtualKeys.VK_A &&
                                    keyPress.VirtualKey <= KeyboardVirtualKeys.VK_Z)
                                {
                                    buildDescription += keyPair.Special;
                                }
                                else
                                {
                                    buildDescription += keyPair.Default;
                                }
                            }

                            break;

                        default:

                            if (VirtualKeyPairs.ContainsKey((int)keyPress.VirtualKey))
                            {
                                var keyPair = VirtualKeyPairs[(int)keyPress.VirtualKey];
                                buildDescription += keyPair.Default;
                            }
                            else
                            {
                                switch (keyPress.VirtualKey)
                                {
                                    case KeyboardVirtualKeys.VK_DELETE:
                                    case KeyboardVirtualKeys.VK_BACK:

                                        if (buildDescription.Length > 0)
                                        {
                                            buildDescription =
                                                buildDescription.Substring(0, buildDescription.Length - 1);
                                        }

                                        break;
                                }
                            }

                            break;
                    }
                }
            }

            if (keyPresses.Count > 0 && string.IsNullOrWhiteSpace(buildDescription))
            {
                buildDescription += "{ Review Control Keys }";
            }

            return buildDescription;
        }
    }
}