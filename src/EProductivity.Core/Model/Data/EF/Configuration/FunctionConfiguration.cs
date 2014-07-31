using System.Data.Entity.ModelConfiguration;

namespace EProductivity.Core.Model.Data.EF.Configuration
{
    public class FunctionConfiguration : EntityTypeConfiguration<Function>
    {
        public FunctionConfiguration()
        {
            this.ToTable("Functions");
        }
    }
}