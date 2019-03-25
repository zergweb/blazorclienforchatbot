using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Pages.AdminPages.VotingPage
{
    public class VotingPattern
    {
        public Voting Voting { get; set; }
        public List<String> ResultEventsNames { get; set; }
        
    }
}
