using BlazorClient.Services;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorClient.Pages.AdminPages.VotingPage
{
    public class VotingPageLogic : BlazorComponent
    {
        public class VotingEvent
        {
            public bool IsSelected { get; set; }
            public String TypeName { get; set; }
            public String Desc { get; set; }
        }
        [Inject]
        private AuthService Auth { get; set; }
        [Inject]
        private HttpClient http { get; set; }
        public String ObjectOfVoting;
        public String OptCommand;
        public String OptValue;
        public List<VotingEvent> VotingEvents;
        public VotingPattern VP;
        public VotingPageLogic()
        {
          
            VotingEvents = new List<VotingEvent> { new VotingEvent { Desc="Вывод результатов в чат",
                   IsSelected=true,
                   TypeName="BaseResultEvent"
            } , new VotingEvent { Desc="Сохранение результатов голосования в базе",
                   IsSelected=false,
                   TypeName="DbSaveResultEvent"
            } };
            VP = new VotingPattern { Voting = new Voting()
            {
                Options = new Dictionary<string, string>()
            },
                ResultEventsNames = new List<string>() };
        }
        protected override async Task OnInitAsync()
        {
            await Auth.CheckAuthorize();
           
        }
        public void AddOption()
        {
            if (!VP.Voting.Options.Any(x => x.Key == OptCommand)){
                VP.Voting.Options.Add(OptCommand, OptValue);
            }        
            StateHasChanged();
        }
        public void Test()
        {
            Console.WriteLine(VotingEvents.First().IsSelected);
        }
        public void RemoveOption(String key)
        {
            Console.WriteLine(key);
            VP.Voting.Options.Remove(key);
            StateHasChanged();
        }
        public async Task StartVoting()
        {
            VP.Voting.ObjectOfVoting = ObjectOfVoting;
            foreach (var item in VotingEvents.Where(x=> x.IsSelected))
            {
                VP.ResultEventsNames.Add(item.TypeName);
            }
             await http.PostJsonAsync("https://localhost:44353/startvoting", VP);

        }
        
    }
}
