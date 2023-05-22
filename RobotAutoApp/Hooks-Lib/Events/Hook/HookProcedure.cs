using System;

namespace WinAppDriver.Generation.Events.Hook
{
    internal delegate IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam);
}