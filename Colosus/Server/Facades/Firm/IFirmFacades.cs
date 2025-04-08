using Colosus.Business.Abstracts;
using Colosus.Operations.Abstracts;

namespace Colosus.Server.Facades.Firm
{
    public interface IFirmFacades
    {
        public IDataConverter dataConverter { get; set; }
        public IOperations operations { get; set; }
        public IOperationRunner operationRunner { get; set; }
        public IGuid guid { get; set; }
    }
}
