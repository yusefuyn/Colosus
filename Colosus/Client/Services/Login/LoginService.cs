using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.DatabaseModel;

namespace Colosus.Client.Blazor.Services.Login
{
    public class LoginService : ILoginService
    {
        HttpClientService service;
        public LoginService(HttpClientService service)
        {
            this.service = service;
        }

        public string GetAddress(string Action) => AppState.GetAddress("Login", Action);

        public async Task<RequestResult> LoginAsync(string username, string password)
        {
            User user = new() { UserName = username, Password = password };
            var res = await service.GetPostAsync<RequestResult>(user, GetAddress("Login"));
            return res;
        }

        public async Task<RequestResult> RegisterAsync(User user)
        {
            var res = await service.GetPostAsync<RequestResult>(user, GetAddress("Register"));
            return res;
        }
    }
}
