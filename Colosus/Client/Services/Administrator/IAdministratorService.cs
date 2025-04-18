using Colosus.Entity.Concretes.RequestModel;

namespace Colosus.Client.Blazor.Services.Administrator
{
    public interface IAdministratorService
    {

        public Task<RequestResult> UpdateDatabase();
    }
}
