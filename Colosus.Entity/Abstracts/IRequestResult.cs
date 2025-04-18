using Colosus.Entity.Concretes.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Abstracts
{
    public interface IRequestResult
    {
        public string OperationName { get; set; }
        public DateTime CreateDate { get; set; }
        public string? RequestToken { get; set; }
        public EnumRequestResult Result { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool Ok() => Result == EnumRequestResult.Ok ? true : false;
    }
}
