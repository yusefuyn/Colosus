using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;

namespace Colosus.Client.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        HttpClientService httpClientService;
        public SettingsService(HttpClientService httpClientService)
         => this.httpClientService = httpClientService;

        public string GetAddress(string Action) => AppState.GetAddress("Settings", Action);

        public async Task<RequestResult> AddPaymentType(Colosus.Entity.Concretes.CreateModel.PaymentType paymentType)
         => await httpClientService.GetPostAsync<RequestResult>(paymentType, GetAddress("AddPaymentType"));

        public async Task<RequestResult> DeletePaymentType(PaymentTypeRequestModel paymentType)
         => await httpClientService.GetPostAsync<RequestResult>(paymentType, GetAddress("DeletePaymentType"));

        public async Task<RequestResult> GetAllPaymentTypeForFirmPublicKey(string FirmPublicKey)
         => await httpClientService.GetPostAsync<RequestResult>(FirmPublicKey, GetAddress("GetAllPaymentTypeForFirmPublicKey"));

        public async Task<RequestResult> RecommendedPaymentType(string FirmPublicKey)
         => await httpClientService.GetPostAsync<RequestResult>(FirmPublicKey, GetAddress("RecommendedPaymentType"));

        public async Task<RequestResult> AddPaymentTypeRelation(PaymentTypeRequestModel deletePaymentType)
         => await httpClientService.GetPostAsync<RequestResult>(deletePaymentType, GetAddress("AddPaymentTypeRelation"));

        public async Task<RequestResult> GetAllCurrencyForFirmPublicKey(string FirmPublicKey)
         => await httpClientService.GetPostAsync<RequestResult>(FirmPublicKey, GetAddress("GetAllCurrencyForFirmPublicKey"));


        public async Task<RequestResult> RecommendedCurrency(string FirmPublicKey)
         => await httpClientService.GetPostAsync<RequestResult>(FirmPublicKey, GetAddress("RecommendedCurrency"));


        public async Task<RequestResult> AddCurrency(CurrencyCreateModel Currency)
         => await httpClientService.GetPostAsync<RequestResult>(Currency, GetAddress("AddCurrency"));


        public async Task<RequestResult> DeleteCurrency(CurrencyRequestModel deleteCurrency)
         => await httpClientService.GetPostAsync<RequestResult>(deleteCurrency, GetAddress("DeleteCurrency"));


        public async Task<RequestResult> AddCurrencyRelation(CurrencyRequestModel addCurrency)
         => await httpClientService.GetPostAsync<RequestResult>(addCurrency, GetAddress("AddCurrencyRelation"));

    }
}
