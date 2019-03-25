using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorClient.Model;
using System.Net.Http;
using Microsoft.AspNetCore.Blazor;
using System.Net.Http.Headers;

namespace BlazorClient.Services
{
    public class AuthService
    {
        private LocalStorage localStorage;
        private String apiUrl = "https://localhost:44353/";
        private IUriHelper uriHelper { get; set; }
        private HttpClient http;
        public bool IsLogged { get; set; } = false;
        public AuthService(LocalStorage _localStorage,IUriHelper _uriHelper, HttpClient _http)
        {
            localStorage = _localStorage;
            uriHelper = _uriHelper;
            http= _http;
        }
        public async Task CheckAuthorize()
        {
            var t = await http.GetAsync(apiUrl+ "isauth");
            if (t.StatusCode == System.Net.HttpStatusCode.OK)
            {
                this.IsLogged = true;
            }
            else
            {
                uriHelper.NavigateTo("/login");
            }
        }
        public async Task Login(String login, String password)
        {
            var r = await http.PostJsonAsync<AuthResponse>(apiUrl+"login", new AuthForm() { Login = login, Pass = password });
            if (r!=null)
            {
                await localStorage.SetItem<String>("token", r.Token);
                await SetAuthorizationHeader();
                this.IsLogged = true;
                uriHelper.NavigateTo("/");
            }           
        }
        public void LogOut()
        {
            localStorage.RemoveItem("token");
            this.IsLogged = false;
            uriHelper.NavigateTo("/login");
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
