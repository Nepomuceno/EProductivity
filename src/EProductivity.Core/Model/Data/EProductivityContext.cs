using System.Data.Entity;
using System.Reflection;
using EProductivity.Core.Model.Data.Convention;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EProductivity.Core.Model.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class EProductivityContext : IdentityDbContext<EProductivityUser>
    {
        public EProductivityContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new KeyConvention());
            base.OnModelCreating(modelBuilder);
        }

        public static EProductivityContext Create()
        {
            return new EProductivityContext();
        }
    }
}