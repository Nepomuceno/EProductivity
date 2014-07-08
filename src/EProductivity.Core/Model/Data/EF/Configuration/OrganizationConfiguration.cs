using System.Data.Entity.ModelConfiguration;

namespace EProductivity.Core.Model.Data.EF.Configuration
{
    public class OrganizationConfiguration : EntityTypeConfiguration<Organization>
    {
        public OrganizationConfiguration()
        {
            this.ToTable("Organizations");
        }
    }
}