using Colosus.Entity.Concretes;
using Colosus.Server.Facades.Setting;
using Microsoft.AspNetCore.Mvc;
using PaymentTypeCreateModel = Colosus.Entity.Concretes.CreateModel.PaymentTypeCreateModel;
using PaymentType = Colosus.Entity.Concretes.DatabaseModel.PaymentType;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.RequestModel;
using Colosus.Entity.Concretes.CreateModel;
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
                PaymentTypeFirmRelation relation = settingFacades.operations.GetCurrencyFirmRelation(delModel.PaymentTypePublicKey, delModel.FirmPublicKey);
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
                List<Colosus.Entity.Concretes.DTO.PaymentTypeDTO> payTypeDtos= settingFacades.mapping.ConvertToList<Colosus.Entity.Concretes.DTO.PaymentTypeDTO>(paymentTypes);
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







        [HttpPost]
        public string RecommendedCurrency([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("RecommendedCurrency");

            settingFacades.operationRunner.ActionRunner(() =>
            {
                List<Currency> currencies = settingFacades.operations.RecommendedCurrency();
                result.Data = settingFacades.dataConverter.Serialize(currencies);

                result.Result = EnumRequestResult.Ok;
                result.Description = "RecommendedCurrency operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "RecommendedCurrency operation not success";
            });

            return settingFacades.dataConverter.Serialize(result);
        }

        [HttpPost]
        public string AddCurrencyRelation([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("AddCurrencyRelation");

            settingFacades.operationRunner.ActionRunner(() =>
            {
                CurrencyRequestModel model = settingFacades.dataConverter.Deserialize<CurrencyRequestModel>(parameter.Data.ToString());
                Firm firm = settingFacades.operations.GetMyFirmForFirmPublicKey(model.FirmPublicKey);
                Currency currency = settingFacades.operations.GetCurrency(model.CurrencyPublicKey);
                CurrencyFirmRelation firmRelation = new()
                {
                    CurrencyPrivateKey = currency.PrivateKey,
                    FirmPrivateKey = firm.PrivateKey,
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.PaymentTypeFirmRelation),
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.PaymentTypeFirmRelation),
                };
                settingFacades.operations.SaveEntity(firmRelation);

                result.Result = EnumRequestResult.Ok;
                result.Description = "AddCurrencyRelation operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "AddCurrencyRelation operation not success";
            });

            return settingFacades.dataConverter.Serialize(result);
        }


        [HttpPost]
        public string AddCurrency([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("AddCurrency");

            settingFacades.operationRunner.ActionRunner(() =>
            {
                CurrencyCreateModel paymentType = settingFacades.dataConverter.Deserialize<CurrencyCreateModel>(parameter.Data.ToString());
                Firm firm = settingFacades.operations.GetMyFirmForFirmPublicKey(paymentType.FirmPublicKey);

                Currency PaymentTypedb = settingFacades.mapping.Convert<Currency>(paymentType);
                PaymentTypedb.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.Currency);
                PaymentTypedb.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.Currency);
                settingFacades.operations.SaveEntity(PaymentTypedb);

                CurrencyFirmRelation currencyFirmRelation = new()
                {
                    CurrencyPrivateKey = PaymentTypedb.PrivateKey,
                    FirmPrivateKey = firm.PrivateKey,
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.PaymentTypeFirmRelation),
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.PaymentTypeFirmRelation),
                };
                settingFacades.operations.SaveEntity(currencyFirmRelation);

                result.Result = EnumRequestResult.Ok;
                result.Description = "AddCurrency operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "AddCurrency operation not success";
            });

            return settingFacades.dataConverter.Serialize(result);
        }

        [HttpPost]
        public string DeleteCurrency([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("DeleteCurrency");

            settingFacades.operationRunner.ActionRunner(() =>
            {
                CurrencyRequestModel delModel = settingFacades.dataConverter.Deserialize<CurrencyRequestModel>(parameter.Data.ToString());
                PaymentTypeFirmRelation relation = settingFacades.operations.GetCurrencyFirmRelation(delModel.CurrencyPublicKey, delModel.FirmPublicKey);
                settingFacades.operations.RemoveEntity(relation);
                result.Result = EnumRequestResult.Ok;
                result.Description = "DeleteCurrency operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "DeleteCurrency operation not success";
            });

            return settingFacades.dataConverter.Serialize(result);
        }

        [HttpPost]
        public string GetAllCurrencyForFirmPublicKey([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("GetAllCurrencyForFirmPublicKey");
            settingFacades.operationRunner.ActionRunner(() =>
            {
                string FirmPublicKey = settingFacades.dataConverter.Deserialize<string>(parameter.Data.ToString());
                List<Currency> paymentTypes = settingFacades.operations.GetAllCurrencyForFirmPublicKey(FirmPublicKey);
                List<Colosus.Entity.Concretes.DTO.CurrencyDTO> currencies = settingFacades.mapping.ConvertToList<Colosus.Entity.Concretes.DTO.CurrencyDTO>(paymentTypes);
                result.Data = settingFacades.dataConverter.Serialize(currencies);
                result.Result = EnumRequestResult.Ok;
                result.Description = "GetAllCurrencyForFirmPublicKey operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "GetAllCurrencyForFirmPublicKey operation not success";
            });

            return settingFacades.dataConverter.Serialize(result);
        }

    }
}
