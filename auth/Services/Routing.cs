using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public interface IRoutingService{

    void ConfigureRoutes(WebApplication app);
}
public class RoutingService : IRoutingService{

    IAuthService authService;
    public RoutingService(IServiceScopeFactory scopeFactory, IAuthService authService){
        this.authService = authService;
    }
    public void ConfigureRoutes(WebApplication app)
    {
        app.MapPost("/teacher",async ([FromBody] TeacherDto teacherDto) => {
            return await authService.RegisterAsync(teacherDto);
        });
        
        app.MapPost("/student",async ([FromBody] StudentDto studentDto) => {
            return await authService.RegisterAsync(studentDto);
        });
        
        app.MapPost("/admin",async ([FromBody] AdminDto adminDto) => {
            return await authService.RegisterAsync(adminDto);
        });
        app.MapPost("/login", async ([FromBody] LoginDto loginDto) =>
        {
            return await authService.loginAsync(loginDto);
        });
        
        app.MapGet("/.well-known/jwks.json", async () =>
        {
            return await authService.publicKeyFetch();
        });
    }
}