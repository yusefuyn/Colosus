using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.RequestModel;
using Colosus.Entity.Concretes.ResultModel;

namespace Colosus.Client.Blazor.Services.Login
{
    public interface ILoginService
    {
        public Task<RequestResult<LoginResultModel>> LoginAsync(string username, string password);
        public Task<RequestResult> RegisterAsync(RegisterCreateModel user);
    }
}
