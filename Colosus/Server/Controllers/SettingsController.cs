using Colosus.Entity.Concretes;
using Colosus.Server.Facades.Setting;
using Microsoft.AspNetCore.Mvc;
using PaymentTypeCreateModel = Colosus.Entity.Concretes.CreateModel.PaymentType;
using PaymentType = Colosus.Entity.Concretes.DatabaseModel.PaymentType;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.RequestModel;
namespace Colosus.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class SettingsController : Controller
    {
        ISettingFacades settingFacades;
        public SettingsController(ISettingFacades settingFacades)
        {
            this.settingFacades = settingFacades;
        }

        private string GenKey(string keyType, string entityType)
            => settingFacades.guid.Generate(keyType, entityType);


        [HttpPost]
        public string RecommendedPaymentType([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("RecommendedPaymentType");

            settingFacades.operationRunner.ActionRunner(() =>
            {
                List<PaymentType> paymentTypes = settingFacades.operations.RecommendedPaymentType();
                result.Data = settingFacades.dataConverter.Serialize(paymentTypes);

                result.Result = EnumRequestResult.Ok;
                result.Description = "RecommendedPaymentType operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "RecommendedPaymentType operation not success";
            });

            return settingFacades.dataConverter.Serialize(result);
        }

        [HttpPost]
        public string AddPaymentTypeRelation([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("AddPaymentTypeRelation");

            settingFacades.operationRunner.ActionRunner(() =>
            {
                PaymentTypeRequestModel model = settingFacades.dataConverter.Deserialize<PaymentTypeRequestModel>(parameter.Data.ToString());
                Firm firm = settingFacades.operations.GetMyFirmForFirmPublicKey(model.FirmPublicKey);
                PaymentType pytype = settingFacades.operations.GetPaymentType(model.PaymentTypePublicKey);
                PaymentTypeFirmRelation paymentTypeFirmRelation = new()
                {
                    PaymentTypePrivateKey = pytype.PrivateKey,
                    FirmPrivateKey = firm.PrivateKey,
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.PaymentTypeFirmRelation),
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.PaymentTypeFirmRelation),
                };
                settingFacades.operations.SaveEntity(paymentTypeFirmRelation);

                result.Result = EnumRequestResult.Ok;
                result.Description = "AddPaymentTypeRelation operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "AddPaymentTypeRelation operation not success";
            });

            return settingFacades.dataConverter.Serialize(result);
        }


        [HttpPost]
        public string AddPaymentType([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("AddPaymentType");

            settingFacades.operationRunner.ActionRunner(() =>
            {
                PaymentTypeCreateModel paymentType = settingFacades.dataConverter.Deserialize<PaymentTypeCreateModel>(parameter.Data.ToString());
                Firm firm = settingFacades.operations.GetMyFirmForFirmPublicKey(paymentType.FirmPublicKey);

                PaymentType PaymentTypedb = settingFacades.mapping.Convert<PaymentType>(paymentType);
                PaymentTypedb.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.PaymentType);
                PaymentTypedb.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.PaymentType);
                settingFacades.operations.SaveEntity(PaymentTypedb);

                PaymentTypeFirmRelation paymentTypeFirmRelation = new()
                {
                    PaymentTypePrivateKey = PaymentTypedb.PrivateKey,
                    FirmPrivateKey = firm.PrivateKey,
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.PaymentTypeFirmRelation),
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.PaymentTypeFirmRelation),
                };
                settingFacades.operations.SaveEntity(paymentTypeFirmRelation);

                result.Result = EnumRequestResult.Ok;
                result.Description = "AddPaymentType operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "AddPaymentType operation not success";
            });

            return settingFacades.dataConverter.Serialize(result);
        }

        [HttpPost]
        public string DeletePaymentType([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("DeletePaymentType");

            settingFacades.operationRunner.ActionRunner(() =>
            {
                PaymentTypeRequestModel delModel = settingFacades.dataConverter.Deserialize<PaymentTypeRequestModel>(parameter.Data.ToString());
                PaymentTypeFirmRelation relation = settingFacades.operations.GetPaymentTypeFirmRelation(delModel.PaymentTypePublicKey, delModel.FirmPublicKey);
                settingFacades.operations.RemoveEntity(relation);
                result.Result = EnumRequestResult.Ok;
                result.Description = "DeletePaymentType operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "DeletePaymentType operation not success";
            });

            return settingFacades.dataConverter.Serialize(result);
        }

        [HttpPost]
        public string GetAllPaymentTypeForFirmPublicKey([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("GetAllPaymentTypeForFirmPublicKey");
            settingFacades.operationRunner.ActionRunner(() =>
            {
                string FirmPublicKey = settingFacades.dataConverter.Deserialize<string>(parameter.Data.ToString());
                List<PaymentType> paymentTypes = settingFacades.operations.GetAllPaymentTypeForFirmPublicKey(FirmPublicKey);
                List<Colosus.Entity.Concretes.DTO.PaymentType> payTypeDtos= settingFacades.mapping.ConvertToList<Colosus.Entity.Concretes.DTO.PaymentType>(paymentTypes);
                result.Data = settingFacades.dataConverter.Serialize(payTypeDtos);
                result.Result = EnumRequestResult.Ok;
                result.Description = "GetAllPaymentTypeForFirmPublicKey operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "GetAllPaymentTypeForFirmPublicKey operation not success";
            });

            return settingFacades.dataConverter.Serialize(result);
        }

    }
}
