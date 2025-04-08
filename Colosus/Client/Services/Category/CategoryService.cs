using Colosus.Entity.Concretes;

namespace Colosus.Client.Services.Category
{
    public class CategoryService : ICategoryService
    {
        HttpClientService httpClientService;
        public CategoryService(HttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public string GetAddress(string Action) => AppState.GetAddress("Category", Action);
        public async Task<RequestResult> AddCategory(Entity.Concretes.CreateModel.Category category) =>
            await httpClientService.GetPostAsync<RequestResult>(category, GetAddress("AddCategory"));

        public async Task<RequestResult> GetsCategory(string firmPublicKey, int Supply) =>
            await httpClientService.GetPostAsync<RequestResult>(new RequestParameter() { Data = firmPublicKey, Supply = Supply, Address = GetAddress("GetAllCategoriesButSupply") });

        public async Task<RequestResult> DeleteCategory(string publicKey) =>
            await httpClientService.GetPostAsync<RequestResult>(publicKey, GetAddress("DeleteCategory"));

        public async Task<RequestResult> GetAllCategory() =>
            await httpClientService.GetPostAsync<RequestResult>("", GetAddress("GetAllCategories"));

    }
}
