using Microsoft.AspNetCore.Components;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http.Headers;
using System;
using Colosus.Operations.Abstracts;
using Colosus.Entity.Concretes.RequestModel;
using Colosus.Entity.Abstracts;
using Microsoft.AspNetCore.Components.Forms;

namespace Colosus.Client.Blazor.Services
{
    public class HttpClientService
    {
        public HttpClient httpClient;
        NavigationManager navigationManager;
        CookieService cookieService;
        IHash hash;
        IDataConverter dataConverter;

        public HttpClientService(IDataConverter dataConverter, IHash hash, HttpClient httpClient, NavigationManager navigationManager, CookieService cookieService)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
            this.cookieService = cookieService;
            this.hash = hash;
            this.dataConverter = dataConverter;
        }



        private async Task<RequestResult<T>> _GetPostAsync<T>(RequestParameter requestParameter)
        {

            RequestResult<T> returned = new RequestResult<T>("")
            {
                Result = EnumRequestResult.Error,
            };
            var token = await cookieService.GetCookie("Token");
            if (!string.IsNullOrEmpty(token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            else
                httpClient.DefaultRequestHeaders.Authorization = null;
            requestParameter.RequestParameterHash = "";
            requestParameter.RequestParameterHash = hash.Calc(dataConverter.Serialize(requestParameter));

            string Uri = navigationManager.BaseUri + requestParameter.Address;
            StringContent stringContent = new(dataConverter.Serialize(requestParameter), System.Text.Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync(Uri, stringContent);
            string Returned = await res.Content.ReadAsStringAsync();
            RequestResult<T> returnedObject = dataConverter.Deserialize<RequestResult<T>>(Returned);
            return returnedObject;
        }
        /// <summary>
        /// T Bekleyen
        /// </summary>
        /// <typeparam name="T">Alınan</typeparam>
        /// <param name="requestParameter"></param>
        /// <returns></returns>
        public async Task<RequestResult<T>> GetPostAsync<T>(string Address, string RequestToken = "")
            => await _GetPostAsync<T>(new RequestParameter() { Address = Address, RequestToken = RequestToken });



        private async Task<RequestResult<T>> _GetPostAsync<T, Z>(RequestParameter<Z> requestParameter)
            where T : class
            where Z : class
        {
            RequestResult<T> returned = new RequestResult<T>("")
            {
                Result = EnumRequestResult.Error,
            };

            var token = await cookieService.GetCookie("Token");

            if (!string.IsNullOrEmpty(token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            else
                httpClient.DefaultRequestHeaders.Authorization = null;

            requestParameter.RequestParameterHash = "";
            requestParameter.RequestParameterHash = hash.Calc(dataConverter.Serialize(requestParameter));

            string Uri = navigationManager.BaseUri + requestParameter.Address;
            StringContent stringContent = new(dataConverter.Serialize(requestParameter), System.Text.Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync(Uri, stringContent);
            string Returned = await res.Content.ReadAsStringAsync();
            RequestResult<T> returnedObject = dataConverter.Deserialize<RequestResult<T>>(Returned);
            return returnedObject;
        }
        /// <summary>
        /// T bekleyen Z gönderen
        /// </summary>
        /// <typeparam name="T">Alınan</typeparam>
        /// <typeparam name="Z">Gönderilen</typeparam>
        /// <param name="requestParameter"></param>
        /// <returns></returns>
        public async Task<RequestResult<T>> GetPostAsync<T, Z>(string Address, Z data, string RequestToken = "")
            where T : class
            where Z : class
            => await _GetPostAsync<T, Z>(new RequestParameter<Z> { Address = Address, Data = data, RequestToken = RequestToken });


        private async Task<RequestResult> _GetPostAsync(RequestParameter requestParameter)
        {
            RequestResult returned = new RequestResult("")
            {
                Result = EnumRequestResult.Error,
            };

            var token = await cookieService.GetCookie("Token");

            if (!string.IsNullOrEmpty(token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            else
                httpClient.DefaultRequestHeaders.Authorization = null;

            string Uri = navigationManager.BaseUri + requestParameter.Address;
            StringContent stringContent = new(dataConverter.Serialize(requestParameter), System.Text.Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync(Uri, stringContent);
            string Returned = await res.Content.ReadAsStringAsync();
            RequestResult returnedObject = dataConverter.Deserialize<RequestResult>(Returned);
            return returnedObject;
        }
        /// <summary>
        /// Hiçbirşey göndermeyip hiçbirşey beklemeyen.
        /// </summary>
        /// <param name="requestParameter"></param>
        /// <returns></returns>
        public async Task<RequestResult> GetPostAsync(string Address, string RequestToken = "")
            => await _GetPostAsync(new RequestParameter() { Address = Address, RequestToken = RequestToken });


        private async Task<RequestResult> _GetPostAsync<T>(RequestParameter<T> requestParameter)
            where T : class
        {
            RequestResult returned = new RequestResult("")
            {
                Result = EnumRequestResult.Error,
            };

            var token = await cookieService.GetCookie("Token");

            if (!string.IsNullOrEmpty(token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            else
                httpClient.DefaultRequestHeaders.Authorization = null;

            requestParameter.RequestParameterHash = "";
            requestParameter.RequestParameterHash = hash.Calc(dataConverter.Serialize(requestParameter));

            string Uri = navigationManager.BaseUri + requestParameter.Address;
            StringContent stringContent = new(dataConverter.Serialize(requestParameter), System.Text.Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync(Uri, stringContent);
            string Returned = await res.Content.ReadAsStringAsync();
            RequestResult returnedObject = dataConverter.Deserialize<RequestResult>(Returned);
            return returnedObject;
        }
        /// <summary>
        /// T gönderen birşey beklemeyen.
        /// </summary>
        /// <typeparam name="T">Gönderilen</typeparam>
        /// <param name="requestResult"></param>
        /// <returns></returns>
        public async Task<RequestResult> GetPostAsync<T>(string Address, T Data, string RequestToken = "")
            where T : class
            => await _GetPostAsync(new RequestParameter<T>() { Address = Address, Data = Data, RequestToken = RequestToken });




    }
}
