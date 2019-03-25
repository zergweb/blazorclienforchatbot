using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Model
{
    public class BotStatus
    {
        public String Name { get; set; }
        public bool IsConnected { get; set; }
        public IEnumerable<String> Channels { get; set; }
    }
}
