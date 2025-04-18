using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;
using System.Collections.Generic;

namespace Colosus.Client.Blazor.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        HttpClientService httpClientService;
        public SettingsService(HttpClientService httpClientService)
         => this.httpClientService = httpClientService;

        public string GetAddress(string Action) => AppState.GetAddress("Settings", Action);

        public async Task<RequestResult> AddPaymentType(Colosus.Entity.Concretes.CreateModel.PaymentTypeCreateModel paymentType)
         => await httpClientService.GetPostAsync(GetAddress("AddPaymentType"), paymentType);

        public async Task<RequestResult> DeletePaymentType(PaymentTypeRequestModel paymentType)
         => await httpClientService.GetPostAsync(GetAddress("DeletePaymentType"), paymentType);

        public async Task<RequestResult<List<PaymentTypeDTO>>> GetAllPaymentTypeForFirmPublicKey(string FirmPublicKey)
         => await httpClientService.GetPostAsync<List<PaymentTypeDTO>, string>(GetAddress("GetAllPaymentTypeForFirmPublicKey"), FirmPublicKey);

        public async Task<RequestResult<List<PaymentTypeDTO>>> RecommendedPaymentType(string FirmPublicKey)
         => await httpClientService.GetPostAsync<List<PaymentTypeDTO>, string>(GetAddress("RecommendedPaymentType"), FirmPublicKey);

        public async Task<RequestResult> AddPaymentTypeRelation(PaymentTypeRequestModel deletePaymentType)
         => await httpClientService.GetPostAsync(GetAddress("AddPaymentTypeRelation"), deletePaymentType);

        public async Task<RequestResult<List<CurrencyDTO>>> GetAllCurrencyForFirmPublicKey(string FirmPublicKey)
         => await httpClientService.GetPostAsync<List<CurrencyDTO>, string>(GetAddress("GetAllCurrencyForFirmPublicKey"), FirmPublicKey);


        public async Task<RequestResult<List<CurrencyDTO>>> RecommendedCurrency(string FirmPublicKey)
         => await httpClientService.GetPostAsync<List<CurrencyDTO>, string>(GetAddress("RecommendedCurrency"), FirmPublicKey);


        public async Task<RequestResult> AddCurrency(CurrencyCreateModel Currency)
         => await httpClientService.GetPostAsync(GetAddress("AddCurrency"), Currency);


        public async Task<RequestResult> DeleteCurrency(CurrencyRequestModel deleteCurrency)
         => await httpClientService.GetPostAsync(GetAddress("DeleteCurrency"), deleteCurrency);


        public async Task<RequestResult> AddCurrencyRelation(CurrencyRequestModel addCurrency)
         => await httpClientService.GetPostAsync(GetAddress("AddCurrencyRelation"), addCurrency);
    }
}
