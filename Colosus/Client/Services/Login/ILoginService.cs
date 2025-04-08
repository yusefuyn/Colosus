using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.DatabaseModel;

namespace Colosus.Client.Services.Login
{
    public interface ILoginService
    {
        public Task<RequestResult> LoginAsync(string username, string password);
        public Task<RequestResult> RegisterAsync(User user);
    }
}
