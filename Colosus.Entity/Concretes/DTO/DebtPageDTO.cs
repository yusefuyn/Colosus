using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DTO
{
    public class DebtPageDTO
    {
        public DebtPageDTO()
        {
            Debts = new();
            CustomerName = "";
        }
        public string CustomerName { get; set; }
        public List<DebtDTO> Debts { get; set; }
    }
}
