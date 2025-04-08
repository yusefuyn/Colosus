using Colosus.Business.Abstracts;
using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Operations.Abstracts;
using Colosus.Operations.Concretes;
using Colosus.Server.Facades.Login;
using Colosus.Server.Services.Token;
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


        [HttpPost]
        public string Register([FromBody] RequestParameter parameter)
        {
            RequestResult result = new(KeyTypes.SaveUser);
            loginFacades.operationRunner.ActionRunner(() =>
            {
                User usera = loginFacades.dataConverter.Deserialize<User>(parameter.Data);
                usera.PublicKey = loginFacades.guid.Generate(KeyTypes.PublicKey, KeyTypes.User);
                usera.PrivateKey = loginFacades.guid.Generate(KeyTypes.PrivateKey, KeyTypes.User);
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
                User paramUser = loginFacades.dataConverter.Deserialize<User>(parameter.Data);
                User usera = loginFacades.operations.GetUser(paramUser.UserName, paramUser.Password);
                List<Role> userRoles = loginFacades.operations.GetUserRole(usera.PrivateKey);
                if (usera != null)
                {
                    result.Data = loginFacades.tokenService.GenerateJwtToken(usera.PrivateKey,usera.UserName, userRoles.Select(xd=> xd.Name).ToList());
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
