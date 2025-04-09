using Colosus.Business.Abstracts;
using Colosus.Entity.Concretes;
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
    //[EnableCors("AllowAll")]
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
        public string Register([FromBody] RequestParameter parameter)
        {
            RequestResult result = new(KeyTypes.SaveUser);
            loginFacades.operationRunner.ActionRunner(() =>
            {
                User usera = loginFacades.dataConverter.Deserialize<User>(parameter.Data);
                usera.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.User);
                usera.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.User);
                loginFacades.operations.SaveEntity(usera);
                result.Result = EnumRequestResult.Ok;
                result.Description = "Successfuly Registered";
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = $"Error #{DateTime.Now}";
            });
            return loginFacades.dataConverter.Serialize(result);
        }


        [HttpPost]
        public string Login([FromBody] RequestParameter parameter)
        {
            RequestResult result = new(KeyTypes.LoginUser);
            loginFacades.operationRunner.ActionRunner(() =>
            {
                LoginUserRequestModel paramUser = loginFacades.dataConverter.Deserialize<LoginUserRequestModel>(parameter.Data);
                string pas = paramUser.HashedToPass == true ? paramUser.Password : loginFacades.hash.Calc(paramUser.Password);
                User usera = loginFacades.operations.GetUser(paramUser.UserName, pas);
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
            return loginFacades.dataConverter.Serialize(result);
        }

    }
}
