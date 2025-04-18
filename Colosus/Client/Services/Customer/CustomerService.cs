using Colosus.Entity.Abstracts;
using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;

namespace Colosus.Client.Blazor.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        HttpClientService httpClientService;
        public CustomerService(HttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public string GetAddress(string Action) => AppState.GetAddress("Customer", Action);

        public async Task<RequestResult> AddIndividualCustomer(IndividualCustomerCreateModel customer)
            => await httpClientService.GetPostAsync(GetAddress("AddIndividualCustomer"), customer);

        public async Task<RequestResult<CustomersDTO>> GetMyCustomers(string FirmPublicKey)
            => await httpClientService.GetPostAsync<CustomersDTO, string>(GetAddress("GetMyCustomers"), FirmPublicKey);

        public async Task<RequestResult> AddCorporateCustomer(CorporateCustomerCreateModel customer)
            => await httpClientService.GetPostAsync(GetAddress("AddCorporateCustomer"), customer);

        public async Task<RequestResult<DebtPageDTO>> GetCustomerDebtsForCustomerPublicKey(string CustomerPublicKey) // TODO : Sınıflandır
            => await httpClientService.GetPostAsync<DebtPageDTO, string>(GetAddress("GetCustomerDebtsForCustomerPublicKey"),  CustomerPublicKey );

        public async Task<RequestResult> PayedDebt(string DebtPublicKey) // TODO : Sınıflandır
            => await httpClientService.GetPostAsync(GetAddress("PayedDebt"), new { PublicKey = DebtPublicKey });

        public async Task<RequestResult> AddDebt(DebtCreateModel debt)
            => await httpClientService.GetPostAsync(GetAddress("AddCustomerDebt"), debt);

        public async Task<RequestResult> DeleteDebt(string DebtPublicKey) // TODO : Sınıflandır
            => await httpClientService.GetPostAsync(GetAddress("DeleteDebt"), new { PublicKey = DebtPublicKey });

        public async Task<RequestResult> UnPaidDebt(string DebtPublicKey)// TODO : Sınıflandır
            => await httpClientService.GetPostAsync(GetAddress("UnPaidDebt"), new { PublicKey = DebtPublicKey });

        public async Task<RequestResult> DeleteCustomer(string CustomerPublicKey)// TODO : Sınıflandır
            => await httpClientService.GetPostAsync(GetAddress("DeleteCustomer"), new { PublicKey = CustomerPublicKey });

        public async Task<RequestResult> GetMyUpCommingDebt(string FirmPublicKey)// TODO : Sınıflandır
            => await httpClientService.GetPostAsync(GetAddress("GetMyUpCommingDebt"), new { PublicKey = FirmPublicKey });

        public async Task<RequestResult> AddDebtPay(DebtPayCreateModel debtPayCreateModel)
            => await httpClientService.GetPostAsync(GetAddress("PayedDebt"), debtPayCreateModel);

        public async Task<RequestResult> DeleteDebtPay(string DebtPayPublicKey)// TODO : Sınıflandır
            => await httpClientService.GetPostAsync(GetAddress("DeleteDebtPay"), new { PublicKey = DebtPayPublicKey });

        public async Task<RequestResult> AddFastCustomer(FastCustomerCreateModel customer)
            => await httpClientService.GetPostAsync(GetAddress("AddFastCustomer"), customer);
    }
}
