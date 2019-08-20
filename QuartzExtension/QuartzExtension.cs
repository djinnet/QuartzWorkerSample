using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;
using Quartz.Logging;
using Microsoft.Extensions.Logging;

namespace QuartzExtension
{
    public static class QuartzExtension
    {
        /// <summary>
        /// Add Scheduler to the DI and add quartz to the logging
        /// </summary>
        /// <param name="services"></param>
        /// <returns>Add to the logging</returns>
        public static ILoggingBuilder AddQuartzLogging(this ILoggingBuilder services)
        {
            try
            {
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };

                StdSchedulerFactory factory = new StdSchedulerFactory(props);


                //add to service
                IScheduler scheduler = factory.GetScheduler().GetAwaiter().GetResult();
                services.Services.AddSingleton(scheduler);

                //Set logging
                LogProvider.SetCurrentLogProvider(new IloggerProvider());

                return services;
            }
            catch (SchedulerException se)
            {
                Console.Error.WriteLine(se.ToString());
                throw;
            }
        }
    }
}
