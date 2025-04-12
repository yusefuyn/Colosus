using Colosus.Entity.Concretes;

namespace Colosus.Client.Blazor.Services.Firm
{
    public class FirmService : IFirmService
    {
        HttpClientService service;
        public FirmService(HttpClientService service)
        {
            this.service = service;
        }
        private string GetAddress(string Action) => AppState.GetAddress("Firm",Action);
        public async Task<RequestResult> GetMyFirmAsync() => await service.GetPostAsync<RequestResult>("", GetAddress("GetMyFirm"));

        public async Task<RequestResult> AddFirm(Entity.Concretes.DatabaseModel.Firm firm) => await service.GetPostAsync<RequestResult>(firm, GetAddress("AddNewFirm"));
    }
}
