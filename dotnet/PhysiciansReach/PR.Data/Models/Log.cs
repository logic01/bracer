using System;

namespace PR.Data.Models
{
    public class Log
    {
        public enum LogSeverity
        {
            Info = 0,
            Warning = 1,
            Error = 2
        }

        public int LogId { get; set; }

        public LogSeverity Severity { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
