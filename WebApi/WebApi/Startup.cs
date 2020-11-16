// <copyright file="Startup.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Injection;
using WebApi.Repositories;
using Microsoft.AspNetCore.Mvc.Formatters.Json;
using System.Text.Json;
using Newtonsoft.Json.Serialization;

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
        /// <param Name="services">Коллекция сервисов приложения.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSerilogServices();
            //services.AddControllers(); // используем контроллеры без представлений
            services.AddSwaggerGen();

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
        /// <param Name="app">Компоненты обработки запроса.</param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

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
