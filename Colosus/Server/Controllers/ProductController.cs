using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;
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
        public RequestResult AddProduct([FromBody] RequestParameter<ProductCreateModel> parameter)
        {
            RequestResult requestResult = new("AddProduct");
            productFacades.operationRunner.ActionRunner(() =>
            {

                Firm firm = productFacades.operations.GetMyFirmWithFirmPublicKey(parameter.Data.FirmPublicKey);
                var category = productFacades.operations.GetCategory(parameter.Data.CategoryPublicKey);

                Entity.Concretes.DatabaseModel.Product newProduct = new Entity.Concretes.DatabaseModel.Product()
                {
                    Name = parameter.Data.Name,
                    SalePrice = parameter.Data.SalePrice,
                    PurchasePrice = parameter.Data.PurchasePrice,
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
            return requestResult;
        }


        [HttpPost]
        public RequestResult DeleteStock([FromBody] RequestParameter<string> parameter)
        {
            RequestResult requestResult = new("DeleteStock");

            productFacades.operationRunner.ActionRunner(() =>
            {
                ProductStock stock = productFacades.operations.GetProductStockWithPublicKey(parameter.Data);
                productFacades.operations.RemoveEntity(stock);
                requestResult.Result = EnumRequestResult.Ok;
                requestResult.Description = "DeleteStock operations Success";

            }, () =>
            {
                requestResult.Result = EnumRequestResult.Error;
                requestResult.Description = "DeleteStock operations not success";
            });

            return requestResult;
        }

        [HttpPost]
        public RequestResult<List<ProductDTO>> GetMyFirmProducts([FromBody] RequestParameter<string> parameter)
        {
            RequestResult<List<ProductDTO>> requestResult = new("GetMyFirmProducts");

            productFacades.operationRunner.ActionRunner(() =>
            {
                Firm myFirm = productFacades.operations.GetMyFirmWithFirmPublicKey(parameter.Data);
                List<ProductDTO> prods = productFacades.operations.GetMyFirmProductDTOs(myFirm.PrivateKey);
                requestResult.Data = prods;
                requestResult.Result = EnumRequestResult.Ok;
                requestResult.Description = "Success";

            }, () =>
            {
                requestResult.Result = EnumRequestResult.Error;
                requestResult.Description = "Bir hata meydana geldi";
            });

            return requestResult;
        }



        [HttpPost]
        public RequestResult<List<ProductStockDTO>> GetStockHistoryDTO([FromBody] RequestParameter<string> parameter)
        {
            RequestResult<List<ProductStockDTO>> requestResult = new("GetStockHistoryDTO");

            productFacades.operationRunner.ActionRunner(() =>
            {

                List<ProductStockDTO> returnedList = new();
                returnedList = productFacades.operations.GetProductStockHistoryDTOs(parameter.Data);
                requestResult.Result = EnumRequestResult.Ok;
                requestResult.Data = returnedList;
                requestResult.Description = "Success";

            }, () =>
            {
                requestResult.Result = EnumRequestResult.Error;
                requestResult.Description = "Bir hata meydana geldi";
            });

            return requestResult;
        }

        [HttpPost]
        [GetAuthorizeToken]
        public RequestResult<ProductDTO> AddStockForProduct([FromBody] RequestParameter<StockCreateModel> parameter)
        {
            RequestResult<ProductDTO> requestResult = new("AddStockForProduct");

            productFacades.operationRunner.ActionRunner(() =>
            {

                Firm myFirms = productFacades.operations.GetMyFirm(parameter.Token.ToString());

                Product prod = productFacades.operations.GetMyProduct(parameter.Data.ProductPublicKey);

                ProductStock productStock = new();
                productStock.ProductPrivateKey = prod.PrivateKey;
                productStock.FirmPrivateKey = myFirms.PrivateKey;
                productStock.UserPrivateKey = parameter.Token.ToString();
                productStock.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.ProductStock);
                productStock.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.ProductStock);
                productStock.Amount = parameter.Data.Amount;
                productStock.Description = parameter.Data.Description;

                productFacades.operations.SaveEntity(productStock);

                ProductDTO newProd = productFacades.operations.GetMyProductDTOs(prod.PrivateKey);
                requestResult.Data = newProd;

                requestResult.Result = EnumRequestResult.Ok;
                requestResult.Description = "Success";

            }, () =>
            {
                requestResult.Result = EnumRequestResult.Error;
                requestResult.Description = "Bir hata meydana geldi";
            });

            return requestResult;
        }

        [HttpPost]
        public RequestResult DeleteProduct([FromBody] RequestParameter<string> parameter)
        {
            RequestResult requestResult = new("DeleteProduct");
            productFacades.operationRunner.ActionRunner(() =>
            {
                Product prod = productFacades.operations.GetMyProduct(parameter.Data);
                List<ProductStock> stocks = productFacades.operations.GetProductStocks(prod.PrivateKey);
                productFacades.operations.RemoveEntity(prod);
                stocks.ForEach(xd => productFacades.operations.RemoveEntity(xd));
                requestResult.Result = EnumRequestResult.Ok;
                requestResult.Description = "Success";
            }, () =>
            {
                requestResult.Result = EnumRequestResult.Error;
                requestResult.Description = "Bir hata meydana geldi";
            });

            return requestResult;
        }
    }
}
