using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using myshop.DAL.Models;
using myshop.Entities.Models;

namespace myshop.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            ApplySoftDelete();
            ApplyAudite();
            return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            ApplySoftDelete();
            ApplyAudite();
            return base.SaveChanges();
        }
        private void ApplySoftDelete()
        {
            foreach (var entity in ChangeTracker.Entries<ISoftDelete>())
            {
                if (entity.State == EntityState.Deleted)
                {
                    entity.State = EntityState.Modified;
                    entity.Entity.IsDeleted = true;
                }
            }
        }
       private void ApplyAudite()
        {
            foreach (var entity in ChangeTracker.Entries<IAuditable>())
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedAt= DateTime.UtcNow;
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}