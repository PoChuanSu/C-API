
using HelloWorld.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data
{
    public class DataContextEF : DbContext
    {

        private IConfiguration _config;
        // private string _connectionString;
        public DataContextEF (IConfiguration config)
        {
            _config = config;
            // _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public DbSet<Computer>? Computer {get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                    options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<Computer>()
                .HasKey(c => c.ComputerId);
        //    .ToTable("Computer", "TutorialAppSchema");
        }
    }
}