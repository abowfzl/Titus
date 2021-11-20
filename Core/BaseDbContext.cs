using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    public class BaseDbContext : DbContext
    {
        private readonly string _dbConnectionString;

        public BaseDbContext(DbContextOptions<BaseDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public BaseDbContext(string dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
        }
        
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product").HasKey(i => i.Id);
            
            base.OnModelCreating(modelBuilder);

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_dbConnectionString))
                optionsBuilder.UseSqlServer(_dbConnectionString);
        }
    }
}