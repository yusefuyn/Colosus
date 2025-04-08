using Colosus.Business.Abstracts;
using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.DatabaseModel;
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
        public string AddCategory([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("Add Category");
            categoryFacades.operationRunner.ActionRunner(() =>
            {
                Colosus.Entity.Concretes.CreateModel.Category paramCat = categoryFacades.dataConverter.Deserialize<Colosus.Entity.Concretes.CreateModel.Category>(parameter.Data);
                Firm firm = categoryFacades.operations.GetMyFirmForFirmPublicKey(paramCat.FirmPublicKey);
                Category cat = categoryFacades.mapping.Convert<Category>(paramCat);
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
            return categoryFacades.dataConverter.Serialize(result);
        }



        [HttpPost]
        public string GetAllCategoriesButSupply([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("GetAllCategoriesButSupply");
            categoryFacades.operationRunner.ActionRunner(() =>
            {
                string FirmpublicKey = parameter.Data.ToString();
                Firm firm = categoryFacades.operations.GetMyFirmForFirmPublicKey(FirmpublicKey);
                List<Category> categories = categoryFacades.operations.GetCategories(firm.PrivateKey, (Convert.ToInt32(parameter.Supply) - 1) * 20, 20);
                result.Data = categoryFacades.dataConverter.Serialize(categories);
                result.Result = EnumRequestResult.Ok;
                result.Description = "GetAllCategoriesButSupply Operation Success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "GetAllCategoriesButSupply Operation Failed";
            });
            return categoryFacades.dataConverter.Serialize(result);
        }
        [HttpPost]
        public string DeleteCategory([FromBody] RequestParameter parameter)
        {
            RequestResult result = new($"DeleteCategory Category {parameter.Data.ToString()}");

            categoryFacades.operationRunner.ActionRunner(() =>
            {
                string categoryPublicKey = categoryFacades.dataConverter.Deserialize<string>(parameter.Data);
                var cat = categoryFacades.operations.GetCategory(categoryPublicKey);
                categoryFacades.operations.RemoveEntity(cat);
                result.Result = EnumRequestResult.Ok;
                result.Description = "DeleteCategory Operations Success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "DeleteCategory Operations not Success";
            });
            return categoryFacades.dataConverter.Serialize(result);
        }

        [HttpPost]
        public string GetAllCategories([FromBody] RequestParameter parameter)
        {
            RequestResult result = new($"GetAllCategories");

            categoryFacades.operationRunner.ActionRunner(() =>
            {
                List<Category> res = categoryFacades.operations.GetAllCategories();
                result.Data = categoryFacades.dataConverter.Serialize(res);
                result.Result = EnumRequestResult.Ok;
                result.Description = "GetAllCategories Operations Success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "GetAllCategories not Operations Success";
            });
            return categoryFacades.dataConverter.Serialize(result);
        }


    }
}
