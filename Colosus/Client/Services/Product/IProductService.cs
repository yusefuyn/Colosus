using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;

namespace Colosus.Client.Blazor.Services.Product
{
    public interface IProductService
    {
        public Task<RequestResult> AddProduct(ProductCreateModel product);
        public Task<RequestResult<List<ProductDTO>>> GetMyFirmProductDTOs(string FirmPublicKey);
        public Task<RequestResult<ProductDTO>> AddStockForProduct(StockCreateModel product);
        public Task<RequestResult<List<ProductStockDTO>>> GetStockHistoryDTO(string ProductPublicKey);
        public Task<RequestResult> DeleteProduct(string ProductPublicKey);
        public Task<RequestResult> DeleteStock(string StockPublicKey);

    }
}
