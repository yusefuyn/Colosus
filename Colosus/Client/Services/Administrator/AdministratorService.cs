using Colosus.Entity.Concretes;

namespace Colosus.Client.Services.Administrator
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
        {
            var res = await httpClientService.GetPostAsync<RequestResult>("", GetAddress("UpdateDatabase"));
            return res;
        }
    }
}
