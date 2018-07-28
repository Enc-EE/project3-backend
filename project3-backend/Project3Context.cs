using project3_backend.Interfaces;
using project3_backend.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace project3_backend
{
    public class Project3Context : DbContext
    {
        public DbSet<List> Lists { get; set; }
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            UpdateAudit();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            UpdateAudit();
            return base.SaveChangesAsync();
        }

        private void UpdateAudit()
        {
            var currentUsername = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;

            foreach (var entity in ChangeTracker.Entries().Where(x => x.Entity is IDbEntity && x.State == EntityState.Added))
            {
                ((IDbEntity)entity.Entity).CreatedAt = DateTime.UtcNow;
                ((IDbEntity)entity.Entity).CreatedBy = currentUsername;
                ((IDbEntity)entity.Entity).ModifiedAt = DateTime.UtcNow;
                ((IDbEntity)entity.Entity).ModifiedBy = currentUsername;
            }

            foreach (var entity in ChangeTracker.Entries().Where(x => x.Entity is IDbEntity && x.State == EntityState.Modified))
            {
                ((IDbEntity)entity.Entity).ModifiedAt = DateTime.UtcNow;
                ((IDbEntity)entity.Entity).ModifiedBy = currentUsername;
            }
        }
    }
}