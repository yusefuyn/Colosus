using Colosus.Business.Abstracts;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.RequestModel;
using Colosus.Operations.Abstracts;
using Colosus.Server.Facades.Administrator;
using Colosus.Server.Facades.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Colosus.Server.Controllers
{

    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class AdministratorController : Controller
    {
        IAdministratorFacades administratorFacades;
        public AdministratorController(IAdministratorFacades administratorFacades)
        {
            this.administratorFacades = administratorFacades;
        }


        [HttpPost]
        public RequestResult UpdateDatabase([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("Update Database");
            administratorFacades.operationRunner.ActionRunner(() =>
            {
                result.Result = EnumRequestResult.Ok;
                result.Description = "Successfuly Update Database Operation";
                administratorFacades.operations.DatabaseUpdate();
            }, () =>
            {
                result.Result = EnumRequestResult.Not;
                result.Description = $"Error #{DateTime.Now}";
            });

            return result;
        }
        private string GenKey(string keyType, string entityType)
            => administratorFacades.guid.Generate(keyType, entityType);

        [HttpPost]
        public RequestResult Setup([FromBody] RequestParameter<string> parameter)
        {
            RequestResult result = new("Setup");
            administratorFacades.operationRunner.ActionRunner(() =>
            {

                result.Result = EnumRequestResult.Ok;
                result.Description = "Successfuly Setup Operation";
                string Password = parameter.Data.ToString();

                if (string.IsNullOrEmpty(Password) && administratorFacades.hash.Calc(Password) != administratorFacades.hash.Calc("219619"))
                {
                    result.Result = EnumRequestResult.Not;
                    result.Description = "Incorected Passwd";
                }
                else
                    administratorFacades.operations.DatabaseUpdate();

                User administratorUser = new()
                {
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.User),
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.User),
                    ReferancePrivateKey = "System",
                    UserName = "Yussefuynstein",
                    Password = administratorFacades.hash.Calc("219619yusuf_"),
                    FirstName = "Yusuf",
                    EMail = "yussefuynstein@gmail.com",
                    LastName = "Kıdır",
                };


                administratorFacades.operations.SaveEntity(administratorUser);

                Role administratorRole = new()
                {
                    Name = "Administrator",
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.Role),
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.Role),
                };

                administratorFacades.operations.SaveEntity(administratorRole);

                UserRoleRelations administratorRoleRelation = new()
                {
                    PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.RoleRelation),
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.RoleRelation),
                    RolePrivateKey = administratorRole.PrivateKey,
                    UserPrivateKey = administratorUser.PrivateKey,
                    IssuerPrivateKey = "System",
                };

                administratorFacades.operations.SaveEntity(administratorRoleRelation);

                PaymentType nakitOdeme = new()
                {
                    Name = "Nakit",
                    PictureUri = "",
                    PrivateKey = "All-Nakit",
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.PaymentType)
                };
                administratorFacades.operations.SaveEntity(nakitOdeme);

                PaymentType kartOdeme = new()
                {
                    Name = "Kart",
                    PictureUri = "",
                    PrivateKey = "All-Kart",
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.PaymentType)
                };
                administratorFacades.operations.SaveEntity(kartOdeme);

                PaymentType cariOdeme = new()
                {
                    Name = "Cari",
                    PictureUri = "",
                    PrivateKey = "All-Cari",
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.PaymentType)
                };
                administratorFacades.operations.SaveEntity(cariOdeme);

                PaymentType cekOdeme = new()
                {
                    Name = "Cek",
                    PictureUri = "",
                    PrivateKey = "All-Cek",
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.PaymentType)
                };
                administratorFacades.operations.SaveEntity(cekOdeme);

                PaymentType senetOdeme = new()
                {
                    Name = "Senet",
                    PictureUri = "",
                    PrivateKey = "All-Senet",
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.PaymentType)
                };
                administratorFacades.operations.SaveEntity(senetOdeme);

                PaymentType sodexoOdeme = new()
                {
                    Name = "Sodexo",
                    PictureUri = "",
                    PrivateKey = "All-Sodexo",
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.PaymentType)
                };
                administratorFacades.operations.SaveEntity(sodexoOdeme);


                Currency tl = new()
                {
                    Name = "Turkish Lira",
                    Symbol = "₺",
                    PrivateKey = "All-TL",
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.Currency)
                };
                administratorFacades.operations.SaveEntity(tl);

                Currency euro = new()
                {
                    Name = "Euroa",
                    Symbol = "€",
                    PrivateKey = "All-EU",
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.Currency)
                };
                administratorFacades.operations.SaveEntity(euro);

                Currency usd = new()
                {
                    Name = "USD",
                    Symbol = "$",
                    PrivateKey = "All-US",
                    PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.Currency)
                };
                administratorFacades.operations.SaveEntity(usd);

            }, () =>
            {
                result.Result = EnumRequestResult.Not;
                result.Description = $"Error #{DateTime.Now}";
            });

            return result;
        }
    }
}
