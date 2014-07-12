using System.Data.Entity.ModelConfiguration;

namespace EProductivity.Core.Model.Data.EF.Configuration
{
    public class ObservationConfiguration : EntityTypeConfiguration<Observation>
    {
        public ObservationConfiguration()
        {
            this.ToTable("Observations");
        }
    }
}