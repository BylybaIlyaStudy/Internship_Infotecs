// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog.Injection;

    /// <summary>
    /// ������� ����� ���������� ASP.NET Core.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ������������� ��������, ������� ������������ �����������.
        /// </summary>
        /// <param name="services">��������� �������� ����������.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository, UsersDB>();
            services.AddSerilogServices();
            services.AddControllers(); // ���������� ����������� ��� �������������
            services.AddSwaggerGen();
        }

        /// <summary>
        /// ��������� ������� ��������� �������.
        /// </summary>
        /// <param name="app">���������� ��������� �������.</param>
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
                endpoints.MapControllers(); // ���������� ������������� �� �����������
            });
        }
    }
}
