using Colosus.Entity.Concretes;

namespace Colosus.Client.Services.Administrator
{
    public interface IAdministratorService
    {

        public Task<RequestResult> UpdateDatabase();
    }
}
