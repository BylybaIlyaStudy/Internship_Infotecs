using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infotecs.SPA_blazor.Data;

namespace Infotecs.SPA_blazor
{
    /// <summary>
    /// ������� ����� ���������� ASP.NET Core.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ������� ���������� ������������.
        /// </summary>
        /// <param name="configuration">��������� ������������.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ��������� ���������� ������������.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ������������� ��������, ������� ������������ �����������.
        /// </summary>
        /// <param name="services">��������� �������� ����������.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<UserStatisticsService>();
        }

        /// <summary>
        /// ��������� ������� ��������� �������.
        /// </summary>
        /// <param name="app">���������� ��������� �������.</param>
        /// <param name="env">���������� � ����� ������� ����������.</param>
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
