using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Abstracts
{
    public interface IRequestParameter
    {
        public DateTime CreateDate { get; set; }
        public string? Token { get; set; }
        public string? RequestToken { get; set; }
        public string RequestParameterHash { get; set; }
        public string? Address { get; set; }
    }
}
