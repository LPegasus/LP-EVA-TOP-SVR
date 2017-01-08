using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LP.Common.AuthPolicies
{
    public class RoleConfirmRequirement : IAuthorizationRequirement {
        private string _role;
        public RoleConfirmRequirement(string role)
        {
            _role = role;
        }

        public string Role { get { return _role; } }
    }
}
