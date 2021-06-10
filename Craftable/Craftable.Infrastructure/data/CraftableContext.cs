using Craftable.Core.aggregate.postcode;
using Microsoft.EntityFrameworkCore;

namespace Craftable.Infrastructure.data
{
    public class CraftableContext : DbContext
    {
        private const string DB_NAME = "Craftable";

        public DbSet<AddressRegister> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseInMemoryDatabase(DB_NAME)
                .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressRegister>().HasKey(x => x.Id);
            modelBuilder.Entity<AddressRegister>().OwnsOne(x => x.Distance);
            modelBuilder.Entity<AddressRegister>().OwnsOne(x => x.Coordinates);
        }
    }
}