using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext{
    private readonly IConfiguration configuration;
    public DbSet<User> Users { get; set; }
     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) 
        : base(options)
    {
        this.configuration = configuration;
    }
}