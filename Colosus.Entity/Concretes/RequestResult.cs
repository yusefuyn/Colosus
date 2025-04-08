using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes
{
    public class RequestResult
    {
        public RequestResult(string operationName)
        {
            CreateDate = DateTime.Now;
            Result = EnumRequestResult.Not;
            Description = "";
            OperationName = operationName;
        }
        public string OperationName { get; set; }
        public DateTime CreateDate { get; private set; }
        public string Data { get; set; }
        public string? RequestToken { get; set; }
        public EnumRequestResult Result { get; set; }
        public string Description { get; set; }

        public bool Ok() => Result == EnumRequestResult.Ok ? true : false;
    }

    public enum EnumRequestResult
    {
        Ok,
        Not,
        Stoped,
        Error,
    }
}
