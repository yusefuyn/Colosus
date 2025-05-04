using Colosus.Entity.Abstracts;
using Colosus.Entity.Concretes.RequestModel;
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
            ProductsForSale = new();
        }

        public CustomerDTO Customer { get; set; } // Müşteri Bilgilerini tutar
        public List<ProductDTO> Products { get; set; } // Tüm ürünleri Tutar
        public List<CategoryDTO> Categories { get; set; } // Kategori bilgilerini TUtar
        public List<CurrencyDTO> Currencies { get; set; } // Para birimi bilgilerini tutar
        public List<PaymentTypeDTO> PayTypes { get; set; } // Ödeme tiplerini tutar
        public List<SaleRequestModel> ProductsForSale { get; set; } // Satılması için eklenen verileri tutar.
        //public List<SaleDTO> SoldProducts { get; set; } // Satılanlar
    }
}
