using System.Data.Entity.ModelConfiguration;

namespace EProductivity.Core.Model.Data.EF.Configuration
{
    public class ResponsabilityConfiguration : EntityTypeConfiguration<Responsability>
    {
        public ResponsabilityConfiguration()
        {
            this.ToTable("Responsabilities");
        }
    }
}