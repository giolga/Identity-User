using Microsoft.AspNetCore.Identity;
using IdentityApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace IdentityApp.Authorization
{
    public class InvoiceCreatorAutorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Invoice>
    {
        UserManager<IdentityUser> _userManager;
        public InvoiceCreatorAutorizationHandler(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Invoice resource)
        {
            if (context.User is null || resource is null)
                return Task.CompletedTask;


            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            if (resource.CreatorId == _userManager.GetUserId(context.User))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
