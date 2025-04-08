using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DTO
{
    /// <summary>
    /// Ürünleri listelerken kullanılacak obje
    /// </summary>
    public class Product
    {
        public Product()
        {
            Stock = 0;
        }
        public string PublicKey { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public string CategoryName { get; set; }
        public string FirmName { get; set; }
        public string CategoryPublicKey { get; set; }
        public string ProductCategoryRelationPublicKey { get; set; }
        public int? Stock { get; set; }

    }
}
