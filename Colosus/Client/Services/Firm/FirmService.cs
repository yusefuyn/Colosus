
using Colosus.Entity.Concretes.RequestModel;
using System.Collections.Generic;

namespace Colosus.Client.Blazor.Services.Firm
{
    public class FirmService : IFirmService
    {
        HttpClientService service;
        public FirmService(HttpClientService service)
        {
            this.service = service;
        }
        private string GetAddress(string Action) => AppState.GetAddress("Firm", Action);
        public async Task<RequestResult<List<Entity.Concretes.DatabaseModel.Firm>>> GetMyFirmAsync() =>
            await service.GetPostAsync<List<Entity.Concretes.DatabaseModel.Firm>>(GetAddress("GetMyFirm"));

        public async Task<RequestResult> AddFirm(Entity.Concretes.DatabaseModel.Firm firm)
            => await service.GetPostAsync(GetAddress("AddNewFirm"), firm);
    }
}
