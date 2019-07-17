using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace TestIdentityServer3.Models.OpenId
{
    public static class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return StandardScopes.All;
        }
    }

}
