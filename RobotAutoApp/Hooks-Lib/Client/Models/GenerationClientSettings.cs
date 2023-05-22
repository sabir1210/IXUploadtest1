using System;

namespace WinAppDriver.Generation.Client.Models
{
    public class GenerationClientSettings
    {
        public int ProcessId { get; set; }

        public TimeSpan AutomationTransactionTimeout { get; set; }
    }
}