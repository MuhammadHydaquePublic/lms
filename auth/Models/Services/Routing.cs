using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public interface IRoutingService{

    void ConfigureRoutes(WebApplication app);
}
public class RoutingService : IRoutingService{

    AuthService authService;
    public RoutingService(IServiceScopeFactory scopeFactory, IConfiguration configuration){
        authService = new AuthService(scopeFactory,configuration);
    }
    public void ConfigureRoutes(WebApplication app)
    {
        app.MapPost("/signup", async ([FromBody] UserDto userDto) =>
        {
            return await authService.RegisterAsync(userDto);
            
        });

        app.MapPost("/login", async ([FromBody] LoginDto loginDto) =>
        {
            return await authService.loginAsync(loginDto);
        });
    }
}