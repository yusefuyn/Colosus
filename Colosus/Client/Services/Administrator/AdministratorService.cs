using Colosus.Entity.Concretes.RequestModel;

namespace Colosus.Client.Blazor.Services.Administrator
{
    public class AdministratorService : IAdministratorService
    {
        HttpClientService httpClientService;
        public AdministratorService(HttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public string GetAddress(string Action) => AppState.GetAddress("Administrator", Action);

        public async Task<RequestResult> UpdateDatabase()
            => await httpClientService.GetPostAsync(GetAddress("UpdateDatabase"));
    }
}
