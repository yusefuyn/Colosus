using Colosus.Business.Abstracts;
using Colosus.Entity.Concretes;
using Colosus.Entity.Concretes.DatabaseModel;
using Colosus.Operations.Abstracts;
using Colosus.Server.Attributes;
using Colosus.Server.Facades.Firm;
using Colosus.Server.Services.Token;
using Microsoft.AspNetCore.Mvc;

namespace Colosus.Server.Controllers
{
    [ApiController]
    [Route("/Api/[controller]/[action]")]
    public class FirmController : Controller
    {
        IFirmFacades firmFacades;
        public FirmController(IFirmFacades firmFacades)
        {
            this.firmFacades = firmFacades;
        }


        [HttpPost]
        [GetAuthorizeToken]
        public string GetMyFirm([FromBody] RequestParameter parameter)
        {
            RequestResult result = new("Get my Firm");
            firmFacades.operationRunner.ActionRunner(() =>
            {
                result.Result = EnumRequestResult.Ok;
                result.Description = "Successfly";
                List<Firm> source = new();
                if (!string.IsNullOrEmpty(parameter.Token))
                    source = firmFacades.operations.GetsMyFirms(parameter.Token);
                result.Data = firmFacades.dataConverter.Serialize(source);
            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "Firmalar gelmedi.";
            });
            return firmFacades.dataConverter.Serialize(result);
        }

        [HttpPost]
        [GetAuthorizeToken]

        public string AddNewFirm([FromBody] RequestParameter parameter)
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
                    Firm firm = firmFacades.dataConverter.Deserialize<Firm>(parameter.Data);
                    firm.PublicKey = firmFacades.guid.Generate(KeyTypes.PublicKey, KeyTypes.Firm);
                    firm.PrivateKey = firmFacades.guid.Generate(KeyTypes.PrivateKey, KeyTypes.Firm);
                    firm.PublicID = "F" + new Random().Next(100000, 999999).ToString();
                    firmFacades.operations.SaveEntity(firm);

                    FirmRole role = new()
                    {
                        Name = "Manager",
                        FirmPrivateKey = firm.PrivateKey,
                        PrivateKey = firmFacades.guid.Generate(KeyTypes.PrivateKey, KeyTypes.FirmRole),
                        PublicKey = firmFacades.guid.Generate(KeyTypes.PublicKey, KeyTypes.FirmRole),
                    };

                    firmFacades.operations.SaveEntity(role);

                    FirmUserRelation relation = new()
                    {
                        FirmPrivateKey = firm.PrivateKey,
                        PositionPrivateKey = role.PrivateKey,
                        UserPrivateKey = parameter.Token,
                        PrivateKey = firmFacades.guid.Generate(KeyTypes.PrivateKey, KeyTypes.FirmUserRelation),
                        PublicKey = firmFacades.guid.Generate(KeyTypes.PublicKey, KeyTypes.FirmUserRelation),
                    };

                    firmFacades.operations.SaveEntity(relation);
                }

            }, () =>
            {
                result.Result = EnumRequestResult.Error;
                result.Description = "Firma eklenemedi.";
            });
            return firmFacades.dataConverter.Serialize(result);
        }
    }
}
