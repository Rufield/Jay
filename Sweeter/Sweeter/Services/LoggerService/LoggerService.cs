using Microsoft.Extensions.Logging;
using System;

namespace Sweeter.Services.LoggerService
{
    public class LoggerService : ILogger
    {
        private ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                _logger.Log(logLevel, eventId, state, exception, formatter);
            }
        }
    }
}
