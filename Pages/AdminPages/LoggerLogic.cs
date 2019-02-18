using BlazorWebSocketHelper;
using BlazorWebSocketHelper.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Blazor.Components;
using System.Text;
using System.Threading.Tasks;
using static BlazorWebSocketHelper.Classes.BwsEnums;
using BlazorClient.Services;

namespace BlazorClient.Pages.AdminPages
{
    public class LoggerLogic : BlazorComponent
    {
        [Inject]
        public LogService Log { get; set; }
        [Inject]
        private AuthService auth { get; set; }
        public String ConnectStatus { get { return Log.Ws_Status; } }
        //protected override void OnInit()
        //{          
        //    Log.WsConnect();
        //    Log.StateChanged+= StateHasChanged;
        //    //
        //}
        protected override async Task OnInitAsync()
        {
            await auth.CheckAuthorize();
            Log.StateChanged += StateHasChanged;
            Log.WsConnect();
                  
        }
        public String ConnectBtn()
        {
             if (Log.IsConnected) { return "Disconnect"; }
                else { return "Connect";}
        }
        public void ConnectLog()
        {
             Log.WsConnect();
        }      
        public void ClearLog()
        {
            Log.ClearLog();
        }
    }
}
