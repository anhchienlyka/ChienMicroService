using Contracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Product.API.Persistence
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Entities.CatalogProduct> Products { get; set; }

        public Task<int> SaveChangeAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var modified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Deleted || x.State == EntityState.Added).ToList();
            foreach (var entity in modified)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        if (entity.Entity is IDateTracking addedEntity)
                        {
                            addedEntity.CreatedDate = DateTime.UtcNow;
                            entity.State = EntityState.Added;
                        }
                        break;

                    case EntityState.Modified:
                        Entry(entity.Entity).Property("Id").IsModified = false;

                        if (entity.Entity is IDateTracking modifiedEntity)
                        {
                            modifiedEntity.LastModifiedDate = DateTime.UtcNow;
                            entity.State = EntityState.Modified;
                        }
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Seed();
        }

    }
}