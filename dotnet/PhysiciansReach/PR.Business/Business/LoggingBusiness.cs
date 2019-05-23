using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Data.Models;
using System;

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
            try
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
            catch (Exception ex)
            {
                var e = ex;
            }
        }
    }
}
