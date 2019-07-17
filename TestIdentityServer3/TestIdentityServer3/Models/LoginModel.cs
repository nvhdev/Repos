using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestIdentityServer3.Models
{
    public class LoginModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Titel { get; set; }

        public string FoutMelding { get; internal set; }


    }
}