using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DTO
{
    public class PosDTO
    {
        public PosDTO()
        {
            Products = new();
            Categories = new();
            Currencies = new();
            PayTypes = new();
        }

        public ICustomer Customer { get; set; }
        public List<ProductDTO> Products { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<CurrencyDTO> Currencies { get; set; }
        public List<PaymentTypeDTO> PayTypes { get; set; }
    }
}
