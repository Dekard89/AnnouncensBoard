using Microsoft.AspNetCore.Authorization;

namespace AnnouncensBoard.Autoriztion.Requerments
{
    public class OnlyOwnerRequerment : IAuthorizationRequirement
    {
        protected internal int AuthotId { get; set; }
    }
}
