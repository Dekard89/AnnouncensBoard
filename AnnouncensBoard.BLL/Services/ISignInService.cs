using AnnouncensBoard.BLL.Models;

namespace AnnouncensBoard.BLL.Services;

public interface ISignInService
{
    public Task LoginAsync(string email, string password);
    
    public Task RegisterAsync(RegisterRequest user) ;
    
    public Task LogoutAsync();
}