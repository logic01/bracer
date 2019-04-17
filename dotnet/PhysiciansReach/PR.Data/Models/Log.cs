using PR.Constants.Enums;
using System;

namespace PR.Data.Models
{
    public class Log
    {
        public int LogId { get; set; }

        public LogSeverity Severity { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
