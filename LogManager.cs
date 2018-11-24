using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace LoggerLib
{
    public static class LogManager
    {
        private static List<Logger> Loggers { get; set; }
        public static ILogger GetLogger(string name)
        {
            Logger logger = new Logger(name);
            // check if logger with name exist
            // if absent add
            Loggers.Add(logger);
            return logger;             
        }

        public static void ConfigureLogger(string name, LogConfiguration configuration)
        {
            // Get Logger with name from list
            // Assume its index 0
            Loggers[0].Configuration = configuration;                    
        }
    }

    public interface ILogger
    {
        void Info(object message);
        void Debug(object message);
    }

    public class Logger: ILogger
    {
        private string name;
        public LogConfiguration Configuration { get; set; }

        public Logger(string name)
        {
            this.name = name;
            
        }

        private List<object> messages;

        public void Debug(object message)
        {
            // if lower than current level
            messages.Add(message);
        }

        public void Info(object message)
        {
            // if lower than current level
            messages.Add(message);
        }

        private void DispatchMessages()
        {
                  
        }
        

    }

    public enum LogLevel
    {
        Info,
        Debug
    }

    public class LogConfiguration
    {
        public LogLevel  Level{ get; set; }
        public List<Appender> Appenders { get; set; }

    }

    public abstract class Appender
    {
        public string Name { get; set; }
        public List<object> Messages { get; set; }
        public abstract void Write(object message);
    }

    public class ConsoleAppender: Appender
    {
        public override void Write(object message)
        {
            Messages.Add(message);    
        }
        
    }

}
