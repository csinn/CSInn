﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            builder.AddOAuth(DiscordAuthenticationDefaults.AuthenticationScheme, options =>
            {

                options.ClientId = config["discord:client_id"];
                options.ClientSecret = config["discord:app_secret"];
                options.CallbackPath = new PathString("/signin-discord");

                options.AuthorizationEndpoint = DiscordAuthenticationDefaults.AuthorizationEndpoint;
                options.TokenEndpoint = DiscordAuthenticationDefaults.TokenEndpoint;

                options.Scope.Add("identify");
                options.Scope.Add("guilds");

                options.Events = new OAuthEvents()
                {
                    
                    OnCreatingTicket = async ctx =>
                    {
                        //Instead of running claimactions and letting it build the claims automatically,
                        //we manually requests and parses the information we want and build the claims manually.
                        //reason is that we need to inject custom logic for setting roles/Fetching info from our db.
                        
                        JsonDocument userinfodocument = await GetInfoFromEndPoint(ctx, DiscordAuthenticationDefaults.UserInformationEndpoint);
                        JsonDocument userguildsinfodocument = await GetInfoFromEndPoint(ctx, DiscordAuthenticationDefaults.UserGuildsInformationEndPoint);

                        var avatarhex = userinfodocument.RootElement.GetProperty("avatar").GetString();
                        var username = userinfodocument.RootElement.GetProperty("username").GetString();
                        var discordID = userinfodocument.RootElement.GetProperty("id").GetString();
                        
                        IRoleProvider roleprovider = builder.Services.BuildServiceProvider().GetService<IRoleProvider>();

                        bool ismember = roleprovider.ConfirmCSInnMembership(userguildsinfodocument);

                        var role = ismember ? roleprovider.GetRole(discordID) : "Guest";

                        ctx.Identity.AddClaims(BuildClaims(

                            ("urn:discord:avatar", avatarhex),
                            (ClaimTypes.Name, username),
                            (ClaimTypes.Role, role)
                            ));
                    }
                };
            });

            return builder;
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
