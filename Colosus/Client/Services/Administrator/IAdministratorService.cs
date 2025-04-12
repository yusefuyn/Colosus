using Colosus.Entity.Concretes;

namespace Colosus.Client.Blazor.Services.Administrator
{
    public interface IAdministratorService
    {

        public Task<RequestResult> UpdateDatabase();
    }
}
