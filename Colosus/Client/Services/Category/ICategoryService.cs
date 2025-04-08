using Colosus.Entity.Concretes;

namespace Colosus.Client.Services.Category
{
    public interface ICategoryService
    {
        public Task<RequestResult> AddCategory(Entity.Concretes.CreateModel.Category category);
        public Task<RequestResult> GetsCategory(string firmPublicKey,int supply);
        public Task<RequestResult> DeleteCategory(string publicKey);

        public Task<RequestResult> GetAllCategory();
    }
}
