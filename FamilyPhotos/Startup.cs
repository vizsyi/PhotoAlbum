using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyPhotos.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.Extensions.Configuration;

namespace FamilyPhotos
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Mem�ri�ba t�rol�shoz kell
            services.AddSingleton<PhotoRepository, PhotoRepository>();

            //AutoMapper
            var automapperCgf = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile(new ViewModel.PhotoProfile()));
            var mapper = automapperCgf.CreateMapper();

            services.AddSingleton(mapper); //inent�l k�rhetem a Controller param�terlist�j�ban

            //2015: services.AddMvc();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //1. Ha nem csak egyszer� st�tuszk�dokkal akarunk v�laszolni, hanem szeretn�nk egyszer� inform�ci�t adni,
            // akkor p�ld�ul �gy tudunk 400-599 k�z�tti k�dokhoz megold�s, de csak ha el�tte a kv�telt kezelt�k �s
            // a st�tuszk�ddal t�r�nk vissza az action-b�l.
            // app.UseStatusCodePages();
            //2.
            // app.UseStatusCodePages("text/plain", "Ez egy hib�s k�r�s, a k�d: {0}");

            //4. �ti�ny�thatjuk saj�t oldalra
            //app.UseStatusCodePagesWithRedirects("~/Errors/CodePagesithRedirects/{0}"); //�gy id-vel kell �tvenni
            app.UseStatusCodePagesWithRedirects("~/Errors/CodePagesithRedirects?statusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Photo}/{action=Index}/{id?}");
            });
        }
    }
}
