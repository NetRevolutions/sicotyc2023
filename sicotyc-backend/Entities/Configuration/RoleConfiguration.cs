using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                { 
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR" // Todo menos acceso a Mantenimiento de usuarios y lookup
                },
                new IdentityRole
                { 
                    Name = "Forwarder",
                    NormalizedName = "FORWARDER"    // Transportista
                },
                new IdentityRole { 
                    Name = "Forwarder-Coordinator",
                    NormalizedName = "FORWARDER-COORDINATOR"
                },
                new IdentityRole { 
                    Name = "Forwarder-Biller",
                    NormalizedName = "FORWARDER-BILLER"
                },
                new IdentityRole
                { 
                    Name = "Agency",
                    NormalizedName = "AGENCY" // Agencia de Aduana
                }
            );
        }
    }
}
