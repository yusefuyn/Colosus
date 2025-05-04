using Colosus.Business.Abstracts;
using Colosus.Operations.Abstracts;
using Colosus.Server.Services.Token;

namespace Colosus.Server.Facades.Login
{
    public interface ILoginFacades
    {
        public IDataConverter dataConverter { get; set; }
        public IDatabaseOperations operations { get; set; }
        public IOperationRunner operationRunner { get; set; }
        public ITokenService tokenService { get; set; }
        public IGuid guid { get; set; }
        public IHash hash { get; set; }
        public IMapping mapping { get; set; }
    }
}
