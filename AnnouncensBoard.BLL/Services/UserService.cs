using AnnouncensBoard.BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace AnnouncensBoard.BLL.Services;

public class UserService : ISignInService
{
    public UserService( UserManager<IdentityUser> userManager)
    {
        
    }
    public Task LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task RegisterAsync(RegisterRequest user)
    {
        throw new NotImplementedException();
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }
}