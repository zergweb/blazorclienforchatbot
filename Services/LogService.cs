using BlazorWebSocketHelper;
using BlazorWebSocketHelper.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlazorWebSocketHelper.Classes.BwsEnums;

namespace BlazorClient.Services
{
    public class LogService
    {
        private int CountLog = 50;
        protected int TransportCode = 0;
        public List<BwsMessage> log = new List<BwsMessage>();
        public bool IsConnected = false;
        public string Ws_Status = "";
        private string Ws_URL = "wss://localhost:44353/ws";
        public delegate void ChangedStateHandler();
        public event ChangedStateHandler StateChanged;

        public WebSocketHelper WebSocketHelper1;
        private void WsOnStateChange(short par_state)
        {
            Ws_Status = WebSocketHelper1.bwsState.ToString();
            if (WebSocketHelper1.bwsState == BwsState.Close)
            {
                WebSocketHelper1.Dispose();
            }
            StateChanged();
        }
        private void Cmd_SetTransport(int Par_transportCode)
        {
            if (TransportCode != Par_transportCode)
            {
                TransportCode = Par_transportCode;
                BwsTransportType b = (BwsTransportType)(TransportCode);
                if (WebSocketHelper1.bwsTransportType != b)
                {
                    WebSocketHelper1.SetTransportType(b);
                };
            }
        }
        private void WsOnError(string par_error)
        {
            Console.WriteLine(par_error);
            //BwsJsInterop.Alert(par_error);
        }
        private void WsOnMessage(BwsMessage par_message)
        {
            par_message.ID = GetNewIDFromLog();
            log.Add(par_message);
            if (log.Count > this.CountLog) { log.RemoveAt(0); }
            StateChanged();
        }
        public void WsConnect()
        {
            if (!IsConnected)
            {              
                WebSocketHelper1 = new WebSocketHelper(Ws_URL, (BwsTransportType)(TransportCode))
                {
                    DoLog = false,
                    OnStateChange = WsOnStateChange,
                    OnMessage = WsOnMessage,
                    OnError = WsOnError
                };
                IsConnected = !IsConnected;
                
            }
            else
            {
                WebSocketHelper1.Close();
                IsConnected = !IsConnected;
                
            }
        }
        public void WsSendMessage(string Ws_Message)
        {
            if (WebSocketHelper1.bwsState == BwsState.Open)
            {
                if (!string.IsNullOrEmpty(Ws_Message))
                {
                    switch (WebSocketHelper1.bwsTransportType)
                    {
                        case BwsTransportType.Text:
                            //log.Add(new BwsMessage()
                            //{
                            //    ID = GetNewIDFromLog(),
                            //    Date = DateTime.Now,
                            //    Message = Ws_Message,
                            //    MessageType = BwsMessageType.send,
                            //    TransportType = WebSocketHelper1.bwsTransportType,
                            //});
                            WebSocketHelper1.send(Ws_Message);
                            break;
                        case BwsTransportType.ArrayBuffer:
                            byte[] data = Encoding.UTF8.GetBytes(Ws_Message);
                            BwsMessage b = new BwsMessage()
                            {
                                ID = GetNewIDFromLog(),
                                Date = DateTime.Now,
                                Message = Ws_Message,
                                MessageBinary = data,
                                MessageType = BwsMessageType.send,
                                TransportType = WebSocketHelper1.bwsTransportType,
                            };
                            WebSocketHelper1.send(b.MessageBinary);
                           // b.MessageBinaryVisual = string.Join(", ", data);
                            //log.Add(b);
                            break;
                        case BwsTransportType.Blob:
                            break;
                        default:
                            break;
                    }
                    Ws_Message = string.Empty;
                }
            }
            else
            {
                Console.WriteLine("Connection is closed");
            }
        }
        private int GetNewIDFromLog()
        {
            if (log.Any())
            {
                return log.Max(x => x.ID) + 1;
            }
            else
            {
                return 1;
            }
        }
        public string WsGetStatus()
        {
            return WebSocketHelper1.bwsState.ToString();
        }
        public void ClearLog()
        {
            if (log.Any())
            {
                log = new List<BwsMessage>();
                //StateHasChanged();
            }
        }
        public void SetLogCount(int count)
        {
            this.CountLog = count;
        }


    }
}
