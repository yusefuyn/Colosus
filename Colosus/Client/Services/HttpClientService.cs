using Microsoft.AspNetCore.Components;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http.Headers;
using System;
using Colosus.Operations.Abstracts;
using Colosus.Entity.Concretes;

namespace Colosus.Client.Services
{
    public class HttpClientService
    {
        public HttpClient httpClient;
        NavigationManager navigationManager;
        CookieService cookieService;
        IHash hash;
        IDataConverter dataConverter;

        public HttpClientService(IDataConverter dataConverter, IHash hash,HttpClient httpClient, NavigationManager navigationManager, CookieService cookieService)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
            this.cookieService = cookieService;
            this.hash = hash;
            this.dataConverter = dataConverter;
        }

        public async Task<RequestResult> GetPostAsync<T>(RequestParameter requestParameter)
        {
            RequestResult returned = new RequestResult("")
            {
                Result = EnumRequestResult.Error,
                Data = "",
            };

            //if (string.IsNullOrEmpty(requestParameter.Address))
            //{
            //    returned.Description = "Address empty";
            //    returned.Result = EnumRequestResult.Error;

            //    return returned;
            //}

            //if (requestParameter.Supply < 1)
            //{
            //    returned.Description = "Supply small to 1";
            //    returned.Result = EnumRequestResult.Error;
            //    return returned;
            //}

            requestParameter.RequestParameterHash = "";
            requestParameter.RequestParameterHash = hash.Calc(dataConverter.Serialize(requestParameter));

            string Uri = navigationManager.BaseUri + requestParameter.Address;
            StringContent stringContent = new(dataConverter.Serialize(requestParameter), System.Text.Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync(Uri, stringContent);
            string Returned = await res.Content.ReadAsStringAsync();
            RequestResult returnedObject = dataConverter.Deserialize<RequestResult>(Returned);
            return returnedObject;
        }

        public async Task<RequestResult> GetPostAsync<T>(object param, string Address)
        {
            var reqparam = new RequestParameter() { Address = Address, Data = dataConverter.Serialize(param), Supply = 1 };
            var token = await cookieService.GetCookie("Token");

            if (!string.IsNullOrEmpty(token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            else
                httpClient.DefaultRequestHeaders.Authorization = null;

            return await GetPostAsync<T>(reqparam);
        }
    }
}
