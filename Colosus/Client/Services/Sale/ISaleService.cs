using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;
using Colosus.Entity.Concretes.ResultModel;

namespace Colosus.Client.Blazor.Services.Sale
{
    public interface ISaleService
    {
        public Task<RequestResult<PosDTO>> GetMyPosDTO(PosRequestModel posRequestModel);
        public Task<RequestResult<List<BasicCustomerResultModel>>> GetMyCustomers(string firmPublicKey);
    }
}
