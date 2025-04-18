using Colosus.Business.Abstracts;
using Colosus.Operations.Abstracts;

namespace Colosus.Server.Facades.Pos
{
    public class SaleFacades : ISaleFacades
    {
        public SaleFacades(IDataConverter dataConverter, IOperations operations, IGuid guid, IOperationRunner operationRunner, IMapping mapping)
        {
            this.dataConverter = dataConverter;
            this.operations = operations;
            this.guid = guid;
            this.operationRunner = operationRunner;
            this.mapping = mapping;
        }

        public IDataConverter dataConverter { get;set;}
        public IOperations operations { get;set;}
        public IGuid guid { get;set;}
        public IOperationRunner operationRunner {get;set; }
        public IMapping mapping { get; set; }
    }
}
