using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CSInn.Presentation.Blazor.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using CSInn.Application.Discord.Authentication;

namespace CSInn.Presentation.Blazor
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            this._config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
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
            })
            .AddDiscord(this._config);

            // TODO: In Preview 6 we will get IAuthenticationState to replace this.
            // Validate if the user is authenticated with @inject ClaimsPrincipal for the time being.
            services.AddHttpContextAccessor();
            services.AddScoped(ioc => ioc.GetRequiredService<IHttpContextAccessor>().HttpContext.User);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
