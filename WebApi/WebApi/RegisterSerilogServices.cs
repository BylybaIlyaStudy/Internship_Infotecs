// <copyright file="RegisterSerilogServices.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

namespace Serilog.Injection
{
    using System;
    using System.IO;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Класс подключения serilog.
    /// </summary>
    public static class RegisterSerilogServices
    {
        /// <summary>
        /// Register the Serilog service with a custom configuration.
        /// </summary>
        /// <param Name="services">Коллекция сервисов приложения.</param>
        /// <param Name="configuration">Конфигурация логгера.</param>
        /// <returns>Коллекция сервисов приложения с добавленным serilog.</returns>
        public static IServiceCollection AddSerilogServices(this IServiceCollection services, LoggerConfiguration configuration)
        {
            Log.Logger = configuration.CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
            return services.AddSingleton(Log.Logger);
        }

        /// <summary>
        /// Register the Serilog service for console logging only.
        /// </summary>
        /// <param Name="services">Коллекция сервисов приложения.</param>
        /// <returns>Коллекция сервисов приложения.</returns>
        public static IServiceCollection AddSerilogServices(this IServiceCollection services)
        {
            return services.AddSerilogServices(
                new LoggerConfiguration()

                    // .MinimumLevel.VerbOSe()
                    // .WriteTo.Console()
                    .WriteTo.Seq("http://localhost:5341")
                    .MinimumLevel.Debug()
                    .WriteTo.RollingFile(Path.Combine(Directory.GetCurrentDirectory(), "log-{Date}.txt")));
        }
    }
}