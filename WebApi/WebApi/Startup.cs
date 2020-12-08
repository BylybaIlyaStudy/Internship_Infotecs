// <copyright file="Startup.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Injection;
using WebApi.Repositories;
using System.Text.Json;
using RabbitMQ.Client;
using System.Configuration;
using Infotecs.WebApi.Services;

namespace Infotecs.WebApi
{
    /// <summary>
    /// Входная точка приложения ASP.NET Core.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Регистрирация сервисов, которые используются приложением.
        /// </summary>
        /// <param name="services">Коллекция сервисов приложения.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ICustomerUpdateSender, CustomerUpdateSender>();
            services.AddSerilogServices();
            services.AddSwaggerGen();

            services.AddSignalR();

            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       .AllowCredentials();
            }));


            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                });
        }

        /// <summary>
        /// Установка способа обработки запроса.
        /// </summary>
        /// <param name="app">Компоненты обработки запроса.</param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<WebApiHub>("/api");
                endpoints.MapDefaultControllerRoute();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
            });
        }
    }
}
