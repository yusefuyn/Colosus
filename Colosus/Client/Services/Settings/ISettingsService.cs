using Colosus.Entity.Concretes;

namespace Colosus.Client.Services.Settings
{
    public interface ISettingsService
    {
        public Task<RequestResult> GetAllPaymentTypeForFirmPublicKey(string FirmPublicKey); 
        public Task<RequestResult> RecommendedPaymentType(string FirmPublicKey); 
        public Task<RequestResult> AddPaymentType(Colosus.Entity.Concretes.CreateModel.PaymentType paymentType);
        public Task<RequestResult> DeletePaymentType(Colosus.Entity.Concretes.RequestModel.PaymentTypeRequestModel deletePaymentType);
        public Task<RequestResult> AddPaymentTypeRelation(Colosus.Entity.Concretes.RequestModel.PaymentTypeRequestModel deletePaymentType);
    }
}
