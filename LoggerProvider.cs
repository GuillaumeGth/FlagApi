using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace FlagApi
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly List<ILogger> loggers = new List<ILogger>();

        public ILogger CreateLogger(string categoryName)
        {
            var logger = new Logger();
            loggers.Add(logger);
            return logger;
        }

        public void Dispose() => loggers.Clear();
    }
}