using CSInn.Application.Discord.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OAuthExtensions
    {
        public static AuthenticationBuilder AddDiscord(this AuthenticationBuilder builder, IConfiguration config)
        {
            builder.AddOAuth(DiscordAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.ClientId = config["discord:client_id"];
                options.ClientSecret = config["discord:app_secret"];
                options.CallbackPath = new PathString("/signin-discord");

                options.AuthorizationEndpoint = DiscordAuthenticationDefaults.AuthorizationEndpoint;
                options.TokenEndpoint = DiscordAuthenticationDefaults.TokenEndpoint;
                options.UserInformationEndpoint = DiscordAuthenticationDefaults.UserInformationEndpoint;

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
            });

            return builder;
        }
    }
}
