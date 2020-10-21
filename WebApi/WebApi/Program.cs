using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi
{
    public class Program
    {
        public static UsersDB DB = new UsersDB();

        public static void Main(string[] args)
        {
            DB.Create(new UserStatistics("ivan", DateTime.Now, "1.0.0", "android"));
            DB.Create(new UserStatistics("pavel", DateTime.Now, "1.0.0", "android"));

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
