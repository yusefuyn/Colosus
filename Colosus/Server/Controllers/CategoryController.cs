using Colosus.Business.Abstracts;
using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.RequestModel;
using Colosus.Operations.Abstracts;
using Colosus.Server.Attributes;
using Colosus.Server.Facades.Category;
using Colosus.Server.Facades.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Colosus.Server.Controllers
{
    [ApiController]
    [Route("/Api/[controller]/[action]")]
    public class CategoryController : Controller
    {
        ICategoryFacades categoryFacades;
        public CategoryController(ICategoryFacades categoryFacades)
        {
            this.categoryFacades = categoryFacades;
        }

        private string GenKey(string keyType, string entityType)
    => categoryFacades.guid.Generate(keyType, entityType);

        [HttpPost]
        [GetAuthorizeToken]
        public RequestResult AddCategory([FromBody] RequestParameter<CategoryCreateModel> parameter)
        {
            RequestResult result = new("Add Category");
            categoryFacades.operationRunner.ActionRunner(() =>
            {
                Firm firm = categoryFacades.operations.GetMyFirmWithFirmPublicKey(parameter.Data.FirmPublicKey);
                Category cat = categoryFacades.mapping.Convert<Category>(parameter.Data);
                cat.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.Category);
                cat.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.Category);
                categoryFacades.operations.SaveEntity(cat);

                CategoryFirmRelation categoryFirmRelation = new()
                {
                    CategoryPrivateKey = cat.PrivateKey,
                    FirmPrivateKey = firm.PrivateKey,
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.CategoryFirmRelation),
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.CategoryFirmRelation)
                };
                categoryFacades.operations.SaveEntity(categoryFirmRelation);
                result.Result = EnumRequestResult.Ok;
                result.Description = "AddCategory Operations Success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "AddCategory Operations not Success";
            });
            return result;
        }



        [HttpPost]
        public RequestResult<List<Category>> GetAllCategoriesButSupply([FromBody] RequestParameter<string> parameter)
        {
            RequestResult<List<Category>> result = new("GetAllCategoriesButSupply");
            categoryFacades.operationRunner.ActionRunner(() =>
            {
                string FirmpublicKey = parameter.Data.ToString();
                Firm firm = categoryFacades.operations.GetMyFirmWithFirmPublicKey(FirmpublicKey);
                result.Data = categoryFacades.operations.GetCategoriesWithPrivateKey(firm.PrivateKey);
                result.Result = EnumRequestResult.Ok;
                result.Description = "GetAllCategoriesButSupply Operation Success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "GetAllCategoriesButSupply Operation Failed";
            });
            return result;
        }
        [HttpPost]
        public RequestResult DeleteCategory([FromBody] RequestParameter<Category> parameter)
        {
            RequestResult result = new($"DeleteCategory Category {parameter.Data.PublicKey.ToString()}");

            categoryFacades.operationRunner.ActionRunner(() =>
            {
                List<Product> prod = categoryFacades.operations.GetMyProductWithCategoryPrivateKey(parameter.Data.PrivateKey);
                categoryFacades.operations.RemoveEntity(parameter.Data);
                result.Result = EnumRequestResult.Ok;
                result.Description = "DeleteCategory Operations Success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "DeleteCategory Operations not Success";
            });
            return result;
        }

        [HttpPost]
        public RequestResult<List<Category>> GetAllCategories([FromBody] RequestParameter parameter)
        {
            RequestResult<List<Category>> result = new($"GetAllCategories");

            categoryFacades.operationRunner.ActionRunner(() =>
            {
                result.Data = categoryFacades.operations.GetAllCategories();
                result.Result = EnumRequestResult.Ok;
                result.Description = "GetAllCategories Operations Success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "GetAllCategories not Operations Success";
            });
            return result;
        }


    }
}
