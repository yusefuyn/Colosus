using Colosus.Client;
using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Server.Attributes;
using Colosus.Server.Facades.Customer;
using Microsoft.AspNetCore.Mvc;
using iCustomer = Colosus.Entity.Concretes.CreateModel.IndividualCustomer;
using cCustomer = Colosus.Entity.Concretes.CreateModel.CorporateCustomer;
using Colosus.Entity.Abstracts;
namespace Colosus.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class CustomerController : Controller
    {
        ICustomerFacades customerFacades;
        public CustomerController(ICustomerFacades facades)
        {
            customerFacades = facades;
        }

        [HttpPost]
        [GetAuthorizeToken]
        public string AddCustomerDebt([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("AddCustomerDebts");
            customerFacades.operationRunner.ActionRunner(() =>
            {
                Colosus.Entity.Concretes.CreateModel.Debt debt = customerFacades.dataConverter.Deserialize<Colosus.Entity.Concretes.CreateModel.Debt>(parameter.Data);
                ICustomer customer = customerFacades.operations.GetICustomerFromCustomerPublicKey(debt.CustomerPublicKey);
                Debt res = customerFacades.mapping.Convert<Debt>(debt);
                res.CustomerKey = customer.CustomerKey;
                res.UserPrivateKey = parameter.Token.ToString();
                res.PrivateKey = customerFacades.guid.Generate(KeyTypes.PrivateKey, KeyTypes.Debt);
                res.PublicKey = customerFacades.guid.Generate(KeyTypes.PublicKey, KeyTypes.Debt);
                customerFacades.operations.SaveEntity(res);
                result.Result = EnumRequestResult.Ok;
                result.Description = "AddCustomerDebts Operations Success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "AddCustomerDebts Operations Not Success";
            });
            return customerFacades.dataConverter.Serialize(result);
        }


        [HttpPost]
        [GetAuthorizeToken]
        public string DeleteCustomer([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("DeleteCustomer");
            customerFacades.operationRunner.ActionRunner(() =>
            {
                string CustomerPublicKey = customerFacades.dataConverter.Deserialize<string>(parameter.Data);
                ICustomer customer = customerFacades.operations.GetICustomerFromCustomerPublicKey(CustomerPublicKey);
                customerFacades.operations.RemoveEntity(customer);
                List<Debt> debts = customerFacades.operations.GetsDebitForCustomerKey(customer.CustomerKey);
                debts.ForEach(xd => customerFacades.operations.RemoveEntity(xd));
                result.Result = EnumRequestResult.Ok;
                result.Description = "DeleteCustomer Operations Success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "DeleteCustomer Operations Not Success";
            });
            return customerFacades.dataConverter.Serialize(result);
        }

        [HttpPost]
        [GetAuthorizeToken]
        public string DeleteDebt([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("DeleteDebt");
            customerFacades.operationRunner.ActionRunner(() =>
            {
                string DebtPublicKey = customerFacades.dataConverter.Deserialize<string>(parameter.Data.ToString());

                Debt debt = customerFacades.operations.GetDebt(DebtPublicKey);
                debt.Payed = true;
                customerFacades.operations.RemoveEntity(debt);
                result.Result = EnumRequestResult.Ok;
                result.Description = "DeleteDebt Operations Success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "DeleteDebt Operations Not Success";
            });
            return customerFacades.dataConverter.Serialize(result);
        }


        [HttpPost]
        [GetAuthorizeToken]
        public string PayedDebt([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("PayedDebt");

            customerFacades.operationRunner.ActionRunner(() =>
            {
                string DebtPublicKey = customerFacades.dataConverter.Deserialize<string>(parameter.Data.ToString());

                Debt debt = customerFacades.operations.GetDebt(DebtPublicKey);
                debt.Payed = true;
                customerFacades.operations.UpdateEntity(debt);
                result.Result = EnumRequestResult.Ok;
                result.Description = "PayedDebt Operations Success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "PayedDebt Operations Not Success";
            });

            return customerFacades.dataConverter.Serialize(result);
        }



        [HttpPost]
        [GetAuthorizeToken]
        public string UnPaidDebt([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("UnPaidDebt");

            customerFacades.operationRunner.ActionRunner(() =>
            {
                string DebtPublicKey = customerFacades.dataConverter.Deserialize<string>(parameter.Data.ToString());

                Debt debt = customerFacades.operations.GetDebt(DebtPublicKey);
                debt.Payed = false;
                customerFacades.operations.UpdateEntity(debt);
                result.Result = EnumRequestResult.Ok;
                result.Description = "UnPaidDebt Operations Success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "UnPaidDebt Operations Not Success";
            });

            return customerFacades.dataConverter.Serialize(result);
        }


        [HttpPost]
        [GetAuthorizeToken]
        public string GetCustomerDebtsForCustomerPublicKey([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("GetCustomerDebtsForCustomerPublicKey");

            customerFacades.operationRunner.ActionRunner(() =>
            {
                string CustomerPublicKey = customerFacades.dataConverter.Deserialize<string>(parameter.Data.ToString());
                ICustomer customer = customerFacades.operations.GetICustomerFromCustomerPublicKey(CustomerPublicKey);
                Colosus.Entity.Concretes.DTO.Debt debts = new();

                debts.Debts = customerFacades.operations.GetsDebitForCustomerKey(customer.CustomerKey);
                debts.CustomerName = customer.GetName();
                debts.CustomerPublicKey = CustomerPublicKey;

                result.Data = customerFacades.dataConverter.Serialize(debts);
                result.Result = EnumRequestResult.Ok;
                result.Description = "GetCustomerDebtsForCustomerPublicKey Operations Success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "GetCustomerDebtsForCustomerPublicKey Operations Not Success";
            });

            return customerFacades.dataConverter.Serialize(result);
        }


        [HttpPost]
        [GetAuthorizeToken]
        public string AddIndividualCustomer([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("AddIndividualCustomer");
            customerFacades.operationRunner.ActionRunner(() =>
            {
                iCustomer icustomer = customerFacades.dataConverter.Deserialize<iCustomer>(parameter.Data);
                Firm firm = customerFacades.operations.GetMyFirmForFirmPublicKey(icustomer.FirmPublicKey);
                IndividualCustomer customer = customerFacades.mapping.Convert<IndividualCustomer>(icustomer);
                List<ContactAddress> contactAddresses = customerFacades.mapping.ConvertToList<ContactAddress>(icustomer.ContactAddresses);
                customer.PrivateKey = customerFacades.guid.Generate(KeyTypes.PrivateKey, KeyTypes.IndividualCustomer);
                customer.PublicKey = customerFacades.guid.Generate(KeyTypes.PublicKey, KeyTypes.IndividualCustomer);
                customer.CustomerKey = customerFacades.guid.Generate(KeyTypes.Key, KeyTypes.Customer);
                customer.ContactGroupKey = customerFacades.guid.Generate(KeyTypes.GroupKey, KeyTypes.Contact);
                customer.PaymentGroupKey = customerFacades.guid.Generate(KeyTypes.GroupKey, KeyTypes.Payment);
                customerFacades.operations.SaveEntity(customer);
                contactAddresses.ForEach(xd =>
                {
                    xd.PublicKey = customerFacades.guid.Generate(KeyTypes.PublicKey, KeyTypes.Contact);
                    xd.PrivateKey = customerFacades.guid.Generate(KeyTypes.PrivateKey, KeyTypes.Contact);
                    xd.ContactGroupKey = customer.ContactGroupKey;
                    customerFacades.operations.SaveEntity(xd);
                });

                CustomerFirmRelation customerFirmRelation = new()
                {
                    CustomerPrivateKey = customer.PrivateKey,
                    FirmPrivateKey = firm.PrivateKey,
                    PublicKey = customerFacades.guid.Generate(KeyTypes.PublicKey, KeyTypes.CustomerFirmRelation),
                    PrivateKey = customerFacades.guid.Generate(KeyTypes.PrivateKey, KeyTypes.CustomerFirmRelation),
                };
                customerFacades.operations.SaveEntity(customerFirmRelation);
                result.Result = EnumRequestResult.Ok;
                result.Description = "AddIndividualCustomer Operations Success";

            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "AddIndividualCustomer Operations Not Success";
            });

            return customerFacades.dataConverter.Serialize(result);
        }


        [HttpPost]
        [GetAuthorizeToken]
        public string AddCorporateCustomer([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("AddCorporateCustomer");
            customerFacades.operationRunner.ActionRunner(() =>
            {
                cCustomer ccustomer = customerFacades.dataConverter.Deserialize<cCustomer>(parameter.Data);
                Firm firm = customerFacades.operations.GetMyFirmForFirmPublicKey(ccustomer.FirmPublicKey);
                CorporateCustomer customer = customerFacades.mapping.Convert<CorporateCustomer>(ccustomer);

                List<ContactAddress> contactAddresses = customerFacades.mapping.ConvertToList<ContactAddress>(ccustomer.ContactAddresses);
                List<PaymentAddress> paymentAddresses = customerFacades.mapping.ConvertToList<PaymentAddress>(ccustomer.PaymentAddresses);
                customer.PrivateKey = customerFacades.guid.Generate(KeyTypes.PrivateKey, KeyTypes.CorporateCustomer);
                customer.PublicKey = customerFacades.guid.Generate(KeyTypes.PublicKey, KeyTypes.CorporateCustomer);
                customer.CustomerKey = customerFacades.guid.Generate(KeyTypes.Key, KeyTypes.Customer);
                customer.ContactGroupKey = customerFacades.guid.Generate(KeyTypes.GroupKey, KeyTypes.Contact);
                customer.PaymentGroupKey = customerFacades.guid.Generate(KeyTypes.GroupKey, KeyTypes.Payment);
                customerFacades.operations.SaveEntity(customer);
                contactAddresses.ForEach(xd =>
                {
                    xd.PublicKey = customerFacades.guid.Generate(KeyTypes.PublicKey, KeyTypes.Contact);
                    xd.PrivateKey = customerFacades.guid.Generate(KeyTypes.PrivateKey, KeyTypes.Contact);
                    xd.ContactGroupKey = customer.ContactGroupKey;
                    customerFacades.operations.SaveEntity(xd);
                });

                paymentAddresses.ForEach(xd =>
                {
                    xd.PublicKey = customerFacades.guid.Generate(KeyTypes.PublicKey, KeyTypes.Contact);
                    xd.PrivateKey = customerFacades.guid.Generate(KeyTypes.PrivateKey, KeyTypes.Contact);
                    xd.PaymentGroupKey = customer.PaymentGroupKey;
                    customerFacades.operations.SaveEntity(xd);
                });

                CustomerFirmRelation customerFirmRelation = new()
                {
                    CustomerPrivateKey = customer.PrivateKey,
                    FirmPrivateKey = firm.PrivateKey,
                    PublicKey = customerFacades.guid.Generate(KeyTypes.PublicKey, KeyTypes.CustomerFirmRelation),
                    PrivateKey = customerFacades.guid.Generate(KeyTypes.PrivateKey, KeyTypes.CustomerFirmRelation),
                };
                customerFacades.operations.SaveEntity(customerFirmRelation);
                result.Result = EnumRequestResult.Ok;
                result.Description = "AddCorporateCustomer Operation Success";

            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "AddCorporateCustomer Operations Not Success";
            });

            return customerFacades.dataConverter.Serialize(result);
        }



        [HttpPost]
        [GetAuthorizeToken]
        public string GetMyCustomers([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("GetMyCustomers");
            customerFacades.operationRunner.ActionRunner(() =>
            {
                string firmPublicKey = customerFacades.dataConverter.Deserialize<string>(parameter.Data);


                Colosus.Entity.Concretes.DTO.Customers resultCustomer = customerFacades.operations.GetMyFirmCustomers(firmPublicKey);

                result.Data = customerFacades.dataConverter.Serialize(resultCustomer);
                result.Result = EnumRequestResult.Ok;
                result.Description = "Success";

            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "GetMyCustomers Operations Not Success";
            });

            return customerFacades.dataConverter.Serialize(result);
        }


    }
}
