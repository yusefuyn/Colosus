using Colosus.Entity.Concretes;

namespace Colosus.Client.Services.Settings
{
    public interface ISettingsService
    {
        public Task<RequestResult> GetAllPaymentTypeForFirmPublicKey(string FirmPublicKey);
        public Task<RequestResult> AddPaymentType(Colosus.Entity.Concretes.CreateModel.PaymentType paymentType);
        public Task<RequestResult> DeletePaymentType(Colosus.Entity.Concretes.RequestModel.DeletePaymentType deletePaymentType);
    }
}
