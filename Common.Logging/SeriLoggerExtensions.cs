using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Common.Logging
{
    public static class SeriLoggerExtensions
    {
        /// <summary>
        /// Extension to configure Serilog.
        /// </summary>
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
           (context, configuration) =>
           {
               configuration
                    .MinimumLevel.Debug()
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                    .ReadFrom.Configuration(context.Configuration);
           };
    }
}
