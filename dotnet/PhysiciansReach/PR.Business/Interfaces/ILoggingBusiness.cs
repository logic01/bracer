using static PR.Data.Models.Log;

namespace PR.Business.Interfaces
{
    public interface ILoggingBusiness
    {
        void Log(LogSeverity severity, string message, string stacktrace = "");
    }
}
