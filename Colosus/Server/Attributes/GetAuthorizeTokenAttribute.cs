using Colosus.Client.Services;
using Colosus.Entity.Concretes;
using Colosus.Server.Services.Token;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel;

namespace Colosus.Server.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    [Description("Bu Attribute kullanıldığı Aksiyondaki RequestParameter sınıfının Token Özelliğine kullanıcı ID'sini yerleştirmektedir.\nKullanıcı ID'ye sahip değilse boş bırakmaktadır.\n Tokenin geçerliliğinide kontrol etmektedir.")]
    /// <summary>
    /// Bunu tokenin olup olmaması durumlarında farklı işlem yapacak aksiyonlar için kullanın.
    /// </summary>
    public class GetAuthorizeTokenAttribute : ActionFilterAttribute
    {
        public GetAuthorizeTokenAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            HttpContext httpContext = context.HttpContext;
            RequestParameter? parameter = context.ActionArguments["parameter"] as RequestParameter;
            string requestToken = httpContext.Request.Headers["Authorization"].ToString();
            var tokenService = (ITokenService)context.HttpContext.RequestServices.GetService(typeof(ITokenService));

            if (parameter == null)
                base.OnActionExecuting(context);

            if (string.IsNullOrEmpty(requestToken))
                requestToken = "";
            else
            {
                if (tokenService.ValidateJwtToken(ref requestToken))
                {
                    string userId = tokenService.GetUserIDFromToken(requestToken);
                    requestToken = userId;
                }
            }
            parameter.Token = requestToken;
            base.OnActionExecuting(context);
        }
    }
}
