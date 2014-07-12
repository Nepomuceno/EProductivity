using System.Data.Entity.ModelConfiguration;

namespace EProductivity.Core.Model.Data.EF.Configuration
{
    public class WorkSampleConfiguration : EntityTypeConfiguration<WorkSample>
    {
        public WorkSampleConfiguration()
        {
            this.ToTable("WorkSamples");
        }
    }
}