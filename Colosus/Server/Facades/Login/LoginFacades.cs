using Colosus.Business.Abstracts;
using Colosus.Operations.Abstracts;
using Colosus.Server.Services.Token;

namespace Colosus.Server.Facades.Login
{
    public class LoginFacades : ILoginFacades
    {
        public LoginFacades(
            IDataConverter dataConverter, 
            IDatabaseOperations operations, 
            IOperationRunner operationRunner, 
            ITokenService tokenService, 
            IGuid guid, 
            IHash hash,
            IMapping mapping
            )
        {
            this.dataConverter = dataConverter;
            this.operations = operations;
            this.operationRunner = operationRunner;
            this.tokenService = tokenService;
            this.guid = guid;
            this.hash = hash;
            this.mapping = mapping;
        }

        public IDataConverter dataConverter { get; set; }
        public IDatabaseOperations operations { get; set; }
        public IOperationRunner operationRunner { get; set; }
        public ITokenService tokenService { get; set; }
        public IGuid guid { get; set; }
        public IHash hash { get; set; }
        public IMapping mapping { get; set; }
    }
}
