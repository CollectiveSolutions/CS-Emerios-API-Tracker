using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Data;
using System.Linq;
using System.Security;
using System.Security.Claims;

namespace CS_Emerios_API_Tracker.Helper
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _localStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                ClaimsPrincipal claimsPrincipal;

                var userSessionStorageResult = await _localStorage.GetAsync<string>("UserIdentity");
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                var claims = new List<Claim>
{
                    //new Claim("CustomClaimTypeID", $"{userSession.ID}"),
                    //new Claim("CustomClaimTypeCountryCode", $"{userSession.Country}"),
                    new Claim(ClaimTypes.Name, userSession),
                };

                //foreach (var permission in permission_list)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, permission));
                //}

                var identity = new ClaimsIdentity(claims, "CustomAuth");
                claimsPrincipal = new ClaimsPrincipal(identity);

                //if (userSession.SessionDate.AddHours(6) < DateTime.UtcNow)
                //{
                //    await _localStorage.DeleteAsync("UserIdentity");
                //    claimsPrincipal = _anonymous;
                //}
                //else
                //{
                //    userSession.SessionDate = DateTime.UtcNow;
                //    await _localStorage.SetAsync("UserIdentity", userSession);
                //}

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(string userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                await _localStorage.SetAsync("UserIdentity", userSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new (ClaimTypes.Name, userSession)
                }));

            }
            else
            {
                //await _localStorage.DeleteAsync("Timezone");
                await _localStorage.DeleteAsync("UserIdentity");
                claimsPrincipal = _anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
