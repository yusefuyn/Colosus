using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes
{
    public class RequestParameter
    {

        public RequestParameter()
        {
            CreateDate = DateTime.Now;
            Supply = 1;
        }
        public DateTime CreateDate { get; private set; }
        public string? Token { get; set; }
        public string? RequestToken { get; set; }
        public int? Supply { get; set; }
        public string Data { get; set; }
        public string RequestParameterHash { get; set; }
        public string? Address { get; set; }
    }
}
