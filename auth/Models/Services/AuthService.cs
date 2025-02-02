using Microsoft.EntityFrameworkCore;

public interface IAuthService{
    Task<IResult> loginAsync(LoginDto loginDto);
    Task<IResult> RegisterAsync(UserDto userDto);
}
public class AuthService : IAuthService
{
    IUtilityService utilityService;
    AppDbContext dbContext;
    public AuthService(IServiceScopeFactory scopeFactory,IConfiguration configuration){
        this.dbContext = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        utilityService = new UtilityService(configuration);
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

    public async Task<IResult> RegisterAsync(UserDto userDto)
    {
         if (await dbContext.Users.AnyAsync(u => u.email == userDto.email || u.username == userDto.username))
        {
            return Results.BadRequest("User already exists");
        }

        var user = new User
        {
            fullName = userDto.fullName,
            email = userDto.email,
            mobile = userDto.mobile,
            username = userDto.username,
            password = utilityService.HashPassword(userDto.Password),
            createdDate = DateTime.UtcNow,
        };
        
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        return Results.Ok("User registered successfully");
    }


}