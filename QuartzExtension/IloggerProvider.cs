using Microsoft.Extensions.Logging;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuartzExtension
{
    public class IloggerProvider : ILogProvider
    {

        private readonly ILogger Logger;

        public IloggerProvider()
        {

        }

        public IloggerProvider(ILogger logger)
        {
            Logger = logger;
        }
        

        public Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if(level >= Quartz.Logging.LogLevel.Info && func != null)
                {
                    //this should show up
                    Console.WriteLine($"[{ DateTime.Now.ToLongTimeString() }] [{ level }]" + func(), parameters);
                    //Logging(level, func, exception, parameters);
                }
                return true;
            };
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }

        private void Logging(Quartz.Logging.LogLevel level, Func<string> func, Exception exception, object[] parameters)
        {
            
            switch (level)
            {
                case Quartz.Logging.LogLevel.Trace:
                    
                    this.Logger.Log(Microsoft.Extensions.Logging.LogLevel.Trace, $"[{ DateTime.Now.ToLongTimeString() }] [{ level }] [{exception.Message}]" + func(), parameters);
                    break;
                case Quartz.Logging.LogLevel.Debug:
                    this.Logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, $"[{ DateTime.Now.ToLongTimeString() }] [{ level }] [{exception.Message}]" + func(), parameters);
                    break;
                case Quartz.Logging.LogLevel.Info:
                    this.Logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, $"[{ DateTime.Now.ToLongTimeString() }] [{ level }] [{exception.Message}]" + func(), parameters);
                    break;
                case Quartz.Logging.LogLevel.Warn:
                    this.Logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, $"[{ DateTime.Now.ToLongTimeString() }] [{ level }] [{exception.Message}]" + func(), parameters);
                    break;
                case Quartz.Logging.LogLevel.Error:
                    this.Logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, $"[{ DateTime.Now.ToLongTimeString() }] [{ level }] [{exception.Message}]" + func(), parameters);
                    break;
                case Quartz.Logging.LogLevel.Fatal:
                    this.Logger.Log(Microsoft.Extensions.Logging.LogLevel.Critical, $"[{ DateTime.Now.ToLongTimeString() }] [{ level }] [{exception.Message}]" + func(), parameters);
                    break;
                default:
                    throw new ArgumentNullException();
            }
        }
    }
}
