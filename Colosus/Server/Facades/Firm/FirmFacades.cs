using Colosus.Business.Abstracts;
using Colosus.Operations.Abstracts;

namespace Colosus.Server.Facades.Firm
{
    public class FirmFacades : IFirmFacades
    {
        public FirmFacades(IDataConverter dataConverter, IOperations operations, IOperationRunner operationRunner, IGuid guid)
        {
            this.dataConverter = dataConverter;
            this.operations = operations;
            this.operationRunner = operationRunner;
            this.guid = guid;
        }

        public IDataConverter dataConverter { get;set; }
        public IOperations operations { get;set; }
        public IOperationRunner operationRunner { get;set; }
        public IGuid guid { get;set; }
    }
}
