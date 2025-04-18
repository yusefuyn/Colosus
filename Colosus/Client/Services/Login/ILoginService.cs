using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.RequestModel;

namespace Colosus.Client.Blazor.Services.Login
{
    public interface ILoginService
    {
        public Task<RequestResult<string>> LoginAsync(string username, string password);
        public Task<RequestResult> RegisterAsync(User user);
    }
}
