using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LP.Common.AuthPolicies
{
    public class RoleConfirmHandler : AuthorizationHandler<RoleConfirmRequirement> {

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleConfirmRequirement requirement)
        {
            if (context.Resource is Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext resource)
            {
                var id = resource.HttpContext.User.Identity;
            }
            // 若不存在 role 信息则返回验证失败
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Issuer == "lp-eva"))
            {
                await Task.CompletedTask;
            }

            var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role && c.Issuer == "lp-eva").Value;

            if (role == requirement.Role)
            {
                context.Succeed(requirement);
            }
            await Task.CompletedTask;
        }
    }
}
