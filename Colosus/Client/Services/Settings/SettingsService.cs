using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DatabaseModel;

namespace Colosus.Client.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        HttpClientService httpClientService;
        public SettingsService(HttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public string GetAddress(string Action) => AppState.GetAddress("Settings", Action);

        public Task<RequestResult> AddPaymentType(Colosus.Entity.Concretes.CreateModel.PaymentType paymentType)
         => httpClientService.GetPostAsync<RequestResult>(paymentType, GetAddress("AddPaymentType"));

        public Task<RequestResult> DeletePaymentType(Colosus.Entity.Concretes.RequestModel.DeletePaymentType paymentType)
         => httpClientService.GetPostAsync<RequestResult>(paymentType, GetAddress("DeletePaymentType"));

        public Task<RequestResult> GetAllPaymentTypeForFirmPublicKey(string FirmPublicKey)
         => httpClientService.GetPostAsync<RequestResult>(FirmPublicKey, GetAddress("GetAllPaymentTypeForFirmPublicKey"));


    }
}
