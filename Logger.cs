using System;
using Microsoft.Extensions.Logging;
namespace FlagApi
{
    public class Logger : ILogger
    {
        private void Info(object message){
            Console.ForegroundColor = ConsoleColor.Green;
            Print(message);
        }
        private void Warn(object message){
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Print(message);
        }
        public static void Error(object message){
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Print(message);
        }
        private static void Print(object message){
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public IDisposable BeginScope<TState>(TState state) => null;
        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, 
            EventId eventId, 
            TState state,
            Exception exception, 
            Func<TState, Exception, string> formatter)
        {            
            switch(logLevel){                
                case LogLevel.Error: 
                    Error(formatter(state, exception));
                    break;
                case LogLevel.Debug: 
                    Warn(formatter(state, exception));
                    break;
                default:                
                    Info(formatter(state, exception));
                    break;
            }
            
        }
    }
}