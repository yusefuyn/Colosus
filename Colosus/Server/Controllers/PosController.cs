using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.DTO;
using Colosus.Server.Facades.Pos;
using Microsoft.AspNetCore.Mvc;

namespace Colosus.Server.Controllers
{
    [ApiController]
    [Route("/Api/[controller]/[action]")]
    public class PosController : Controller
    {
        IPosFacades posFacades;
        public PosController(IPosFacades posFacades)
        {
            this.posFacades = posFacades;
        }
        [HttpPost]
        public string GetMyPosDTO([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("GetMyPosDTO");
            posFacades.operationRunner.ActionRunner(() =>
            {
                string FirmPublicKey = posFacades.dataConverter.Deserialize<string>(parameter.Data.ToString());
                Firm myFirm = posFacades.operations.GetMyFirmWithFirmPublicKey(FirmPublicKey);
                PosDTO posDTO = new()
                {
                    Categories = posFacades.mapping.ConvertToList<CategoryDTO>(posFacades.operations.GetCategoriesWithPrivateKey(myFirm.PrivateKey)),
                    Currencies = posFacades.mapping.ConvertToList<CurrencyDTO>(posFacades.operations.GetAllCurrencyWithFirmPrivateKey(myFirm.PrivateKey)),
                    Customers = posFacades.operations.GetMyFirmCustomersForFastOps(myFirm.PrivateKey),
                    PayTypes = posFacades.mapping.ConvertToList<PaymentTypeDTO>(posFacades.operations.GetAllPaymentTypeWithFirmPrivateKey(myFirm.PrivateKey)),
                    Products = posFacades.operations.GetMyProductsDTOsWithFirmPrivateKey(myFirm.PrivateKey)
                };



                result.Data = posFacades.dataConverter.Serialize(posDTO);
                result.Result = EnumRequestResult.Ok;
                result.Description = "GetMyPosDTO operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "GetMyPosDTO operation not success";
            });
            return posFacades.dataConverter.Serialize(result);
        }
    }
}
