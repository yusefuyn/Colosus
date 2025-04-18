using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.RequestModel;

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
        public async Task<RequestResult<string>> LoginAsync(string username, string password) // TODO : Daha sonra bi createobjesiyle sarmalla.
            => await service.GetPostAsync<string, User>(GetAddress("Login"), new User() { UserName = username, Password = password });
        public async Task<RequestResult> RegisterAsync(User user)
            => await service.GetPostAsync<User>(GetAddress("Register"), user);
    }
}
