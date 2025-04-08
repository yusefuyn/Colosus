using Colosus.Entity.Abstracts;
using Colosus.Entity.Concretes.DatabaseModel;
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
        List<Category> GetCategories(string FirmPrivateKey,int skip, int take);
        Category GetCategory(string categoryPublicKey);
        Firm GetMyFirm(string UserPrivateKey);
        List<IndividualCustomer> GetMyFirmIndividualCustomers(string firmPublicKey);
        Firm GetMyFirmForFirmPublicKey(string firmpublicKey);
        List<Colosus.Entity.Concretes.DTO.Product> GetMyFirmProductDTOs(string firmPrivateKey);
        Product GetMyProduct(string productPublicKey);
        Entity.Concretes.DTO.Product GetMyProductDTOs(string privateKey);
        List<Entity.Concretes.DTO.ProductStock> GetProductStockHistoryDTOs(string productPublicKey);
        List<ProductStock> GetProductStocks(string privateKey);
        List<Firm> GetsMyFirms(string UserPrivateKey);
        public User GetUser(string userName, string password);
        List<Role> GetUserRole(string privateKey);
        void RemoveEntity(object entity);
        public void SaveEntity(object entity);
        List<CorporateCustomer> GetMyFirmCorporateCustomers(string firmPublicKey);
        Entity.Concretes.DTO.Customers GetMyFirmCustomers(string firmPublicKey);
        public ICustomer GetICustomerFromCustomerPublicKey(string customerPublicKey);
        List<Debt> GetsDebitForCustomerKey(string customerKey);
        Debt GetDebt(string debtPublicKey);
        void UpdateEntity(object entity);
        PaymentTypeFirmRelation GetPaymentTypeFirmRelation(string paymentTypePublicKey, string firmPublicKey);
        PaymentType GetPaymentType(object paymentTypePublicKey);
        List<PaymentType> GetAllPaymentTypeForFirmPublicKey(string firmPublicKey);
    }
}
