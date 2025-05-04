using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.RequestModel;
using Colosus.Entity.Concretes.ResultModel;

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
        public async Task<RequestResult<LoginResultModel>> LoginAsync(string username, string password) // TODO : Daha sonra bi createobjesiyle sarmalla.
            => await service.GetPostAsync<LoginResultModel, User>(GetAddress("Login"), new User() { UserName = username, Password = password });
        public async Task<RequestResult> RegisterAsync(RegisterCreateModel user)
            => await service.GetPostAsync<RegisterCreateModel>(GetAddress("Register"), user);
    }
}
