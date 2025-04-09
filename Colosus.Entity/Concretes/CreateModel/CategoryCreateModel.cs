using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.CreateModel
{
    public class CategoryCreateModel
    {
        public CategoryCreateModel()
        {
            Tax = 0;
        }
        public string Name { get; set; }
        public decimal Tax { get; set; }
        public string FirmPublicKey { get; set; }
        public bool VisibleWeb { get; set; }
    }
}
