using PR.Constants.Enums;

namespace PR.Business.Interfaces
{
    public interface ILoggingBusiness
    {
        void Log(LogSeverity severity, string message, string stacktrace = "");
    }
}
