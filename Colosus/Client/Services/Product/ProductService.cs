using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DTO;

namespace Colosus.Client.Services.Product
{
    public class ProductService : IProductService
    {
        HttpClientService httpClientService;
        public ProductService(HttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public string GetAddress(string Action) => AppState.GetAddress("Product", Action);

        public async Task<RequestResult> AddProduct(Entity.Concretes.CreateModel.ProductCreateModel product) =>
            await httpClientService.GetPostAsync<RequestResult>(product, GetAddress("AddProduct"));

        public async Task<RequestResult> GetMyFirmProductDTOs(string FirmPublicKey) =>
            await httpClientService.GetPostAsync<RequestResult>(FirmPublicKey, GetAddress("GetMyFirmProducts"));

        public async Task<RequestResult> AddStockForProduct(StockCreateModel product) =>
            await httpClientService.GetPostAsync<RequestResult>(product, GetAddress("AddStockForProduct"));

        public async Task<RequestResult> GetStockHistoryDTO(string ProductPublicKey) =>
            await httpClientService.GetPostAsync<RequestResult>(ProductPublicKey, GetAddress("GetStockHistoryDTO"));

        public async Task<RequestResult> DeleteProduct(string ProductPublicKey) =>
            await httpClientService.GetPostAsync<RequestResult>(ProductPublicKey, GetAddress("DeleteProduct"));
    }
}
