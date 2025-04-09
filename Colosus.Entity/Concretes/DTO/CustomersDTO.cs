using Colosus.Entity.Concretes.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DTO
{
    public class CustomersDTO
    {
        public CustomersDTO()
        {
            individualCustomers = new();
            corporateCustomers = new();
        }
        public List<IndividualCustomer> individualCustomers { get; set; }
        public List<CorporateCustomer> corporateCustomers { get; set; }
    }
}
