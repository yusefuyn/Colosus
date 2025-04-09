using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.CreateModel;

namespace Colosus.Client.Services.Product
{
    public interface IProductService
    {
        public Task<RequestResult> AddProduct(Entity.Concretes.CreateModel.ProductCreateModel product);
        public Task<RequestResult> GetMyFirmProductDTOs(string FirmPublicKey);
        public Task<RequestResult> AddStockForProduct(Entity.Concretes.CreateModel.StockCreateModel product);
        public Task<RequestResult> GetStockHistoryDTO(string ProductPublicKey);
        public Task<RequestResult> DeleteProduct(string ProductPublicKey);
        public Task<RequestResult> DeleteStock(string StockPublicKey);

    }
}
