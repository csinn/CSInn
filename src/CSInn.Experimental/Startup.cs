using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CSInn.Experimental.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using CSInn.Experimental.Models;

namespace CSInn.Experimental
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
            services.AddRazorPages();
            services.AddServerSideBlazor();

            // deprecated in 3.0
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            //
            //services.AddScoped<IRoleProvider, RoleProvider>();

            services.AddAuthentication(ConfigureAuthenticationOptions)
                .AddCookie(ConfigureCookieOptions)
            // .AddJwtBearer
            //    .AddDiscord(this._config)
            ;

            services.AddAuthorization(ConfigureAuthorizationOptions);

            services.AddDbContext<ExperimentContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ExperimentContext")));
            //Uncomment to use a fake idenitity that is authorized from the start.
            //services.AddScoped<AuthenticationStateProvider, FakeAuthenticationStateProvider>();

            //var b = services.Contains(new ServiceDescriptor(typeof(IRoleProvider), typeof(RoleProvider), ServiceLifetime.Scoped));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-2.2&tabs=visual-studio

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseStaticFiles(); // place UseStaticFiles before UseRouting

            app.UseRouting(); // place UseStaticFiles before UseRouting

            app.UseAuthentication(); // place the call to UseAuthentication and UseAuthorization after UseRouting
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapBlazorHub();

                // for some reason, this is rendered on each page
                endpoints.MapFallbackToPage("/_Host");

                // use [AllowAnonymous]
                //endpoints
                //    .MapDefaultControllerRoute()
                //    .RequireAuthorization();

                /* example of 
                endpoints
                    .MapHealthChecks("/healthz")
                    .RequireAuthorization(new AuthorizeAttribute(){ Roles = "admin", });
                 */
            });
        }

        private void ConfigureAuthorizationOptions(AuthorizationOptions options)
        {
            // The DefaultPolicy is triggered by [Authorize] or RequireAuthorization
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                //.RequireScope("MyScope")
                .Build();
        }

        private void ConfigureAuthenticationOptions(AuthenticationOptions options)
        {
//            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//            options.DefaultChallengeScheme = Defaults.AuthenticationScheme;
        }

        private void ConfigureCookieOptions(CookieAuthenticationOptions options)
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.Lax; // Needed for OAuth.

            // TODO: Local dev needs to run over HTTPS. Commenting out because project isn't doing so at the moment.
            // options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

            options.LoginPath = "/auth/login";
            options.LogoutPath = "/auth/logout";
            options.ReturnUrlParameter = "/user/profile";

            options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            options.SlidingExpiration = true;
        }

    }
}
