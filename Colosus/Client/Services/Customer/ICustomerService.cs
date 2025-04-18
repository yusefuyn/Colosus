using Colosus.Client.Blazor.Pages.Debt;
using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;

namespace Colosus.Client.Blazor.Services.Customer
{
    public interface ICustomerService
    {
        public Task<RequestResult> AddIndividualCustomer(IndividualCustomerCreateModel customer);
        public Task<RequestResult> AddCorporateCustomer(CorporateCustomerCreateModel customer); 
        public Task<RequestResult> AddFastCustomer(FastCustomerCreateModel customer); 
        public Task<RequestResult<CustomersDTO>> GetMyCustomers(string FirmPublicKey);
        public Task<RequestResult<DebtPageDTO>> GetCustomerDebtsForCustomerPublicKey(string CustomerPublicKey);
        public Task<RequestResult> AddDebt(DebtCreateModel debt);
        public Task<RequestResult> PayedDebt(string DebtPublicKey); 
        public Task<RequestResult> UnPaidDebt(string DebtPublicKey);
        public Task<RequestResult> DeleteDebt(string DebtPublicKey);
        public Task<RequestResult> DeleteCustomer(string CustomerPublicKey);
        public Task<RequestResult> GetMyUpCommingDebt(string FirmPublicKey);
        public Task<RequestResult> AddDebtPay(DebtPayCreateModel debtPayCreateModel);
        public Task<RequestResult> DeleteDebtPay(string DebtPayPublicKey);
    }
}
