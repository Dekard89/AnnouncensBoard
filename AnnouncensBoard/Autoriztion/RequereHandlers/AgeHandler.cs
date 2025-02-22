using AnnoucensBoard.Domain.Entity;
using AnnouncensBoard.Autoriztion.Requerments;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AnnouncensBoard.Autoriztion.RequereHandlers
{
    public class AgeHandler : AuthorizationHandler<AgeRequerment, Subject>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequerment requirement, Subject subject)
        {
            var yearClaim = context.User.FindFirst(x => x.Type == ClaimTypes.DateOfBirth);


            if(yearClaim is not null)
            {
                if(DateTime.TryParse(yearClaim.Value, out var year))
                {
                    if(subject.AdultOnly==false)
                    {
                        context.Succeed(requirement);

                    }
                    else
                    {

                        if ((DateTime.Now.Year - year.Year) >= requirement.Age)
                        {
                            context.Succeed(requirement);
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
