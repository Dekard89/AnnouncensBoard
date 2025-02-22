using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AnnouncensBoard.Options;

public class JwtOptions 
{
    public const string Section = nameof(JwtOptions);

    public string Issuer { get; set; } = String.Empty;
    
    public string Audience { get; set; }=String.Empty;
    
    public string SecretKey { get; set; }=String.Empty;
    
    public string TokenLifetime { get; set; }= String.Empty;
    
    public SymmetricSecurityKey GetSymmetricSecurityKey()
        => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
    
}