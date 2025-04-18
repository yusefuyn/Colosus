using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.RequestModel
{

    public class RequestParameter : IRequestParameter
    {
        public RequestParameter()
        {
            CreateDate = DateTime.Now;
        }
        public string? Token { get; set; }
        public string? RequestToken { get; set; }
        public string RequestParameterHash { get; set; }
        public string? Address { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class RequestParameter<T> : IRequestParameter where T : class
    {

        public RequestParameter()
        {
            CreateDate = DateTime.Now;
        }
        public T Data { get; set; }
        public string? Token { get; set; }
        public string? RequestToken { get; set; }
        public string RequestParameterHash { get; set; }
        public string? Address { get; set; }
        public DateTime CreateDate { get; set; }
    }

   


}
