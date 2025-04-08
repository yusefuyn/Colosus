using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.DTO;
using Colosus.Server.Attributes;
using Colosus.Server.Facades.Product;
using Colosus.Server.Facades.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Colosus.Server.Controllers
{
    [ApiController]
    [Route("/Api/[controller]/[action]")]
    public class ProductController : Controller
    {
        IProductFacades productFacades;
        public ProductController(IProductFacades productFacades)
        {
            this.productFacades = productFacades;
        }

        private string GenKey(string keyType, string entityType)
            => productFacades.guid.Generate(keyType, entityType);

        [HttpPost]
        [GetAuthorizeToken]
        public string AddProduct([FromBody] RequestParameter parameter)
        {
            RequestResult requestResult = new("AddProduct");
            productFacades.operationRunner.ActionRunner(() =>
            {
                Entity.Concretes.CreateModel.Product parameterObj = productFacades.dataConverter.Deserialize<Entity.Concretes.CreateModel.Product>(parameter.Data);
                Firm firm = productFacades.operations.GetMyFirmForFirmPublicKey(parameterObj.FirmPublicKey);
                var category = productFacades.operations.GetCategory(parameterObj.CategoryPublicKey);

                Entity.Concretes.DatabaseModel.Product newProduct = new Entity.Concretes.DatabaseModel.Product()
                {
                    Name = parameterObj.Name,
                    SalePrice = parameterObj.SalePrice,
                    PurchasePrice = parameterObj.PurchasePrice,
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.Product),
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.Product)
                };

                productFacades.operations.SaveEntity(newProduct);

                ProductCategoryRelation relation = new()
                {
                    ProductPrivateKey = newProduct.PrivateKey,
                    CategoryPrivateKey = category.PrivateKey,
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.ProductCategoryRelation),
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.ProductCategoryRelation)
                };

                productFacades.operations.SaveEntity(relation);


                ProductFirmRelation productFirmRelation = new ProductFirmRelation()
                {
                    FirmPrivateKey = firm.PrivateKey,
                    ProductPrivateKey = newProduct.PrivateKey,
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.ProductFirmRelation),
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.ProductFirmRelation),
                };
                productFacades.operations.SaveEntity(productFirmRelation);

                requestResult.Result = EnumRequestResult.Ok;
                requestResult.Description = "Başarıyla eklendi.";

            }, () =>
            {
                requestResult.Result = EnumRequestResult.Error;
                requestResult.Description = "Bir hata meydana geldi";
            });
            return productFacades.dataConverter.Serialize(requestResult);
        }



        [HttpPost]
        [GetAuthorizeToken]
        public string GetMyFirmProducts([FromBody] RequestParameter parameter)
        {
            RequestResult requestResult = new("GetMyFirmProducts");

            productFacades.operationRunner.ActionRunner(() =>
            {

                string FirmPublicKey = productFacades.dataConverter.Deserialize<string>(parameter.Data);
                Firm myFirm = productFacades.operations.GetMyFirmForFirmPublicKey(FirmPublicKey);
                List<Entity.Concretes.DTO.Product> prods = productFacades.operations.GetMyFirmProductDTOs(myFirm.PrivateKey);
                requestResult.Data = productFacades.dataConverter.Serialize(prods);
                requestResult.Result = EnumRequestResult.Ok;
                requestResult.Description = "Success";

            }, () =>
            {
                requestResult.Result = EnumRequestResult.Error;
                requestResult.Description = "Bir hata meydana geldi";
            });

            return productFacades.dataConverter.Serialize(requestResult);
        }



        [HttpPost]
        [GetAuthorizeToken]
        public string GetStockHistoryDTO([FromBody] RequestParameter parameter)
        {
            RequestResult requestResult = new("GetStockHistoryDTO");

            productFacades.operationRunner.ActionRunner(() =>
            {
                string ProductPublicKey = productFacades.dataConverter.Deserialize<string>(parameter.Data.ToString());
                List<Colosus.Entity.Concretes.DTO.ProductStock> returnedList = new();
                returnedList = productFacades.operations.GetProductStockHistoryDTOs(ProductPublicKey);
                requestResult.Result = EnumRequestResult.Ok;
                requestResult.Data = productFacades.dataConverter.Serialize(returnedList);
                requestResult.Description = "Success";

            }, () =>
            {
                requestResult.Result = EnumRequestResult.Error;
                requestResult.Description = "Bir hata meydana geldi";
            });

            return productFacades.dataConverter.Serialize(requestResult);
        }

        [HttpPost]
        [GetAuthorizeToken]
        public string AddStockForProduct([FromBody] RequestParameter parameter)
        {
            RequestResult requestResult = new("AddStockForProduct");

            productFacades.operationRunner.ActionRunner(() =>
            {

                Firm myFirms = productFacades.operations.GetMyFirm(parameter.Token.ToString());
                Entity.Concretes.CreateModel.Stock parameterStock = productFacades.dataConverter.Deserialize<Entity.Concretes.CreateModel.Stock>(parameter.Data);
                Entity.Concretes.DatabaseModel.Product prod = productFacades.operations.GetMyProduct(parameterStock.ProductPublicKey);

                Entity.Concretes.DatabaseModel.ProductStock productStock = new();
                productStock.ProductPrivateKey = prod.PrivateKey;
                productStock.FirmPrivateKey = myFirms.PrivateKey;
                productStock.UserPrivateKey = parameter.Token.ToString();
                productStock.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.ProductStock);
                productStock.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.ProductStock);
                productStock.Amount = parameterStock.Amount;
                productStock.Description = parameterStock.Description;

                productFacades.operations.SaveEntity(productStock);

                Entity.Concretes.DTO.Product newProd = productFacades.operations.GetMyProductDTOs(prod.PrivateKey);
                requestResult.Data = productFacades.dataConverter.Serialize(newProd);

                requestResult.Result = EnumRequestResult.Ok;
                requestResult.Description = "Success";

            }, () =>
            {
                requestResult.Result = EnumRequestResult.Error;
                requestResult.Description = "Bir hata meydana geldi";
            });

            return productFacades.dataConverter.Serialize(requestResult);
        }




        [HttpPost]
        [GetAuthorizeToken]
        public string DeleteProduct([FromBody] RequestParameter parameter)
        {
            RequestResult requestResult = new("DeleteProduct");

            productFacades.operationRunner.ActionRunner(() =>
            {

                string productPublicKey = productFacades.dataConverter.Deserialize<string>(parameter.Data.ToString());
                Colosus.Entity.Concretes.DatabaseModel.Product prod = productFacades.operations.GetMyProduct(productPublicKey);
                List<Colosus.Entity.Concretes.DatabaseModel.ProductStock> stocks = productFacades.operations.GetProductStocks(prod.PrivateKey);

                productFacades.operations.RemoveEntity(prod);
                stocks.ForEach(xd => productFacades.operations.RemoveEntity(xd));
                requestResult.Result = EnumRequestResult.Ok;
                requestResult.Description = "Success";

            }, () =>
            {
                requestResult.Result = EnumRequestResult.Error;
                requestResult.Description = "Bir hata meydana geldi";
            });

            return productFacades.dataConverter.Serialize(requestResult);
        }
    }
}
