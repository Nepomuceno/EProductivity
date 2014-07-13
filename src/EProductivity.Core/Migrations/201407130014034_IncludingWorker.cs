namespace EProductivity.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncludingWorker : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Observation", newName: "Observations");
            RenameTable(name: "dbo.Tour", newName: "Tours");
            RenameTable(name: "dbo.WorkSample", newName: "WorkSamples");
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.AreaId);
            
            CreateTable(
                "dbo.Responsabilities",
                c => new
                    {
                        ResponsabilityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResponsabilityId)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: true)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        WorkerId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ReponsabilityId = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        Responsability_ResponsabilityId = c.Int(),
                    })
                .PrimaryKey(t => t.WorkerId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("dbo.Responsabilities", t => t.Responsability_ResponsabilityId)
                .Index(t => t.OrganizationId)
                .Index(t => t.Responsability_ResponsabilityId);
            
            CreateTable(
                "dbo.WorkSampleWorker",
                c => new
                    {
                        WorkSample_WorkSampleId = c.Long(nullable: false),
                        Worker_WorkerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkSample_WorkSampleId, t.Worker_WorkerId })
                .ForeignKey("dbo.WorkSamples", t => t.WorkSample_WorkSampleId, cascadeDelete: false)
                .ForeignKey("dbo.Workers", t => t.Worker_WorkerId, cascadeDelete: false)
                .Index(t => t.WorkSample_WorkSampleId)
                .Index(t => t.Worker_WorkerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workers", "Responsability_ResponsabilityId", "dbo.Responsabilities");
            DropForeignKey("dbo.Workers", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.WorkSampleWorker", "Worker_WorkerId", "dbo.Workers");
            DropForeignKey("dbo.WorkSampleWorker", "WorkSample_WorkSampleId", "dbo.WorkSamples");
            DropForeignKey("dbo.Responsabilities", "AreaId", "dbo.Areas");
            DropIndex("dbo.WorkSampleWorker", new[] { "Worker_WorkerId" });
            DropIndex("dbo.WorkSampleWorker", new[] { "WorkSample_WorkSampleId" });
            DropIndex("dbo.Workers", new[] { "Responsability_ResponsabilityId" });
            DropIndex("dbo.Workers", new[] { "OrganizationId" });
            DropIndex("dbo.Responsabilities", new[] { "AreaId" });
            DropTable("dbo.WorkSampleWorker");
            DropTable("dbo.Workers");
            DropTable("dbo.Responsabilities");
            DropTable("dbo.Areas");
            RenameTable(name: "dbo.WorkSamples", newName: "WorkSample");
            RenameTable(name: "dbo.Tours", newName: "Tour");
            RenameTable(name: "dbo.Observations", newName: "Observation");
        }
    }
}
