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
            modelBuilder.Configurations.Add(new ObservationConfiguration());
            modelBuilder.Configurations.Add(new WorkSampleConfiguration());
            modelBuilder.Configurations.Add(new TourConfiguration());
            modelBuilder.Configurations.Add(new WorkerConfiguration());
            modelBuilder.Configurations.Add(new FunctionConfiguration());
            modelBuilder.Configurations.Add(new AreaConfiguration());
            modelBuilder.Configurations.Add(new ActivityConfiguration());
        }
        public static EProductivityContext Create()
        {
            return new EProductivityContext();
        }
        public IDbSet<Organization> OrganizationSet { get; set; } 
        public IModelCollection<Organization, int> Organizations
        {
            get { return new ModelCollection<Organization, int>(this.OrganizationSet);}
        }
        IModelCollection<EProductivityUser, string> IModelContext.Users
        {
            get { return new ModelCollection<EProductivityUser, string>(Users); }
        }
        public IDbSet<WorkSample> WorkSampleSet { get; set; } 
        public IModelCollection<WorkSample, long> WorkSamples
        {
            get { return new ModelCollection<WorkSample, long>(this.WorkSampleSet); }
        }

        public IDbSet<Tour> TourSet { get; set; }
        public IModelCollection<Tour, long> Tours
        {
            get { return new ModelCollection<Tour, long>(this.TourSet); }
        }

        public IDbSet<Observation> ObservationSet { get; set; }
        public IModelCollection<Observation, long> Observations
        {
            get { return new ModelCollection<Observation, long>(this.ObservationSet); }
        }

        public IDbSet<Area> AreaSet { get; set; }
        public IModelCollection<Area, int> Areas
        {
            get{ return new ModelCollection<Area, int>(this.AreaSet);}
        }

        public IDbSet<Worker> WorkerSet { get; set; }
        public IModelCollection<Worker, int> Workers
        {
            get { return new ModelCollection<Worker, int>(this.WorkerSet);}
        }

        public IDbSet<Function> FunctionSet { get; set; }
        public IModelCollection<Function, int> Functions
        {
            get { return new ModelCollection<Function, int>(this.FunctionSet);}
        }

        public IDbSet<Activity> ActivitySet { get; set; }
        public IModelCollection<Activity, long> Activities
        {
            get { return new ModelCollection<Activity, long>(this.ActivitySet); }
        }
        public async Task<int> SaveAsync()
        {
            return await SaveChangesAsync();
        }

    }
}