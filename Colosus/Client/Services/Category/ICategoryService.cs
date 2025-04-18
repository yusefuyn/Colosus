using Colosus.Entity.Concretes.RequestModel;

namespace Colosus.Client.Blazor.Services.Category
{
    public interface ICategoryService
    {
        public Task<RequestResult> AddCategory(Entity.Concretes.CreateModel.CategoryCreateModel category);
        public Task<RequestResult<List<Entity.Concretes.DatabaseModel.Category>>> GetsCategory(string firmPublicKey);
        public Task<RequestResult> DeleteCategory(Entity.Concretes.DatabaseModel.Category category);
        public Task<RequestResult<List<Entity.Concretes.DatabaseModel.Category>>> GetAllCategory();
    }
}
