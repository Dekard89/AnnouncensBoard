namespace AnnouncensBoard.BLL.Models;

public record RegisterRequest
{
    public string Username { get; set; }=String.Empty;
    
    public string Email { get; set; }=String.Empty;
    
    public string Password { get; set; }=String.Empty;
    
    public string ConfirmPassword { get; set; }=String.Empty;
    
    public string Phone { get; set; }=String.Empty;
    
    public DateTime Birthday { get; set; }
    
    
}