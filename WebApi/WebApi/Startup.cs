// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog.Injection;

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
            services.AddSingleton<IRepository, UsersDB>();
            services.AddSerilogServices();
            services.AddControllers(); // используем контроллеры без представлений
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Установка способа обработки запроса.
        /// </summary>
        /// <param name="app">Компоненты обработки запроса.</param>
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
