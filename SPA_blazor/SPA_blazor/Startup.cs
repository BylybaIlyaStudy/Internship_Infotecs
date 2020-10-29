using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infotecs.SPA_blazor.Data;

namespace Infotecs.SPA_blazor
{
    /// <summary>
    /// Входная точка приложения ASP.NET Core.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Задание параметров конфигурации.
        /// </summary>
        /// <param name="configuration">Параметры конфигурации.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Строитель параметров конфигурации.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Регистрирация сервисов, которые используются приложением.
        /// </summary>
        /// <param name="services">Коллекция сервисов приложения.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<UserStatisticsService>();
        }

        /// <summary>
        /// Установка способа обработки запроса.
        /// </summary>
        /// <param name="app">Компоненты обработки запроса.</param>
        /// <param name="env">Информация о среде запуска приложения.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
