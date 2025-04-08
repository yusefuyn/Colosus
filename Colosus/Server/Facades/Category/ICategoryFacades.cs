using Colosus.Business.Abstracts;
using Colosus.Operations.Abstracts;

namespace Colosus.Server.Facades.Category
{
    public interface ICategoryFacades
    {
        public IDataConverter dataConverter { get; set; }
        public IHash hash { get; set; }
        public IOperations operations { get; set; }
        public IOperationRunner operationRunner { get; set; }
        public IGuid guid { get; set; }
        public IMapping mapping { get; set; }
    }
}
