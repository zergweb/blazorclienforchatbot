using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Pages.AdminPages.VotingPage
{
    public class Voting
    {
        public String ObjectOfVoting { get; set; }
        public Dictionary<string, string> Options { get; set; }
        //public Dictionary<string, string> Votes { get; set; }
        //public List<int> ResultEvents { get; set; }
    }
}
