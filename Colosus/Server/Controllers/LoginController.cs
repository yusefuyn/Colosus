using Colosus.Business.Abstracts;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.RequestModel;
using Colosus.Operations.Abstracts;
using Colosus.Operations.Concretes;
using Colosus.Server.Facades.Login;
using Colosus.Server.Facades.Setting;
using Colosus.Server.Services.Token;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Colosus.Server.Controllers
{

    [ApiController]
    [Route("/api/[controller]/[action]")]
    [EnableCors("AllowAll")]
    public class LoginController : Controller
    {
        ILoginFacades loginFacades;
        public LoginController(ILoginFacades loginFacades)
        {
            this.loginFacades = loginFacades;
        }

        private string GenKey(string keyType, string entityType)
            => loginFacades.guid.Generate(keyType, entityType);

        [HttpPost]
        public RequestResult Register([FromBody] RequestParameter<User> parameter)
        {
            RequestResult result = new(KeyTypes.SaveUser);
            loginFacades.operationRunner.ActionRunner(() =>
            {

                parameter.Data.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.User);
                parameter.Data.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.User);
                loginFacades.operations.SaveEntity(parameter.Data);
                result.Result = EnumRequestResult.Ok;
                result.Description = "Successfuly Registered";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = $"Error #{DateTime.Now}";
            });
            return result;
        }


        [HttpPost]
        public RequestResult<string> Login([FromBody] RequestParameter<LoginUserRequestModel> parameter)
        {
            RequestResult<string> result = new(KeyTypes.LoginUser);
            loginFacades.operationRunner.ActionRunner(() =>
            {
              
                string pas = parameter.Data.HashedToPass == true ? parameter.Data.Password : loginFacades.hash.Calc(parameter.Data.Password);
                User usera = loginFacades.operations.GetUser(parameter.Data.UserName, pas);
                List<Role> userRoles = loginFacades.operations.GetUserRole(usera.PrivateKey);
                if (usera != null)
                {
                    result.Data = loginFacades.tokenService.GenerateJwtToken(usera.PrivateKey, usera.UserName, userRoles.Select(xd => xd.Name).ToList());
                    result.Result = EnumRequestResult.Ok;
                    result.Description = "Successfuly Login";
                }
                else
                {
                    result.Description = "User not found";
                    result.Result = EnumRequestResult.Not;
                }
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = $"Error #{DateTime.Now}";
            });
            return result;
        }

    }
}
