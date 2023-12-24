using Demo.DependencyInjections;
using Domain.entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Students;
using Domain.Abstractrions;
using Persistence;

namespace Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<IServiceA, ServiceA>();
            var assembly = typeof(ApplicationDbContext).Assembly.GetName().Name;
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(assembly)));
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddScoped<IRepository, EfRepository>();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Middleware1: Incoming Request\n");
            //    //Calling the Next Middleware Component
            //    await next();
            //    await context.Response.WriteAsync("Middleware1: Outgoing Response\n");
            //});
            ////Second Middleware Component Registered using Use Extension Method
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Middleware2: Incoming Request\n");
            //    //Calling the Next Middleware Component
            //    await next();
            //    await context.Response.WriteAsync("Middleware2: Outgoing Response\n");
            //});
            ////Third Middleware Component Registered using Run Extension Method
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Middleware3: Incoming Request handled and response generated\n");
            //    //Terminal Middleware Component i.e. cannot call the Next Component
            //});
        }
    }
}
