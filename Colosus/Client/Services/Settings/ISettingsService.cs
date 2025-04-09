using Colosus.Entity.Concretes;

namespace Colosus.Client.Services.Settings
{
    public interface ISettingsService
    {
        public Task<RequestResult> GetAllPaymentTypeForFirmPublicKey(string FirmPublicKey);
        public Task<RequestResult> RecommendedPaymentType(string FirmPublicKey);
        public Task<RequestResult> AddPaymentType(Colosus.Entity.Concretes.CreateModel.PaymentTypeCreateModel paymentType);
        public Task<RequestResult> DeletePaymentType(Colosus.Entity.Concretes.RequestModel.PaymentTypeRequestModel deletePaymentType);
        public Task<RequestResult> AddPaymentTypeRelation(Colosus.Entity.Concretes.RequestModel.PaymentTypeRequestModel deletePaymentType);




        public Task<RequestResult> GetAllCurrencyForFirmPublicKey(string FirmPublicKey);
        public Task<RequestResult> RecommendedCurrency(string FirmPublicKey);
        public Task<RequestResult> AddCurrency(Colosus.Entity.Concretes.CreateModel.CurrencyCreateModel paymentType);
        public Task<RequestResult> DeleteCurrency(Colosus.Entity.Concretes.RequestModel.CurrencyRequestModel deletePaymentType);
        public Task<RequestResult> AddCurrencyRelation(Colosus.Entity.Concretes.RequestModel.CurrencyRequestModel deletePaymentType);
    }
}
