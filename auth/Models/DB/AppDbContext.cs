using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext{
    private readonly IConfiguration configuration;
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Admin> Admins { get; set; }
     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .HasDiscriminator<String>("user_type")
        .HasValue<Teacher>("teacher")
        .HasValue<Student>("student")
        .HasValue<Admin>("admin");
        
    }
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) 
        : base(options)
    {
        this.configuration = configuration;
    }
}