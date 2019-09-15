using System.Text.Json;

namespace CSInn.Application.Discord.Authentication
{
    public interface IRoleProvider
    {
        bool ConfirmCSInnMembership(JsonDocument guildsdocument)
        {
            const string CsInnGuildID = "475671343463923714";

            foreach (var arrayitem in guildsdocument.RootElement.EnumerateArray())
            {
                if (arrayitem.TryGetProperty("id", out var result) && result.ToString() == CsInnGuildID)
                {
                    return true;
                }
            }

            return false;
        }

        string GetRole(string identifier);
    }
}
