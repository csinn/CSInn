using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CSInn.Presentation.Blazor.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

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
            services.AddRazorPages()
                .AddRazorPagesOptions(options => 
                {
                    options.Conventions.AuthorizePage("/counter");
                });
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = "Discord";
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddOAuth("Discord", options =>
            {
                options.ClientId = this._config["discord:client_id"];
                options.ClientSecret = this._config["discord:app_secret"];
                options.CallbackPath = new PathString("/signin-discord");

                options.AuthorizationEndpoint = "https://discordapp.com/api/oauth2/authorize";
                options.TokenEndpoint = "https://discordapp.com/api/oauth2/token";
                options.UserInformationEndpoint = "https://discordapp.com/api/users/@me";

                options.Scope.Add("identify");

                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id", ClaimValueTypes.UInteger64);
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, "username", ClaimValueTypes.String);
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email", ClaimValueTypes.Email);
                options.ClaimActions.MapJsonKey("urn:discord:discriminator", "discriminator", ClaimValueTypes.UInteger32);
                options.ClaimActions.MapJsonKey("urn:discord:avatar", "avatar", ClaimValueTypes.String);
                options.ClaimActions.MapJsonKey("urn:discord:verified", "verified", ClaimValueTypes.Boolean);

                options.Events = new OAuthEvents()
                {
                    OnCreatingTicket = async ctx =>
                    {
                        var request = new HttpRequestMessage(HttpMethod.Get, ctx.Options.UserInformationEndpoint);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ctx.AccessToken);

                        var response = await ctx.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ctx.HttpContext.RequestAborted);
                        response.EnsureSuccessStatusCode();

                        var document = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
                        ctx.RunClaimActions(document.RootElement);
                    }
                };
            })
            .AddCookie(options => 
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax; // Needed for OAuth.
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

                options.LoginPath = "/auth/login";
                options.LogoutPath = "/auth/logout";
                options.ReturnUrlParameter = "/user/profile";

                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.SlidingExpiration = true;
            });

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
