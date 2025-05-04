using Colosus.Business.Abstracts;
using Colosus.Operations.Abstracts;

namespace Colosus.Server.Facades.Category
{
    public class CategoryFacades : ICategoryFacades
    {
        public CategoryFacades(IDataConverter dataConverter, IHash hash, IDatabaseOperations operations, IOperationRunner operationRunner, IGuid guid, IMapping mapping)
        {
            this.dataConverter = dataConverter;
            this.hash = hash;
            this.operations = operations;
            this.operationRunner = operationRunner;
            this.guid = guid;
            this.mapping = mapping;
        }

        public IDataConverter dataConverter { get; set; }
        public IHash hash { get; set; }
        public IDatabaseOperations operations { get; set; }
        public IOperationRunner operationRunner { get; set; }
        public IGuid guid { get; set; }
        public IMapping mapping { get; set; }
    }
}
