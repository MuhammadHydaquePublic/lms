using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public interface IAuthService{
    Task<IResult> loginAsync(LoginDto loginDto);
    Task<IResult> RegisterAsync(UserDto userDto);
    Task<IResult> publicKeyFetch();
}
public class AuthService : IAuthService
{
    IUtilityService utilityService;
    AppDbContext dbContext;
    public AuthService(IServiceScopeFactory scopeFactory,IUtilityService utilityService){
        this.dbContext = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        this.utilityService = utilityService;
    }
    public async Task<IResult> loginAsync(LoginDto loginDto)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.username == loginDto.username);
        if (user == null || !utilityService.VerifyPassword(loginDto.password, user.password))
        {
            return Results.Unauthorized();
        }

        var token = utilityService.GenerateJwtToken(user);
        return Results.Ok(new { Token = token });
    }

    public async Task<IResult> publicKeyFetch()
    {
        RSA rsa = RSA.Create(2048);
        var publicKey = rsa.ExportRSAPublicKey();

        rsa.ImportRSAPublicKey(publicKey, out _);
        var parameters = rsa.ExportParameters(false);

        var jwks = new
        {
            keys = new[]
            {
                new
                {
                    kty = "RSA",
                    use = "sig", // "sig" for signature
                    kid = "your-key-id", // Unique key ID
                    n = utilityService.Base64UrlEncode(parameters.Modulus), // Modulus
                    e = utilityService.Base64UrlEncode(parameters.Exponent), // Exponent
                    alg = "RS256" // Algorithm
                }
            }
        };

        return Results.Ok(jwks);
    }

    public async Task<IResult> RegisterAsync(UserDto userDto)
    {
         if (await dbContext.Users.AnyAsync(u => u.email == userDto.email || u.username == userDto.username))
        {
            return Results.BadRequest("User already exists");
        }
        UserFactory userFactory = new UserFactory(utilityService);
        var user = userFactory.CreateUser(userDto);
        String userTypeName = user.GetType().Name;
        switch(userTypeName){
            case "Teacher":
                dbContext.Teachers.Add((Teacher)user);
                break;
            case "Student":
                dbContext.Students.Add((Student)user);
                break;
            case "Admin":
                dbContext.Admins.Add((Admin)user);
                break;
            default:
            break;
        }
        await dbContext.SaveChangesAsync();
        return Results.Ok("User registered successfully");
    }


}