using Microsoft.EntityFrameworkCore;
using PartyOn.Models;
using Microsoft.Extensions.Configuration;

//Boiler Plate set up for Entity Framework
namespace PartyOn.Data 
{
    public class DataContextEF : DbContext
    {
        public DbSet<Computer>? Computer {get; set;}

        private string _connectionString = "Server=localhost;Database=DotnetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true";
        private readonly IConfiguration? _config;

        public DataContextEF(IConfiguration config)
        {
            _config = config;
            _connectionString = _config?.GetConnectionString("DefaultConnection") ?? "default_connection_string";
        } 

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured)
            {
                options.UseSqlServer(_connectionString,
                    options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<Computer>()
                //.HasNoKey();
                .HasKey(c => c.ComputerId);
            //Still looking at default schema - but routes to Tutorial App Schema
            // modelBuilder.Entity<Computer>()
            //     .ToTable("Computer", "TutorialAppSchema");
        }
    }
}