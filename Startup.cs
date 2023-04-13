using HotelMgmtSys.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using HotelMgmtSys.Controllers;

namespace HotelMgmtSys
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HotelManagementDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HotelManagementDbContext")));

            services.AddTransient<HomeController>();
            services.AddControllersWithViews();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddRazorPages();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                });
        }

        protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("HotelManagementDbContext");
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ...
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Add this line to enable authentication

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "search",
                    pattern: "Home/Search/{id}",
                    defaults: new { controller = "Home", action = "Search" });
                endpoints.MapControllerRoute(
                    name: "BookRoom",
                    pattern: "Home/BookRoom/{id}",
                    defaults: new { controller = "Home", action = "BookRoom" });
                endpoints.MapControllerRoute(
                    name: "GuestCheckout",
                    pattern: "Home/GuestCheckoutConfirmation/{id}",
                    defaults: new { controller = "Home", action = "GuestCheckoutConfirmation" });
                endpoints.MapControllerRoute(
                    name: "SignUp",
                    pattern: "Account/Signup/{id}",
                    defaults: new { controller = "Account", action = "Signup" });
                endpoints.MapControllerRoute(
                    name: "Register",
                    pattern: "Account/Register/{id}",
                    defaults: new { controller = "Account", action = "Register" });
                endpoints.MapControllerRoute(
                    name: "LogIn",
                    pattern: "Account/Login/{id}",
                    defaults: new { controller = "Account", action = "Login" });
                endpoints.MapControllerRoute(
                    name: "RedirectToLocal",
                    pattern: "Account/RedirectToLocal/{id}",
                    defaults: new { controller = "Account", action = "RedirectToLocal" });
                endpoints.MapControllerRoute(
                    name: "ClientDashboard",
                    pattern: "Client/Dashboard/{id}",
                    defaults: new { controller = "Client", action = "Dashboard" });
                endpoints.MapControllerRoute(
                    name: "room_details",
                    pattern: "Admin/Dashboard/{id}",
                    defaults: new { controller = "Admin", action = "Dashboard" });
                endpoints.MapControllerRoute(
                    name: "Add_Room",
                    pattern: "Admin/AddRoom/{id}",
                    defaults: new { controller = "Admin", action = "AddRooom" });
                endpoints.MapControllerRoute(
                    name: "AssignRoom",
                    pattern: "Admin/AssignRoom/{id}",
                    defaults: new { controller = "Admin", action = "AssignRoom" });
                endpoints.MapControllerRoute(
                    name: "ListClients",
                    pattern: "Admin/ListClients/{id}",
                    defaults: new { controller = "Admin", action = "ListClients" });
                
            });

        }
    }
}
