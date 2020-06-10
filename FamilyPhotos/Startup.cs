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
            //Memóriába tároláshoz kell
            services.AddSingleton<PhotoRepository, PhotoRepository>();

            //AutoMapper
            var automapperCgf = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile(new ViewModel.PhotoProfile()));
            var mapper = automapperCgf.CreateMapper();

            services.AddSingleton(mapper); //inentõl kérhetem a Controller paraméterlistájában

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

            //1. Ha nem csak egyszerû státuszkódokkal akarunk válaszolni, hanem szeretnénk egyszerû információt adni,
            // akkor például így tudunk 400-599 közötti kódokhoz megoldás, de csak ha elõtte a kvételt kezeltük és
            // a státuszkóddal térünk vissza az action-bõl.
            // app.UseStatusCodePages();
            //2.
            // app.UseStatusCodePages("text/plain", "Ez egy hibás kérés, a kód: {0}");

            //4. Átiányíthatjuk saját oldalra
            //app.UseStatusCodePagesWithRedirects("~/Errors/CodePagesithRedirects/{0}"); //így id-vel kell átvenni
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
