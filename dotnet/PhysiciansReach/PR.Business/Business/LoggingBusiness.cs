using PR.Business.Interfaces;
using PR.Data.Models;
using System;
using static PR.Data.Models.Log;

namespace PR.Business.Business
{
    public class LoggingBusiness : ILoggingBusiness
    {
        private DataContext _context;

        public LoggingBusiness(DataContext context)
        {
            _context = context;
        }

        public void Log(LogSeverity severity, string message, string stacktrace = "")
        {
            var log = new Log
            {
                Message = message,
                StackTrace = stacktrace,
                Severity = severity,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };

            _context.Log.Add(log);
            _context.SaveChanges();
        }
    }
}
