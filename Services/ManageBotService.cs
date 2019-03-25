using BlazorClient.Model;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorClient.Services
{
    public class ManageBotService
    {
        private readonly HttpClient http;
        private String apiUrl = "https://localhost:44353/api/";
        public ManageBotService(HttpClient _http)
        {
            http = _http;
        }
        public Task Connect()
        {
            return  http.PostAsync(apiUrl+"bot/connect", new StringContent(""));
        }
        public Task Disconnect()
        {
            return http.PostAsync(apiUrl + "bot/disconnect", new StringContent(""));
        }
        public Task<BotStatus> GetStatus()
        {
            return http.PostJsonAsync<BotStatus>(apiUrl + "bot/getstatus", new StringContent(""));
        }
    }
}
