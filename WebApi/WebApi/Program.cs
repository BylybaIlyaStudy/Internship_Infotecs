// <copyright file="Program.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

namespace Infotecs.WebApi
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    /// <summary>
    /// Стандартный класс с точкой входа.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Строитель параметров конфигурации.
        /// </summary>
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json")
            .Build();

        /// <summary>
        /// Точка входа приложения.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        /// <returns>
        /// Статус завершения работы программы:
        /// 0 - нормальное завершение работы;
        /// 1 - ошибка при создании узла.
        /// </returns>
        public static int Main(string[] args)
        {
            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// Запуск строителя узла.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        /// <returns>Универсальный узел.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
