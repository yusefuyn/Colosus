using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.CreateModel
{
    public class PaymentTypeCreateModel
    {
        public PaymentTypeCreateModel()
        {
            Name = "";
            PictureUri = "";
        }

        public string Name { get; set; }
        public string FirmPublicKey { get; set; }
        public string? PictureUri { get; set; }
    }
}
