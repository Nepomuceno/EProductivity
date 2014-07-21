using System.Data.Entity.ModelConfiguration;

namespace EProductivity.Core.Model.Data.EF.Configuration
{
    public class ActivitityResponsabilityConfiguration : EntityTypeConfiguration<ActivityResponsability>
    {
        public ActivitityResponsabilityConfiguration()
        {
            this.ToTable("ActivityResponsabilities");
        }
    }
}