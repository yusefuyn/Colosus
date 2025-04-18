using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.RequestModel
{
    public class RequestResult<T> : IRequestResult
    {
        public RequestResult(string operationName)
        {
            CreateDate = DateTime.Now;
            Result = EnumRequestResult.Not;
            Description = "";
            OperationName = operationName;
        }
        public T Data { get; set; }
        public string OperationName { get; set; }
        public DateTime CreateDate { get; set; }
        public string? RequestToken { get; set; }
        public EnumRequestResult Result { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        public bool Ok() => Result == EnumRequestResult.Ok ? true : false;
    }

    public class RequestResult : IRequestResult
    {
        public RequestResult(string operationName)
        {
            CreateDate = DateTime.Now;
            Result = EnumRequestResult.Not;
            Description = "";
            OperationName = operationName;
        }
        public string OperationName { get; set; }
        public DateTime CreateDate { get; set; }
        public string? RequestToken { get; set; }
        public EnumRequestResult Result { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

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
