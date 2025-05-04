using Colosus.Business.Abstracts;
using Colosus.Operations.Abstracts;

namespace Colosus.Server.Facades.Product
{
    public interface IProductFacades
    {
        IDataConverter dataConverter { get; set; }
        IOperationRunner operationRunner { get; set; }
        IGuid guid { get; set; }
        IDatabaseOperations operations { get; set; }

    }
}
