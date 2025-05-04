using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;

namespace Colosus.Client.Blazor.Services.Product
{
    public class ProductService : IProductService
    {
        HttpClientService httpClientService;
        public ProductService(HttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public string GetAddress(string Action) => AppState.GetAddress("Product", Action);

        public async Task<RequestResult> AddProduct(ProductCreateModel product) =>
            await httpClientService.GetPostAsync(GetAddress("AddProduct"), product);

        public async Task<RequestResult<List<ProductDTO>>> GetMyFirmProductDTOs(string FirmPublicKey) =>
            await httpClientService.GetPostAsync<List<ProductDTO>, PublicKeyRequestModel>(GetAddress("GetMyFirmProducts"), new PublicKeyRequestModel() { PublicKey = FirmPublicKey });

        public async Task<RequestResult<ProductDTO>> AddStockForProduct(StockCreateModel product) =>
            await httpClientService.GetPostAsync<ProductDTO, StockCreateModel>(GetAddress("AddStockForProduct"), product);

        public async Task<RequestResult<List<ProductStockDTO>>> GetStockHistoryDTO(string ProductPublicKey) =>
            await httpClientService.GetPostAsync<List<ProductStockDTO>, PublicKeyRequestModel>(GetAddress("GetStockHistoryDTO"), new PublicKeyRequestModel() { PublicKey = ProductPublicKey });

        public async Task<RequestResult> DeleteProduct(string ProductPublicKey) =>
            await httpClientService.GetPostAsync(GetAddress("DeleteProduct"), new PublicKeyRequestModel() { PublicKey = ProductPublicKey });

        public async Task<RequestResult> DeleteStock(string StockPublicKey)
           => await httpClientService.GetPostAsync(GetAddress("DeleteStock"), new PublicKeyRequestModel() { PublicKey = StockPublicKey });

    }
}
