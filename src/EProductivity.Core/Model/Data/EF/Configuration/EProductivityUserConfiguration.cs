using System.Data.Entity.ModelConfiguration;

namespace EProductivity.Core.Model.Data.EF.Configuration
{
    public class EProductivityUserConfiguration : EntityTypeConfiguration<EProductivityUser>
    {
        public EProductivityUserConfiguration()
        {
            this.ToTable("Users");
        }
    }
}