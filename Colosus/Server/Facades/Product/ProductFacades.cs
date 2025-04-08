using Colosus.Business.Abstracts;
using Colosus.Operations.Abstracts;

namespace Colosus.Server.Facades.Product
{
    public class ProductFacades : IProductFacades
    {
        public ProductFacades(IDataConverter dataConverter, IOperationRunner operationRunner, IGuid guid, IOperations operations)
        {
            this.dataConverter = dataConverter;
            this.operationRunner = operationRunner;
            this.guid = guid;
            this.operations = operations;
        }

        public IDataConverter dataConverter { get; set; }
        public IOperationRunner operationRunner { get; set; }
        public IGuid guid { get; set; }
        public IOperations operations { get; set; }
    }
}
