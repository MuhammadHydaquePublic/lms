using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public interface IUtilityService{
    
        string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
    string GenerateJwtToken(User user);
}
public class UtilityService : IUtilityService{
     private const int SaltSize = 16; 
    private const int HashSize = 32; 
    private const int Iterations = 10000;
    private readonly IConfiguration configuration;
    public UtilityService(IConfiguration configuration){
        this.configuration = configuration;
    }

    public string HashPassword(string password)
    {
        using var hmac = new HMACSHA256();        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[SaltSize];
        rng.GetBytes(salt);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(HashSize);
        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    public bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split(':');
        if (parts.Length != 2) return false;

        var salt = Convert.FromBase64String(parts[0]);
        var storedPasswordHash = Convert.FromBase64String(parts[1]);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        var computedHash = pbkdf2.GetBytes(HashSize);

        return computedHash.SequenceEqual(storedPasswordHash);
    }

    public string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.username),
            new Claim("id", user.id.ToString()),
            new Claim(ClaimTypes.Role, user.GetType().Name)
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:issuer"],
            audience: configuration["Jwt:audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}