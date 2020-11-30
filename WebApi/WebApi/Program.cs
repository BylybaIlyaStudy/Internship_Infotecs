// <copyright file="Program.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

using System;
using System.IO;
using FluentMigrator.Runner;
using Infotecs.WebApi.Migrations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Infotecs.WebApi
{
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
        /// <param Name="args">Аргументы командной строки.</param>
        /// <returns>
        /// Статус завершения работы программы:
        /// 0 - нормальное завершение работы;
        /// 1 - ошибка при создании узла.
        /// </returns>
        public static int Main(string[] args)
        {
            var serviceProvider = CreateServices();

            // Put the database upDate into a scope to ensure
            // that all resources will be dispOSed.
            using (var scope = serviceProvider.CreateScope())
            {
                UpDateDatabase(scope.ServiceProvider);
            }

            try
            {
                Log.Information("Starting web Host");
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
        /// <param Name="args">Аргументы командной строки.</param>
        /// <returns>Универсальный узел.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore().ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddPostgres()
                    // Set the connection string
                    .WithGlobalConnectionString(Configuration.GetConnectionString("DefaultConnection"))
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(Baseline).Assembly, typeof(Migration_1).Assembly).For.Migrations())
                    //.ScanIn(typeof(Migration_1).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        private static void UpDateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}
