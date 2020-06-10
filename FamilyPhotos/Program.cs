using System;
using System.Collections.Generic;
//Plesz: using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FamilyPhotos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Plesz:
            //new WebHostBuilder()
            //    .UseUrls("http://*:1000")
            //    .UseKestrel()
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .Build()
            //    .Run();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .CaptureStartupErrors(true)
                        .UseSetting("detailedErrors", "true")
                        .UseStartup<Startup>();
                });
    }
}
