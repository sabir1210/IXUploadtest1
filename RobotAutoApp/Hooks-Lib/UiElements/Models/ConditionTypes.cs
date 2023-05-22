﻿using System;

namespace WinAppDriver.Generation.UiElements.Models
{
    /// <summary>
    ///     Condition Type Enumeration
    /// </summary>
    /// <remarks>
    ///     Used in UiAutomationElementExtensions - FindAllElementByCondition
    /// </remarks>
    [Flags]
    public enum ConditionTypes
    {
        Default = 0,

        AutomationId = 1 << 0,

        Name = 1 << 1,

        ClassName = 1 << 2,

        ElementType = 1 << 3
    }
}