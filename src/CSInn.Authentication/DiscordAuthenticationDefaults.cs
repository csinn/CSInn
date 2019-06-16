using System;
using System.Collections.Generic;
using System.Text;

namespace CSInn.Application.Discord.Authentication
{
    public static class DiscordAuthenticationDefaults
    {
        public static string AuthenticationScheme { get; } = "Discord";

        public static string AuthorizationEndpoint { get; } = "https://discordapp.com/api/oauth2/authorize";

        public static string TokenEndpoint { get; } = "https://discordapp.com/api/oauth2/token";

        public static string UserInformationEndpoint { get; } = "https://discordapp.com/api/users/@me";

        public static string UserGuildsInformationEndPoint { get; } = "https://discordapp.com/api/users/@me/guilds";
    }
}
