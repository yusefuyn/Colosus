using Colosus.Entity.Abstracts;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;
using Colosus.Entity.Concretes.ResultModel;
using Colosus.Server.Facades.Pos;
using Microsoft.AspNetCore.Mvc;

namespace Colosus.Server.Controllers
{
    [ApiController]
    [Route("/Api/[controller]/[action]")]
    public class SaleController : Controller
    {
        ISaleFacades saleFacades;
        public SaleController(ISaleFacades saleFacades)
        {
            this.saleFacades = saleFacades;
        }
        [HttpPost]
        public RequestResult<PosDTO> GetMyPosDTO([FromBody] RequestParameter<PosRequestModel> parameter)
        {
            RequestResult<PosDTO> result = new("GetMyPosDTO");
            saleFacades.operationRunner.ActionRunner(() =>
            {
                Firm myFirm = saleFacades.operations.GetMyFirmWithFirmPublicKey(parameter.Data.FirmPublicKey);
                PosDTO posDTO = new()
                {
                    Categories = saleFacades.mapping.ConvertToList<CategoryDTO>(saleFacades.operations.GetCategoriesWithPrivateKey(myFirm.PrivateKey)),
                    Currencies = saleFacades.mapping.ConvertToList<CurrencyDTO>(saleFacades.operations.GetAllCurrencyWithFirmPrivateKey(myFirm.PrivateKey)),
                    Customer = saleFacades.mapping.Convert<CustomerDTO>(saleFacades.operations.GetMyFirmCustomersWithPublicKey(parameter.Data.CustomerPublicKey)),
                    PayTypes = saleFacades.mapping.ConvertToList<PaymentTypeDTO>(saleFacades.operations.GetAllPaymentTypeWithFirmPrivateKey(myFirm.PrivateKey)),
                    Products = saleFacades.operations.GetMyProductsDTOsWithFirmPrivateKey(myFirm.PrivateKey)
                };



                result.Data = posDTO;
                result.Result = EnumRequestResult.Ok;
                result.Description = "GetMyPosDTO operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "GetMyPosDTO operation not success";
            });
            return result;
        }


        [HttpPost]
        public RequestResult<List<BasicCustomerResultModel>> GetMyCustomersWithFirmPubKey([FromBody] RequestParameter<string> parameter)
        {
            RequestResult<List<BasicCustomerResultModel>> result = new("GetMyCustomersWithFirmPubKey");
            saleFacades.operationRunner.ActionRunner(() =>
            {

                Firm myFirm = saleFacades.operations.GetMyFirmWithFirmPublicKey(parameter.Data);
                List<ICustomer> myCustomers = saleFacades.operations.GetMyFirmSaleCustomersWithFirmPrivateKey(myFirm.PrivateKey);
                List<BasicCustomerResultModel> resModel = saleFacades.mapping.ConvertToList<BasicCustomerResultModel>(myCustomers);
                result.Data = resModel;
                result.Result = EnumRequestResult.Ok;
                result.Description = "GetMyCustomersWithFirmPubKey operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "GetMyCustomersWithFirmPubKey operation not success";
            });
            return result;
        }
    }
}
