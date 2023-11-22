using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infraestructure.Persistence.EntityConfigurations;

namespace RealEstateApp.Infraestructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RealEstateConfiguration());
            modelBuilder.ApplyConfiguration(new TypeOfSaleConfiguration());
            modelBuilder.ApplyConfiguration(new TypeOfRealEstateConfiguration());
            modelBuilder.ApplyConfiguration(new RealEstateClientConfiguration());
            modelBuilder.ApplyConfiguration(new RealEstateImageConfiguration());
            modelBuilder.ApplyConfiguration(new ImprovementConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<TypeOfSale> TypeOfSales { get; set; }
        public DbSet<TypeOfRealEstate> TypeOfRealEstates { get; set; }
        public DbSet<RealEstateImage> RealEstateImages { get; set; }
        public DbSet<RealEstateClient> RealEstateClients { get; set; }
        public DbSet<Improvement> Improvements { get; set; }

    }
}
