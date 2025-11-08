using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SocialMediaAPİ.DB.Entities;
using SocialMediaAPİ.DB.Entities.Base;
using System.Linq.Expressions;

namespace SocialMediaAPİ.DB
{
    public class AppDbContext : IdentityDbContext<User, UserRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users {  get; set; }
        public DbSet<UserRole> Roles {  get; set; }
        public DbSet<Certification> Certifications {  get; set; }
        public DbSet<Education> Educations {  get; set; }
        public DbSet<Experience> Experiences {  get; set; }
        public DbSet<Project> Projects {  get; set; }
        public DbSet<Recommendation> Recommendations {  get; set; }
        public DbSet<Skill> Skills {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
             .HasIndex(x => x.UserName)
             .IsUnique();

            modelBuilder.Entity<User>()
              .HasIndex(x => x.Email)
              .IsUnique();
        }

        public void ApplyGlobalFilters<TInterface>(ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> expression)
        {
            var entities = modelBuilder.Model
                .GetEntityTypes()
                .Where(e => e.ClrType.GetInterface(typeof(TInterface).Name) != null)
                .Select(e => e.ClrType);

            foreach (var entity in entities)
            {
                var newParam = Expression.Parameter(entity);
                var newbody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
                modelBuilder.Entity(entity).HasQueryFilter(Expression.Lambda(newbody, newParam));
            }
        }

        public override int SaveChanges()
        {
            AddEntryHistory();
            return base.SaveChanges();
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AddEntryHistory();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void AddEntryHistory()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted);

            foreach (var entry in entities)
            {
                if (entry.Entity is not IEntity entity)
                    continue;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }
                else
                {
                    entry.Property(nameof(IEntity.CreatedAt)).IsModified = false;
                    entity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }

    }
}
