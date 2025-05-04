using Colosus.Business.Abstracts;
using Colosus.Entity.Concretes.CreateModel;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.RequestModel;
using Colosus.Entity.Concretes.ResultModel;
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
        public RequestResult Register([FromBody] RequestParameter<RegisterCreateModel> parameter)
        {
            RequestResult result = new(KeyTypes.SaveUser);
            loginFacades.operationRunner.ActionRunner(() =>
            {
                User user = new User();
                user = loginFacades.mapping.Convert<User>(parameter.Data);

                user.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.User);
                user.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.User);
                loginFacades.operations.SaveEntity(user);
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
        public RequestResult<LoginResultModel> Login([FromBody] RequestParameter<LoginUserRequestModel> parameter)
        {
            RequestResult<LoginResultModel> result = new(KeyTypes.LoginUser);
            loginFacades.operationRunner.ActionRunner(() =>
            {

                string pas = parameter.Data.HashedToPass == true ? parameter.Data.Password : loginFacades.hash.Calc(parameter.Data.Password);
                User usera = loginFacades.operations.GetUser(parameter.Data.UserName, pas);
                if (usera == null)
                {
                    result.Description = "User not found";
                    result.Result = EnumRequestResult.Not;
                    return;
                }
                List<Role> userRoles = loginFacades.operations.GetUserRole(usera.PrivateKey);
                string Key = loginFacades.tokenService.GenerateJwtToken(usera.PrivateKey, usera.UserName, userRoles.Select(xd => xd.Name).ToList());
                result.Data = new LoginResultModel() { Token = Key, UserPublicKey = usera.PublicKey };
                result.Result = EnumRequestResult.Ok;
                result.Description = "Successfuly Login";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = $"Error #{DateTime.Now}";
            });
            return result;
        }

    }
}
