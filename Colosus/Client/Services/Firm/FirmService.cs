
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.DTO;
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
        public async Task<RequestResult<List<FirmDTO>>> GetMyFirmAsync() =>
            await service.GetPostAsync<List<FirmDTO>>(GetAddress("GetMyFirm"));

        public async Task<RequestResult> AddFirm(Entity.Concretes.DatabaseModel.Firm firm)
            => await service.GetPostAsync(GetAddress("AddNewFirm"), firm);

        public async Task<RequestResult> JoinAFirm(string Key)
            => await service.GetPostAsync(GetAddress("JoinAFirm"),new JoinFirmRequestModel() { Description = "", FirmPublicKey = Key });

    }
}
