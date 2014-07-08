using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using EProductivity.Core.Model.Data.Convention;
using EProductivity.Core.Model.Data.EF.Configuration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EProductivity.Core.Model.Data.EF
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class EProductivityContext : IdentityDbContext<EProductivityUser>, IModelContext
    {
        public EProductivityContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Add(new KeyConvention());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new EProductivityUserConfiguration());
            modelBuilder.Configurations.Add(new OrganizationConfiguration());
        }

        public static EProductivityContext Create()
        {
            return new EProductivityContext();
        }
        public IDbSet<Organization> OrganizationSet { get; set; } 

        public IModelCollection<Organization, Guid> Organizations
        {
            get { return new ModelCollection<Organization, Guid>(this.OrganizationSet);}
        }

        IModelCollection<EProductivityUser, string> IModelContext.Users
        {
            get { return new ModelCollection<EProductivityUser, string>(Users); }
        }

        public async Task<int> SaveAsync()
        {
            return await SaveChangesAsync();
        }

    }
}