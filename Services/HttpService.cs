using Blazor.Extensions.Storage;
using BlazorClient.Model;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BlazorClient.Services
{
    public class HttpService : IHttpService
    {
        private HttpClient http;
        private LocalStorage localStorage;
        public HttpService(HttpClient _http, LocalStorage _localStorage)
        {
            http = _http;
            localStorage = _localStorage;
           // SetAuthorizationHeader();
        }
        public void TestAuth()
        {        
           var t = http.PostJsonAsync<string[]>("https://localhost:44353/testdata", "");
        }
        private async Task SetAuthorizationHeader()
        {
            if (!http.DefaultRequestHeaders.Contains("Authorization"))
            {
                var token = await localStorage.GetItem<string>("token");
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

    }
}
