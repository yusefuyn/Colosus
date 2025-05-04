using Colosus.Business.Abstracts;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;
using Colosus.Operations.Abstracts;
using Colosus.Server.Attributes;
using Colosus.Server.Facades.Firm;
using Colosus.Server.Facades.Setting;
using Colosus.Server.Services.Token;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Colosus.Server.Controllers
{
    [ApiController]
    [Route("/Api/[controller]/[action]")]
    //[EnableCors("AllowAll")]
    public class FirmController : Controller
    {
        IFirmFacades firmFacades;
        public FirmController(IFirmFacades firmFacades)
        {
            this.firmFacades = firmFacades;
        }

        private string GenKey(string keyType, string entityType) => firmFacades.guid.Generate(keyType, entityType);
        [HttpPost]
        [GetAuthorizeToken]
        public RequestResult<List<FirmDTO>> GetMyFirm([FromBody] RequestParameter parameter)
        {
            RequestResult<List<FirmDTO>> result = new("Get my Firm");
            firmFacades.operationRunner.ActionRunner(() =>
            {
                result.Result = EnumRequestResult.Ok;
                result.Description = "Successfly";
                List<FirmDTO> source = new();
                if (!string.IsNullOrEmpty(parameter.Token))
                    source = firmFacades.operations.GetsMyFirms(parameter.Token);
                result.Data = source;
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "Firmalar gelmedi.";
            });
            return result;
        }

        [HttpPost]
        [GetAuthorizeToken]

        public RequestResult AddNewFirm([FromBody] RequestParameter<Firm> parameter)
        {
            RequestResult result = new("Add new Firm");
            firmFacades.operationRunner.ActionRunner(() =>
            {



                if (string.IsNullOrEmpty(parameter.Token))
                {
                    result.Result = EnumRequestResult.Error;
                    result.Description = $"Error #{DateTime.Now}";
                }
                else
                {
                    result.Result = EnumRequestResult.Ok;
                    result.Description = "Successfly";

                    parameter.Data.PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.Firm);
                    parameter.Data.PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.Firm);
                    parameter.Data.PublicID = "F" + new Random().Next(100000, 999999).ToString();
                    firmFacades.operations.SaveEntity(parameter.Data);

                    FirmRole role = new()
                    {
                        Name = "Manager",
                        FirmPrivateKey = parameter.Data.PrivateKey,
                        PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.FirmRole),
                        PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.FirmRole),
                    };

                    firmFacades.operations.SaveEntity(role);

                    FirmUserRelation relation = new()
                    {
                        FirmPrivateKey = parameter.Data.PrivateKey,
                        PositionPrivateKey = role.PrivateKey,
                        UserPrivateKey = parameter.Token,
                        PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.FirmUserRelation),
                        PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.FirmUserRelation),
                    };

                    firmFacades.operations.SaveEntity(relation);
                }

            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "Firma eklenemedi.";
            });
            return result;
        }

        [HttpPost]
        [GetAuthorizeToken]
        public RequestResult JoinAFirm([FromBody] RequestParameter<JoinFirmRequestModel> parameter)
        {
            RequestResult result = new("Join a Firm");
            firmFacades.operationRunner.ActionRunner(() =>
            {
                if (string.IsNullOrEmpty(parameter.Token))
                {
                    result.Result = EnumRequestResult.Error;
                    result.Description = $"Error #{DateTime.Now}";
                }
                else
                {
                    result.Result = EnumRequestResult.Ok;
                    result.Description = "Firmaya başarıyla katılındı.";
                    Firm firm = firmFacades.operations.GetMyFirmWithFirmPublicKey(parameter.Data.FirmPublicKey);
                    if (firm == null)
                    {
                        result.Result = EnumRequestResult.Error;
                        result.Description = "Geçersiz bir firma anahtarı girildi.";
                    }

                    FirmUserRelation relation = new()
                    {
                        FirmPrivateKey = firm.PrivateKey,
                        PositionPrivateKey = "",
                        UserPrivateKey = parameter.Token,
                        PrivateKey = GenKey(KeyTypes.PrivateKey, KeyTypes.FirmUserRelation),
                        PublicKey = GenKey(KeyTypes.PublicKey, KeyTypes.FirmUserRelation),
                    };
                    firmFacades.operations.SaveEntity(relation);
                }
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "Firma eklenemedi.";
            });
            return result;
        }
    }
}
