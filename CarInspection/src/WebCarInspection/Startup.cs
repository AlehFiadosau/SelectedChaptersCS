using DataAccessLayer.DTO;
using DataAccessLayer.DTO.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebCarInspection.Helpers;
using WebCarInspection.Interfaces;

namespace WebCarInspection
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IApiClientHelper, ApiClientHelper>(client =>
            {
                client.BaseAddress = new Uri(Configuration["ApiServerInfo:BaseAddress"]);
            });

            services.AddControllersWithViews();

            string connection = Configuration.GetConnectionString("dbConnection");
            services.AddDbContext<InspectionContext>(option => option.UseSqlServer(connection));
            services.AddIdentity<UserDto, IdentityRole>()
                .AddEntityFrameworkStores<InspectionContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Accounts}/{action=Login}/{id?}");
            });
        }
    }
}
