using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;

namespace Colosus.Client.Blazor.Services.Settings
{
    public interface ISettingsService
    {
        public Task<RequestResult<List<PaymentTypeDTO>>> GetAllPaymentTypeForFirmPublicKey(string FirmPublicKey);
        public Task<RequestResult<List<PaymentTypeDTO>>> RecommendedPaymentType(string FirmPublicKey);
        public Task<RequestResult> AddPaymentType(PaymentTypeCreateModel paymentType);
        public Task<RequestResult> DeletePaymentType(PaymentTypeRequestModel deletePaymentType);
        public Task<RequestResult> AddPaymentTypeRelation(PaymentTypeRequestModel deletePaymentType);




        public Task<RequestResult<List<CurrencyDTO>>> GetAllCurrencyForFirmPublicKey(string FirmPublicKey);
        public Task<RequestResult<List<CurrencyDTO>>> RecommendedCurrency(string FirmPublicKey);
        public Task<RequestResult> AddCurrency(CurrencyCreateModel paymentType);
        public Task<RequestResult> DeleteCurrency(CurrencyRequestModel deletePaymentType);
        public Task<RequestResult> AddCurrencyRelation(CurrencyRequestModel deletePaymentType);
    }
}
