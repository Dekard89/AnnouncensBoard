using AnnoucensBoard.Domain.Entity;
using AnnouncensBoard.Autoriztion.Requerments;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace AnnouncensBoard.Autoriztion.RequereHandlers
{
    public class OnlyOwnerHandler : AuthorizationHandler<OnlyOwnerRequerment, Topic>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyOwnerRequerment requirement, Topic resource)
        {
            if (context.User.Identity?.Name == resource.Author)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
