using Colosus.Entity.Abstracts;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Colosus.Business.Operations;

namespace Colosus.Business.Abstracts
{
    public interface IOperations
    {

        public bool DatabaseUpdate();
        List<Category> GetAllCategories();
        List<Category> GetCategoriesWithPrivateKey(string FirmPrivateKey);
        Category GetCategory(string categoryPublicKey);
        Firm GetMyFirm(string UserPrivateKey);
        Firm GetMyFirmWithFirmPublicKey(string firmpublicKey);
        List<ProductDTO> GetMyFirmProductDTOs(string firmPrivateKey);
        Product GetMyProduct(string productPublicKey);
        ProductDTO GetMyProductDTOs(string privateKey);
        List<ProductStockDTO> GetProductStockHistoryDTOs(string productPublicKey);
        List<ProductStock> GetProductStocks(string privateKey);
        List<Firm> GetsMyFirms(string UserPrivateKey);
        public User GetUser(string userName, string password);
        List<Role> GetUserRole(string privateKey);
        void RemoveEntity(object entity);
        public void SaveEntity(object entity);
        List<CorporateCustomer> GetMyFirmCorporateCustomersWithFirmPrivateKey(string firmPublicKey);
        List<IndividualCustomer> GetMyFirmIndividualCustomersWithFirmPrivateKey(string firmPrivateKey);
        List<FastCustomer> GetMyFirmFastCustomersWithFirmPrivateKey(string firmPrivateKey);

        List<FastCustomer> GetMyFirmFastCustomersForFastOps(string firmPrivateKey);
        List<IndividualCustomer> GetMyFirmIndividualCustomersForFastOps(string firmPrivateKey);
        List<CorporateCustomer> GetMyFirmCorporateCustomersForFastOps(string firmPrivateKey);


        CustomersDTO GetMyFirmCustomersWithPrivateKey(string firmPrivateKey);
        public ICustomer GetICustomerFromCustomerPublicKey(string customerPublicKey);
        List<Debt> GetsDebitWithCustomerKey(string customerKey);
        Debt GetDebt(string debtPublicKey);
        void UpdateEntity(object entity);
        PaymentTypeFirmRelation GetCurrencyFirmRelation(string paymentTypePublicKey, string firmPublicKey);
        PaymentType GetPaymentType(string paymentTypePublicKey);
        List<PaymentType> GetAllPaymentTypeWithFirmPrivateKey(string firmPrivateKey);
        List<Product> GetMyProductWithCategoryPrivateKey(string privateKey);
        List<PaymentType> RecommendedPaymentType();
        List<Currency> RecommendedCurrency();
        Currency GetCurrency(string currencyPublicKey);
        List<Currency> GetAllCurrencyWithFirmPrivateKey(string firmPrivateKey);
        List<DebtPayDTO> GetDebtPayWithDebtPrivateKey(string privateKey);
        ProductStock GetProductStockWithPublicKey(string stockPublicKey);
        DebtPay GetDebtPayWithPublicKey(string debtPayPublicKey);
        List<ProductDTO> GetMyProductsDTOsWithFirmPrivateKey(string firmPrivateKey);
        CustomersDTO GetMyFirmCustomersForFastOps(string privateKey);
        ICustomer GetMyFirmCustomerWithCustomerKey(string CustomerKey);
        List<ICustomer> GetMyFirmSaleCustomersWithFirmPrivateKey(string privateKey);
        ICustomer GetMyFirmCustomersWithPublicKey(string customerPublicKey);
    }
}
