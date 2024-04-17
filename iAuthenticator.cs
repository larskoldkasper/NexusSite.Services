using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NexusSite.Services
{
    public interface iAuthenticator
    {
        bool Authenticate(ClaimsPrincipal user);
    }
}
