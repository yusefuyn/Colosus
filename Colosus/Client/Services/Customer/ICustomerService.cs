using Colosus.Client.Pages.Debt;
using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.CreateModel;

namespace Colosus.Client.Services.Customer
{
    public interface ICustomerService
    {
        public Task<RequestResult> AddIndividualCustomer(IndividualCustomerCreateModel customer);
        public Task<RequestResult> AddCorporateCustomer(CorporateCustomerCreateModel customer);
        public Task<RequestResult> GetMyCustomers(string FirmPublicKey);
        public Task<RequestResult> GetCustomerDebtsForCustomerPublicKey(string CustomerPublicKey);
        public Task<RequestResult> AddDebt(DebtCreateModel debt);
        public Task<RequestResult> PayedDebt(string DebtPublicKey); 
        public Task<RequestResult> UnPaidDebt(string DebtPublicKey);
        public Task<RequestResult> DeleteDebt(string DebtPublicKey);
        public Task<RequestResult> DeleteCustomer(string CustomerPublicKey);
        public Task<RequestResult> GetMyUpCommingDebt(string FirmPublicKey);
        public Task<RequestResult> AddDebtPay(DebtPayCreateModel debtPayCreateModel);
    }
}
