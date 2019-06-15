using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CSInn.Presentation.Blazor.Data
{
    class FakeAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, "Some fake user"),
            new Claim(ClaimTypes.Role, "Some fake role")
            },
            
            "Fake authentication type");
            
            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
