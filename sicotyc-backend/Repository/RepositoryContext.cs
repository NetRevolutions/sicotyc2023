using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new LookupCodeGroupConfiguration());
            modelBuilder.ApplyConfiguration(new LookupCodeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.Entity<CompanyCompanyType>()
                .HasKey(e => new { e.CompanyId, e.CompanyTypeId});
            
        }

        public DbSet<LookupCodeGroup>? LookupCodeGroups { get; set; }
        public DbSet<LookupCode>? LookupCodes { get; set; }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<CompanyType>? CompanyTypes { get; set; }
        public DbSet<CompanyCompanyType>? CompanyCompanyTypes { get; set; }
        public DbSet<UserCompany>? UserCompanies { get; set;}
    }
}
