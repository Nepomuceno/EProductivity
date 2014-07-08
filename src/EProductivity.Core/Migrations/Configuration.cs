using EProductivity.Core.Model.Data.EF;

namespace EProductivity.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EProductivityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        

        protected override void Seed(EProductivityContext context)
        {
        }
    }
}
