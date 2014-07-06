namespace EProductivity.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EProductivity.Core.Model.Data.EProductivityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        

        protected override void Seed(EProductivity.Core.Model.Data.EProductivityContext context)
        {
        }
    }
}
