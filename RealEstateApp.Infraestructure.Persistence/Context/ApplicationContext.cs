using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Domain.Commons;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infraestructure.Persistence.EntityConfigurations;

namespace RealEstateApp.Infraestructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }
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
            modelBuilder.ApplyConfiguration(new RealEstateImprovementConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<TypeOfSale> TypeOfSales { get; set; }
        public DbSet<RealEstateImprovements> RealEstateImprovements { get; set; }
        public DbSet<TypeOfRealEstate> TypeOfRealEstates { get; set; }
        public DbSet<RealEstateImage> RealEstateImages { get; set; }
        public DbSet<RealEstateClient> RealEstateClients { get; set; }
        public DbSet<Improvement> Improvements { get; set; }

    }
}
