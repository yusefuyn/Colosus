using Colosus.Entity.Concretes;

namespace Colosus.Client.Services.Firm
{
    public interface IFirmService
    {
        public Task<RequestResult> GetMyFirmAsync();
        public Task<RequestResult> AddFirm(Entity.Concretes.DatabaseModel.Firm firm);

    }
}
