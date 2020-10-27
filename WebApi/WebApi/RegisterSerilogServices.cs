// <copyright file="RegisterSerilogServices.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Serilog.Injection
{
    using System;
    using System.IO;
    using Microsoft.Extensions.DependencyInjection;

    public static class RegisterSerilogServices
    {
        /// <summary>
        /// Register the Serilog service with a custom configuration.
        /// </summary>
        public static IServiceCollection AddSerilogServices(this IServiceCollection services, LoggerConfiguration configuration)
        {
            Log.Logger = configuration.CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
            return services.AddSingleton(Log.Logger);
        }

        /// <summary>
        /// Register the Serilog service for console logging only.
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddSerilogServices(this IServiceCollection services)
        {
            return services.AddSerilogServices(
                new LoggerConfiguration()

                    // .MinimumLevel.Verbose()
                    // .WriteTo.Console()
                    .WriteTo.Seq("http://localhost:5341")
                    .MinimumLevel.Debug()
                    .WriteTo.RollingFile(Path.Combine(Directory.GetCurrentDirectory(), "log-{Date}.txt")));
        }
    }
}