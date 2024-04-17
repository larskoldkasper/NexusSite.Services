using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace NexusSite.Services
{
    public class Authenticator : iAuthenticator
    {
        const string NexusAccessingGroup = "os2rk_nexusvikaraktivering";
        const string NexusAdminGroup = "os2rk_nexus_systemadministrator";
        Dictionary<string,string> groups=null;
        public bool Authenticate(ClaimsPrincipal user)
        {
            if(groups == null)
            {
                if(MakeGroups(user) == false)
                    return false;
            }
            return groups.ContainsKey(NexusAccessingGroup) || groups.ContainsKey(NexusAdminGroup);
        }

        private bool MakeGroups(ClaimsPrincipal user)
        {
            groups = new Dictionary<string, string>();

            var claim = (WindowsIdentity)user.Identity;
            if (claim == null)
            {
                return false;
            }
            
            foreach (var group in claim.Groups)
            {
                if (group == null) 
                { 
                    continue; 
                }
                string strGroupName = new System.Security.Principal.SecurityIdentifier(group.Value).Translate(typeof(System.Security.Principal.NTAccount)).ToString().ToLower().Replace(@"samdrift\", "");
                if (string.IsNullOrEmpty(strGroupName))
                    continue;
                groups.Add(strGroupName, group.Value);
                if(strGroupName.ToLower().Contains("nexus"))
                {
                    continue;
                }
            }
            
            return groups.Count > 0;
        }
    }
}
