using Colosus.Entity.Concretes;

namespace Colosus.Client.Services.Customer
{
    public interface ICustomerService
    {
        public Task<RequestResult> AddIndividualCustomer(Entity.Concretes.CreateModel.IndividualCustomer customer);
        public Task<RequestResult> AddCorporateCustomer(Entity.Concretes.CreateModel.CorporateCustomer customer);
        public Task<RequestResult> GetMyCustomers(string FirmPublicKey);
        public Task<RequestResult> GetCustomerDebtsForCustomerPublicKey(string CustomerPublicKey);
        public Task<RequestResult> AddDebt(Colosus.Entity.Concretes.CreateModel.Debt debt);
        public Task<RequestResult> PayedDebt(string DebtPublicKey); 
        public Task<RequestResult> UnPaidDebt(string DebtPublicKey);
        public Task<RequestResult> DeleteDebt(string DebtPublicKey);
        public Task<RequestResult> DeleteCustomer(string CustomerPublicKey);
        public Task<RequestResult> GetMyUpCommingDebt(string FirmPublicKey);
    }
}
