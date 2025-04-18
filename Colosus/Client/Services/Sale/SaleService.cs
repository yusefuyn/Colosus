using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;
using Colosus.Entity.Concretes.ResultModel;
using System.Collections.Generic;

namespace Colosus.Client.Blazor.Services.Sale
{
    public class SaleService : ISaleService
    {
        HttpClientService clientService;
        public SaleService(HttpClientService clientService)
        {
            this.clientService = clientService;
        }

        public string GetAddress(string Action) => AppState.GetAddress("Sale", Action);

        public Task<RequestResult<PosDTO>> GetMyPosDTO(string firmPublicKey, string CustomerPublicKey) // TODO : Sarmalla
            => clientService.GetPostAsync<PosDTO, dynamic>(GetAddress("GetMyPosDTO"), new { FirmPublicKey = firmPublicKey, CustomerPublicKey = CustomerPublicKey });

        public Task<RequestResult<List<BasicCustomerResultModel>>> GetMyCustomers(string firmPublicKey)
            => clientService.GetPostAsync<List<BasicCustomerResultModel>, string>(GetAddress("GetMyCustomersWithFirmPubKey"), firmPublicKey);
    }
}
