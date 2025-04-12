using Microsoft.AspNetCore.Components.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http.Headers;
using System.Security.Claims;
using Colosus.Operations.Abstracts;

namespace Colosus.Client.Blazor.Services
{
    public class CustomAuthStateProvider
        : AuthenticationStateProvider
    {
        private readonly HttpClient http;
        private CookieService cookieService;
        IDataConverter dataConverter;
        public CustomAuthStateProvider(CookieService cookieService, HttpClient http,IDataConverter dataConverter)
        {
            this.http = http;
            this.cookieService = cookieService;
            this.dataConverter = dataConverter;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await cookieService.GetCookie("Token");
            var identity = new ClaimsIdentity();
            http.DefaultRequestHeaders.Authorization = null;
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                    http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
                }
                catch
                {
                    identity = new();
                }
            }
            var state = new AuthenticationState(new ClaimsPrincipal(identity));

            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = dataConverter.Deserialize<Dictionary<string, object>>(jsonBytes);



            foreach (var kvp in keyValuePairs)
            {
                claims.Add(new Claim(kvp.Key, kvp.Value.ToString()));
            }


            // Rol claims'si ["",""] şeklinde geliyor burada rol claimsini bulup silip değerleri parçalayım yeniden her birini bir claims olarak eliyoruz.
            var roleClaim = claims.SingleOrDefault(xd => xd.Type == ClaimTypes.Role || xd.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            claims.Remove(roleClaim);
            List<string> Roles = dataConverter.Deserialize<List<string>>(roleClaim.Value.ToString());
            claims.AddRange(Roles.Select(xd => new Claim(ClaimTypes.Role, xd.ToString())));
            // Bu olmaz ise birden fazla rol sahibi olanlarda authorizeview'lar çalışmıyor. Şu anda bulduğum çözümüm bir tek bu.
            // Bu işe loginController'de ayrı claims olarak yaptım ama jwt onu birleştiriyor.
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}