using Colosus.Entity.Abstracts;
using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.CreateModel;

namespace Colosus.Client.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        HttpClientService httpClientService;
        public CustomerService(HttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public string GetAddress(string Action) => AppState.GetAddress("Customer", Action);

        public async Task<RequestResult> AddIndividualCustomer(Entity.Concretes.CreateModel.IndividualCustomerCreateModel customer)
            => await httpClientService.GetPostAsync<RequestResult>(customer, GetAddress("AddIndividualCustomer"));

        public async Task<RequestResult> GetMyCustomers(string FirmPublicKey)
            => await httpClientService.GetPostAsync<RequestResult>(FirmPublicKey, GetAddress("GetMyCustomers"));

        public async Task<RequestResult> AddCorporateCustomer(CorporateCustomerCreateModel customer)
            => await httpClientService.GetPostAsync<RequestResult>(customer, GetAddress("AddCorporateCustomer"));

        public async Task<RequestResult> GetCustomerDebtsForCustomerPublicKey(string CustomerPublicKey)
            => await httpClientService.GetPostAsync<RequestResult>(CustomerPublicKey, GetAddress("GetCustomerDebtsForCustomerPublicKey"));

        public async Task<RequestResult> PayedDebt(string DebtPublicKey)
            => await httpClientService.GetPostAsync<RequestResult>(DebtPublicKey, GetAddress("PayedDebt"));

        public async Task<RequestResult> AddDebt(DebtCreateModel debt)
            => await httpClientService.GetPostAsync<RequestResult>(debt, GetAddress("AddCustomerDebt"));

        public async Task<RequestResult> DeleteDebt(string DebtPublicKey)
            => await httpClientService.GetPostAsync<RequestResult>(DebtPublicKey, GetAddress("DeleteDebt"));

        public async Task<RequestResult> UnPaidDebt(string DebtPublicKey)
            => await httpClientService.GetPostAsync<RequestResult>(DebtPublicKey, GetAddress("UnPaidDebt"));

        public async Task<RequestResult> DeleteCustomer(string CustomerPublicKey)
            => await httpClientService.GetPostAsync<RequestResult>(CustomerPublicKey, GetAddress("DeleteCustomer"));

        public async Task<RequestResult> GetMyUpCommingDebt(string FirmPublicKey)
            => await httpClientService.GetPostAsync<RequestResult>(FirmPublicKey, GetAddress("GetMyUpCommingDebt"));

        public async Task<RequestResult> AddDebtPay(DebtPayCreateModel debtPayCreateModel)
            => await httpClientService.GetPostAsync<RequestResult>(debtPayCreateModel, GetAddress("PayedDebt"));

    }
}
