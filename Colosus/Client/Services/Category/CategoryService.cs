using Colosus.Entity.Abstracts;
using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.RequestModel;

namespace Colosus.Client.Blazor.Services.Category
{
    public class CategoryService : ICategoryService
    {
        HttpClientService httpClientService;
        public CategoryService(HttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public string GetAddress(string Action) => AppState.GetAddress("Category", Action);
        public async Task<RequestResult> AddCategory(CategoryCreateModel category) =>
            await httpClientService.GetPostAsync(GetAddress("AddCategory"), category);

        public async Task<RequestResult<List<Entity.Concretes.DatabaseModel.Category>>> GetsCategory(string firmPublicKey)
            => await httpClientService.GetPostAsync<List<Entity.Concretes.DatabaseModel.Category>, string>(GetAddress("GetAllCategoriesButSupply"), firmPublicKey);

        public async Task<RequestResult> DeleteCategory(Entity.Concretes.DatabaseModel.Category category) =>
            await httpClientService.GetPostAsync<Entity.Concretes.DatabaseModel.Category>(GetAddress("DeleteCategory"), category);

        public async Task<RequestResult<List<Entity.Concretes.DatabaseModel.Category>>> GetAllCategory() =>
            await httpClientService.GetPostAsync<List<Entity.Concretes.DatabaseModel.Category>>(GetAddress("GetAllCategories"));
    }
}
