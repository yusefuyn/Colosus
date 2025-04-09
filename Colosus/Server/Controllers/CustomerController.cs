using Colosus.Client;
using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Server.Attributes;
using Colosus.Server.Facades.Customer;
using Microsoft.AspNetCore.Mvc;
using iCustomer = Colosus.Entity.Concretes.CreateModel.IndividualCustomerCreateModel;
using cCustomer = Colosus.Entity.Concretes.CreateModel.CorporateCustomerCreateModel;
using Colosus.Entity.Abstracts;
using Colosus.Server.Facades.Setting;
using Colosus.Entity.Concretes.CreateModel;
using System.Security.Cryptography.X509Certificates;
using Colosus.Entity.Concretes.DTO;
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

        private string GenKey(string keyType, string entityType)
            => customerFacades.guid.Generate(keyType, entityType);


        [HttpPost]
        [GetAuthorizeToken]
        public string DeleteDebtPay([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("DeleteDebtPay");
            customerFacades.operationRunner.ActionRunner(() =>
            {
                string debtPayPublicKey = customerFacades.dataConverter.Deserialize<string>(parameter.Data);
                DebtPay debtPay = customerFacades.operations.GetDebtPayForPublicKey(debtPayPublicKey);
                customerFacades.operations.RemoveEntity(debtPay);
                result.Result = EnumRequestResult.Ok;
                result.Description = "DeleteDebtPay Operations Success";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "DeleteDebtPay Operations Not Success";
            });
            return customerFacades.dataConverter.Serialize(result);
        }

        [HttpPost]
        [GetAuthorizeToken]
        public string AddCustomerDebt([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("AddCustomerDebts");
            customerFacades.operationRunner.ActionRunner(() =>
            {
                DebtCreateModel debt = customerFacades.dataConverter.Deserialize<DebtCreateModel>(parameter.Data);
                ICustomer customer = customerFacades.operations.GetICustomerFromCustomerPublicKey(debt.CustomerPublicKey);
                Debt res = customerFacades.mapping.Convert<Debt>(debt);
                res.CustomerKey = customer.CustomerKey;
                res.UserPrivateKey = parameter.Token.ToString();
                res.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.Debt);
                res.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.Debt);
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
                DebtPayCreateModel model = customerFacades.dataConverter.Deserialize<DebtPayCreateModel>(parameter.Data.ToString());
                Debt debt = customerFacades.operations.GetDebt(model.DebtPublicKey);
                Currency currency = customerFacades.operations.GetCurrency(model.CurrencyPublicKey);
                PaymentType paymentType = customerFacades.operations.GetPaymentType(model.PaymentTypePublicKey);
                DebtPay pay = new()
                {
                    Price = model.Price,
                    CreateDate = DateTime.Now,
                    DebtPrivateKey = debt.PrivateKey,
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.DebtPay),
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.DebtPay),
                    CurrencyPrivateKey = currency.PrivateKey,
                    PaymentTypePrivateKey = paymentType.PrivateKey,
                };
                customerFacades.operations.SaveEntity(pay);
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
                List<Debt> debts = customerFacades.operations.GetsDebitForCustomerKey(customer.CustomerKey);
                List<Colosus.Entity.Concretes.DTO.DebtDTO> returnedObj = customerFacades.mapping.ConvertToList<Colosus.Entity.Concretes.DTO.DebtDTO>(debts);
                debts.ForEach(xd =>
                {
                    var debsts = returnedObj.FirstOrDefault(dx => dx.PublicKey == xd.PublicKey);
                    debsts.CustomerName = customer.GetName();
                    debsts.CustomerPublicKey = CustomerPublicKey;
                    debsts.CustomerKey = customer.CustomerKey;
                    debsts.Pays = customerFacades.operations.GetDebtPayForDebtPrivateKey(xd.PrivateKey);
                });
                DebtPageDTO returned = new();
                returned.CustomerName = customer.GetName();
                returned.Debts = returnedObj;

                result.Data = customerFacades.dataConverter.Serialize(returned);
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
        public string AddFastCustomer([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("AddFastCustomer");
            customerFacades.operationRunner.ActionRunner(() =>
            {
                FastCustomerCreateModel fcustomer = customerFacades.dataConverter.Deserialize<FastCustomerCreateModel>(parameter.Data);
                Firm firm = customerFacades.operations.GetMyFirmForFirmPublicKey(fcustomer.FirmPublicKey);
                FastCustomer customer = customerFacades.mapping.Convert<FastCustomer>(fcustomer);
                customer.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.FastCustomer);
                customer.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.FastCustomer);
                customer.CustomerKey = GenKey(KeyTypes.Key, KeyTypes.Customer);
                customer.ContactGroupKey = GenKey(KeyTypes.GroupKey, KeyTypes.Contact);
                customer.PaymentGroupKey = GenKey(KeyTypes.GroupKey, KeyTypes.Payment);
                customerFacades.operations.SaveEntity(customer);

                CustomerFirmRelation customerFirmRelation = new()
                {
                    CustomerPrivateKey = customer.PrivateKey,
                    FirmPrivateKey = firm.PrivateKey,
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.CustomerFirmRelation),
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.CustomerFirmRelation),
                };

                customerFacades.operations.SaveEntity(customerFirmRelation);
                result.Result = EnumRequestResult.Ok;
                result.Description = "AddFastCustomer Operations Success";

            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "AddFastCustomer Operations Not Success";
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
                customer.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.IndividualCustomer);
                customer.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.IndividualCustomer);
                customer.CustomerKey = GenKey(KeyTypes.Key, KeyTypes.Customer);
                customer.ContactGroupKey = GenKey(KeyTypes.GroupKey, KeyTypes.Contact);
                customer.PaymentGroupKey = GenKey(KeyTypes.GroupKey, KeyTypes.Payment);
                customerFacades.operations.SaveEntity(customer);
                contactAddresses.ForEach(xd =>
                {
                    xd.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.Contact);
                    xd.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.Contact);
                    xd.ContactGroupKey = customer.ContactGroupKey;
                    customerFacades.operations.SaveEntity(xd);
                });

                CustomerFirmRelation customerFirmRelation = new()
                {
                    CustomerPrivateKey = customer.PrivateKey,
                    FirmPrivateKey = firm.PrivateKey,
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.CustomerFirmRelation),
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.CustomerFirmRelation),
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
                customer.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.CorporateCustomer);
                customer.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.CorporateCustomer);
                customer.CustomerKey = GenKey(KeyTypes.Key, KeyTypes.Customer);
                customer.ContactGroupKey = GenKey(KeyTypes.GroupKey, KeyTypes.Contact);
                customer.PaymentGroupKey = GenKey(KeyTypes.GroupKey, KeyTypes.Payment);
                customerFacades.operations.SaveEntity(customer);
                contactAddresses.ForEach(xd =>
                {
                    xd.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.Contact);
                    xd.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.Contact);
                    xd.ContactGroupKey = customer.ContactGroupKey;
                    customerFacades.operations.SaveEntity(xd);
                });

                paymentAddresses.ForEach(xd =>
                {
                    xd.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.Contact);
                    xd.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.Contact);
                    xd.PaymentGroupKey = customer.PaymentGroupKey;
                    customerFacades.operations.SaveEntity(xd);
                });

                CustomerFirmRelation customerFirmRelation = new()
                {
                    CustomerPrivateKey = customer.PrivateKey,
                    FirmPrivateKey = firm.PrivateKey,
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.CustomerFirmRelation),
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.CustomerFirmRelation),
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


                Colosus.Entity.Concretes.DTO.CustomersDTO resultCustomer = customerFacades.operations.GetMyFirmCustomers(firmPublicKey);

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
