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
    /// ����������� ����� � ������ �����.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ��������� ���������� ������������.
        /// </summary>
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json")
            .Build();

        /// <summary>
        /// ����� ����� ����������.
        /// </summary>
        /// <param name="args">��������� ��������� ������.</param>
        /// <returns>
        /// ������ ���������� ������ ���������:
        /// 0 - ���������� ���������� ������;
        /// 1 - ������ ��� �������� ����.
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
        /// ������ ��������� ����.
        /// </summary>
        /// <param name="args">��������� ��������� ������.</param>
        /// <returns>������������� ����.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
