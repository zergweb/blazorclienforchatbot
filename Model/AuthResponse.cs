using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Model
{
    public class AuthResponse
    {
       public string Token { get; set; }
       public string UserName { get; set; }
       public List<String> Roles { get; set; }

    }
}
