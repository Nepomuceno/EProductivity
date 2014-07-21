using System.Data.Entity.ModelConfiguration;

namespace EProductivity.Core.Model.Data.EF.Configuration
{
    public class ActivityConfiguration : EntityTypeConfiguration<Activity>
    {
        public ActivityConfiguration()
        {
            this.ToTable("Activities");
        }
    }
}