using Colosus.Server.Facades.Setting;
using Microsoft.AspNetCore.Mvc;
using PaymentTypeCreateModel = Colosus.Entity.Concretes.CreateModel.PaymentTypeCreateModel;
using PaymentType = Colosus.Entity.Concretes.DatabaseModel.PaymentType;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.RequestModel;
using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DTO;
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
        public RequestResult<List<PaymentType>> RecommendedPaymentType([FromBody] RequestParameter parameter)
        {
            RequestResult<List<PaymentType>> result = new("RecommendedPaymentType");
            settingFacades.operationRunner.ActionRunner(() =>
            {
                List<PaymentType> paymentTypes = settingFacades.operations.RecommendedPaymentType();
                result.Data = paymentTypes;

                result.Result = EnumRequestResult.Ok;
                result.Description = "RecommendedPaymentType operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "RecommendedPaymentType operation not success";
            });

            return result;
        }

        [HttpPost]
        public RequestResult AddPaymentTypeRelation([FromBody] RequestParameter<PaymentTypeRequestModel> parameter)
        {
            RequestResult result = new("AddPaymentTypeRelation");

            settingFacades.operationRunner.ActionRunner(() =>
            {

                Firm firm = settingFacades.operations.GetMyFirmWithFirmPublicKey(parameter.Data.FirmPublicKey);
                PaymentType pytype = settingFacades.operations.GetPaymentType(parameter.Data.PaymentTypePublicKey);
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

            return result;
        }


        [HttpPost]
        public RequestResult AddPaymentType([FromBody] RequestParameter<PaymentTypeCreateModel> parameter)
        {
            RequestResult result = new("AddPaymentType");
            settingFacades.operationRunner.ActionRunner(() =>
            {
                Firm firm = settingFacades.operations.GetMyFirmWithFirmPublicKey(parameter.Data.FirmPublicKey);
                PaymentType PaymentTypedb = settingFacades.mapping.Convert<PaymentType>(parameter.Data);
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

            return result;
        }

        [HttpPost]
        public RequestResult DeletePaymentType([FromBody] RequestParameter<PaymentTypeRequestModel> parameter)
        {
            RequestResult result = new("DeletePaymentType");
            settingFacades.operationRunner.ActionRunner(() =>
            {
                PaymentTypeFirmRelation relation = settingFacades.operations.GetCurrencyFirmRelation(parameter.Data.PaymentTypePublicKey, parameter.Data.FirmPublicKey);
                settingFacades.operations.RemoveEntity(relation);
                result.Result = EnumRequestResult.Ok;
                result.Description = "DeletePaymentType operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "DeletePaymentType operation not success";
            });

            return result;
        }

        [HttpPost]
        public RequestResult<List<PaymentTypeDTO>> GetAllPaymentTypeForFirmPublicKey([FromBody] RequestParameter<string> parameter)
        {
            RequestResult<List<PaymentTypeDTO>> result = new("GetAllPaymentTypeForFirmPublicKey");
            settingFacades.operationRunner.ActionRunner(() =>
            {
                Firm firm = settingFacades.operations.GetMyFirmWithFirmPublicKey(parameter.Data);
                List<PaymentType> paymentTypes = settingFacades.operations.GetAllPaymentTypeWithFirmPrivateKey(firm.PrivateKey);
                List<PaymentTypeDTO> payTypeDtos= settingFacades.mapping.ConvertToList<Colosus.Entity.Concretes.DTO.PaymentTypeDTO>(paymentTypes);
                result.Data = payTypeDtos;
                result.Result = EnumRequestResult.Ok;
                result.Description = "GetAllPaymentTypeForFirmPublicKey operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "GetAllPaymentTypeForFirmPublicKey operation not success";
            });

            return result;
        }







        [HttpPost]
        public RequestResult<List<Currency>> RecommendedCurrency([FromBody] RequestParameter parameter)
        {
            RequestResult<List<Currency>> result = new("RecommendedCurrency");
            settingFacades.operationRunner.ActionRunner(() =>
            {
                List<Currency> currencies = settingFacades.operations.RecommendedCurrency();
                result.Data = currencies;
                result.Result = EnumRequestResult.Ok;
                result.Description = "RecommendedCurrency operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "RecommendedCurrency operation not success";
            });

            return result;
        }

        [HttpPost]
        public RequestResult AddCurrencyRelation([FromBody] RequestParameter<CurrencyRequestModel> parameter)
        {
            RequestResult result = new("AddCurrencyRelation");

            settingFacades.operationRunner.ActionRunner(() =>
            {

                Firm firm = settingFacades.operations.GetMyFirmWithFirmPublicKey(parameter.Data.FirmPublicKey);
                Currency currency = settingFacades.operations.GetCurrency(parameter.Data.CurrencyPublicKey);
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

            return result;
        }


        [HttpPost]
        public RequestResult AddCurrency([FromBody] RequestParameter<CurrencyCreateModel> parameter)
        {
            RequestResult result = new("AddCurrency");

            settingFacades.operationRunner.ActionRunner(() =>
            {

                Firm firm = settingFacades.operations.GetMyFirmWithFirmPublicKey(parameter.Data.FirmPublicKey);

                Currency PaymentTypedb = settingFacades.mapping.Convert<Currency>(parameter.Data);
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

            return result;
        }

        [HttpPost]
        public RequestResult DeleteCurrency([FromBody] RequestParameter<CurrencyRequestModel> parameter)
        {
            RequestResult result = new("DeleteCurrency");

            settingFacades.operationRunner.ActionRunner(() =>
            {
            
                PaymentTypeFirmRelation relation = settingFacades.operations.GetCurrencyFirmRelation(parameter.Data.CurrencyPublicKey, parameter.Data.FirmPublicKey);
                settingFacades.operations.RemoveEntity(relation);
                result.Result = EnumRequestResult.Ok;
                result.Description = "DeleteCurrency operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "DeleteCurrency operation not success";
            });

            return result;
        }

        [HttpPost]
        public RequestResult<List<CurrencyDTO>> GetAllCurrencyForFirmPublicKey([FromBody] RequestParameter<string> parameter)
        {
            RequestResult<List<CurrencyDTO>> result = new("GetAllCurrencyForFirmPublicKey");
            settingFacades.operationRunner.ActionRunner(() =>
            {

                Firm firm = settingFacades.operations.GetMyFirmWithFirmPublicKey(parameter.Data);
                List<Currency> paymentTypes = settingFacades.operations.GetAllCurrencyWithFirmPrivateKey(firm.PrivateKey);
                List<CurrencyDTO> currencies = settingFacades.mapping.ConvertToList<Colosus.Entity.Concretes.DTO.CurrencyDTO>(paymentTypes);
                result.Data = currencies;
                result.Result = EnumRequestResult.Ok;
                result.Description = "GetAllCurrencyForFirmPublicKey operation success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "GetAllCurrencyForFirmPublicKey operation not success";
            });

            return result;
        }

    }
}
