using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;
namespace Colosus.Client.Blazor.Services.Firm
{
    public interface IFirmService
    {
        public Task<RequestResult<List<FirmDTO>>> GetMyFirmAsync();
        public Task<RequestResult> AddFirm(Entity.Concretes.DatabaseModel.Firm firm);
        public Task<RequestResult> JoinAFirm(string Key);
    }
}
