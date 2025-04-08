using Colosus.Business.Abstracts;
using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Operations.Abstracts;
using Colosus.Server.Attributes;
using Colosus.Server.Facades.Category;
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
                cat.PrivateKey = categoryFacades.guid.Generate(KeyTypes.PrivateKey, KeyTypes.Category);
                cat.PublicKey = categoryFacades.guid.Generate(KeyTypes.PublicKey, KeyTypes.Category);
                categoryFacades.operations.SaveEntity(cat);

                CategoryFirmRelation categoryFirmRelation = new()
                {
                    CategoryPrivateKey = cat.PrivateKey,
                    FirmPrivateKey = firm.PrivateKey,
                    PrivateKey = categoryFacades.guid.Generate(KeyTypes.PrivateKey, KeyTypes.CategoryFirmRelation),
                    PublicKey = categoryFacades.guid.Generate(KeyTypes.PublicKey, KeyTypes.CategoryFirmRelation)
                };
                categoryFacades.operations.SaveEntity(categoryFirmRelation);
                result.Result = EnumRequestResult.Ok;
                result.Description = "Added Category";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "Not Added Category";
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
            RequestResult result = new($"Delete Category {parameter.Data.ToString()}");

            categoryFacades.operationRunner.ActionRunner(() =>
            {
                string categoryKey = categoryFacades.dataConverter.Deserialize<string>(parameter.Data);
                categoryFacades.operations.DeleteCategory(categoryKey);
                result.Result = EnumRequestResult.Ok;
                result.Description = "Deletted Category";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "Not Delete Category ";
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
                result.Description = "GetAllCategories";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "GetAllCategories";
            });
            return categoryFacades.dataConverter.Serialize(result);
        }


    }
}
