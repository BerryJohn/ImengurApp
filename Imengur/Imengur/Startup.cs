using Imengur.Enums;
using Imengur.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imengur
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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:Imengur:ConnectionString"]));
            services.AddIdentity<BetterUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddSession();

            services.AddTransient<IImageRepository, EFImageRepository>();
            services.AddTransient<ICrudImageRepository, CrudImageRepository>();
            services.AddTransient<ICustomerImageRepository, CustomerImageRepository>();

            services.AddTransient<IBetterUserRepository, EFUserRepository>();
            services.AddTransient<ICrudBetterUserRepository, CrudUserRepository>();

            services.AddTransient<ICommentRepository, EFCommentRepository>();
            services.AddTransient<ICrudCommentRepository, CrudCommentRepository>();
            services.AddTransient<ICustomerCommentRepository, CustomerCommentRepository>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminAccess", policy => policy.RequireRole(Roles.Admin.ToString()));
                options.AddPolicy("ModeratorAccess", policy => policy.RequireRole(Roles.Moderator.ToString()));
                options.AddPolicy("UsersAccess", policy => policy.RequireRole(Roles.User.ToString()));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            IdentitySeedData.CreateUserRoles(app);
        }
    }
}
