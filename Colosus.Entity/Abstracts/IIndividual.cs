using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Abstracts
{
    public interface IIndividual
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? IDCardNumber { get; set; }
        public IndividualCustomerTypeEnum CustomerTypeEnum { get; set; }
    }

    public enum IndividualCustomerTypeEnum
    { 
        Male,
        Female,
        Unknow
    }
}
