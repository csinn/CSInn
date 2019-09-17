using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSInn.Application.Discord.Authentication
{
    public static class OAuthenticationExtensions
    {
        public static AuthenticationBuilder AddDiscord(this AuthenticationBuilder builder, IConfiguration config)
        {
            builder.AddOAuth(Defaults.AuthenticationScheme, options => ConfigureOptions(builder, options, config));
            return builder;
        }

        private static void ConfigureOptions(AuthenticationBuilder builder, OAuthOptions options, IConfiguration config)
        {
            options.ClientId = config["discord:client_id"];
            options.ClientSecret = config["discord:app_secret"];
            options.CallbackPath = new PathString("/signin-discord");

            options.AuthorizationEndpoint = Defaults.AuthorizationEndpoint;
            options.TokenEndpoint = Defaults.TokenEndpoint;

            options.Scope.Add("identify");
            options.Scope.Add("guilds");

            options.Events = new OAuthEvents()
            {
                OnCreatingTicket = ctx => CreateTickedHandler(builder, ctx)
            };
        }

        private async static Task CreateTickedHandler(AuthenticationBuilder builder, OAuthCreatingTicketContext ctx)
        {
            //Instead of running claimactions and letting it build the claims automatically,
            //we manually request and parse the information we want and build the claims manually.
            //reason is that we need to inject custom logic for setting roles/Fetching info from our db.

            JsonDocument userInfoDocument = await GetInfoFromEndPoint(ctx, Defaults.UserInformationEndpoint);
            JsonDocument userGuildsInfoDocument = await GetInfoFromEndPoint(ctx, Defaults.UserGuildsInformationEndPoint);

            var avatarHex = userInfoDocument.RootElement.GetProperty("avatar").GetString();
            var username = userInfoDocument.RootElement.GetProperty("username").GetString();
            var discordID = userInfoDocument.RootElement.GetProperty("id").GetString();

            IRoleProvider roleProvider = builder.Services.BuildServiceProvider().GetService<IRoleProvider>();

            bool isMember = roleProvider.ConfirmCSInnMembership(userGuildsInfoDocument);

            var role = isMember ? roleProvider.GetRole(discordID) : "Guest";

            ctx.Identity.AddClaims(BuildClaims(
                ("urn:discord:avatar", avatarHex),
                (ClaimTypes.Name, username),
                (ClaimTypes.Role, role)
                ));
        }

        private async static Task<JsonDocument> GetInfoFromEndPoint(OAuthCreatingTicketContext ctx, string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ctx.AccessToken);

            var response = await ctx.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ctx.HttpContext.RequestAborted);
            response.EnsureSuccessStatusCode();

            var document = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());

            return document;
        }

        private static IEnumerable<Claim> BuildClaims(params (string type, string value)[] claimvalues)
        {
            List<Claim> claims = new List<Claim>();

            foreach (var (type, value) in claimvalues)
            {
                if (type != null && value != null)
                {
                    claims.Add(new Claim(type, value));
                }
            }

            return claims;
        }
    }
}
