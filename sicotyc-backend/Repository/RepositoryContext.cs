using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {        
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
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

            modelBuilder.Entity<DriverLicense>()
                .HasKey(u => new { u.DriverId, u.LicenseNumber, u.LicenseType });

            modelBuilder.Entity<DriverWhareHouse>()
                .HasKey(u => new { u.DriverId, u.WhareHouseId });

            modelBuilder.Entity<MenuOptionRole>()
                .HasKey(u => new { u.RoleId, u.OptionId });
            
        }

        #region DBSet
        public DbSet<LookupCodeGroup>? LookupCodeGroups { get; set; }
        public DbSet<LookupCode>? LookupCodes { get; set; }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<CompanyType>? CompanyTypes { get; set; }
        public DbSet<CompanyCompanyType>? CompanyCompanyTypes { get; set; }
        public DbSet<UserCompany>? UserCompanies { get; set;}
        public DbSet<UserDetail>? UserDetails { get; set; }
        public DbSet<ComplementTransport>? ComplementTransports { get; set; }
        public DbSet<Driver>? Drivers { get; set; }
        public DbSet<TransportDetail>? TransportDetails { get; set; }
        public DbSet<UnitTransport>? UnitTransports { get; set; }
        public DbSet<WhareHouse>? WhareHouses { get; set; }
        public DbSet<DriverLicense>? DriverLicenses { get; set; }
        public DbSet<DriverWhareHouse>? DriverWhareHouses { get; set; }
        public DbSet<MenuOption>? MenuOptions { get; set; }
        public DbSet<MenuOptionRole>? MenuOptionRoles { get; set; }
        public DbSet<OptionByRole>? OptionByRoles { get; set;}

        #endregion


        #region Store Procedures
        //public async Task<List<OptionByRole>> GetMenuOptionsByRoleAsync(string roleName)
        //{
        //    try
        //    {
        //        //var connectionState = Database.GetDbConnection().State;
        //        //if (connectionState == System.Data.ConnectionState.Closed)
        //        //{ 
        //        //    await Database.GetDbConnection().OpenAsync();
        //        //}

        //        return await OptionByRoles.FromSqlRaw("EXEC [SCT].[USP_GET_MENU_OPTIONS_BY_ROLE] @p0", roleName).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}        

        #endregion

    }
}
