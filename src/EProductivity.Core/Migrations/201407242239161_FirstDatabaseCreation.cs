namespace EProductivity.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityResponsabilities",
                c => new
                    {
                        ActivityResponsabilityId = c.Long(nullable: false, identity: true),
                        ActivityId = c.Long(nullable: false),
                        ResponsabilityId = c.Long(nullable: false),
                        WorkType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityResponsabilityId)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: false)
                .ForeignKey("dbo.Responsabilities", t => t.ResponsabilityId, cascadeDelete: false)
                .Index(t => t.ActivityId)
                .Index(t => t.ResponsabilityId);
            
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        BaseActivity = c.Boolean(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        OrganizationId = c.Long(nullable: false, identity: true),
                        Document = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OrganizationId = c.Long(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        EProductivityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.EProductivityUser_Id)
                .Index(t => t.EProductivityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        EProductivityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.EProductivityUser_Id)
                .Index(t => t.EProductivityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        EProductivityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.EProductivityUser_Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: false)
                .Index(t => t.RoleId)
                .Index(t => t.EProductivityUser_Id);
            
            CreateTable(
                "dbo.WorkSamples",
                c => new
                    {
                        WorkSampleId = c.Long(nullable: false, identity: true),
                        AreaId = c.Long(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.WorkSampleId)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: false)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.AreaId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AreaId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Responsabilities",
                c => new
                    {
                        ResponsabilityId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        AreaId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ResponsabilityId)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: false)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.AreaId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        WorkerId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ResponsabilityId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.WorkerId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("dbo.Responsabilities", t => t.ResponsabilityId, cascadeDelete: false)
                .Index(t => t.ResponsabilityId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Observations",
                c => new
                    {
                        ObservationId = c.Long(nullable: false, identity: true),
                        WorkerId = c.Long(nullable: false),
                        ResponsabilityId = c.Long(nullable: false),
                        ActivityId = c.Long(nullable: false),
                        TourId = c.Long(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ObservationId)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: false)
                .ForeignKey("dbo.Responsabilities", t => t.ResponsabilityId, cascadeDelete: false)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: false)
                .ForeignKey("dbo.Workers", t => t.WorkerId, cascadeDelete: false)
                .Index(t => t.WorkerId)
                .Index(t => t.ResponsabilityId)
                .Index(t => t.ActivityId)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.Tours",
                c => new
                    {
                        TourId = c.Long(nullable: false, identity: true),
                        WorkSampleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.TourId)
                .ForeignKey("dbo.WorkSamples", t => t.WorkSampleId, cascadeDelete: true)
                .Index(t => t.WorkSampleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.WorkerWorkSample",
                c => new
                    {
                        Worker_WorkerId = c.Long(nullable: false),
                        WorkSample_WorkSampleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Worker_WorkerId, t.WorkSample_WorkSampleId })
                .ForeignKey("dbo.Workers", t => t.Worker_WorkerId, cascadeDelete: false)
                .ForeignKey("dbo.WorkSamples", t => t.WorkSample_WorkSampleId, cascadeDelete: false)
                .Index(t => t.Worker_WorkerId)
                .Index(t => t.WorkSample_WorkSampleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Activities", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.WorkSamples", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.WorkSamples", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.WorkerWorkSample", "WorkSample_WorkSampleId", "dbo.WorkSamples");
            DropForeignKey("dbo.WorkerWorkSample", "Worker_WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Workers", "ResponsabilityId", "dbo.Responsabilities");
            DropForeignKey("dbo.Workers", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Observations", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Tours", "WorkSampleId", "dbo.WorkSamples");
            DropForeignKey("dbo.Observations", "TourId", "dbo.Tours");
            DropForeignKey("dbo.Observations", "ResponsabilityId", "dbo.Responsabilities");
            DropForeignKey("dbo.Observations", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Responsabilities", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Responsabilities", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.ActivityResponsabilities", "ResponsabilityId", "dbo.Responsabilities");
            DropForeignKey("dbo.Areas", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.AspNetUserRoles", "EProductivityUser_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.AspNetUserLogins", "EProductivityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserClaims", "EProductivityUser_Id", "dbo.Users");
            DropForeignKey("dbo.ActivityResponsabilities", "ActivityId", "dbo.Activities");
            DropIndex("dbo.WorkerWorkSample", new[] { "WorkSample_WorkSampleId" });
            DropIndex("dbo.WorkerWorkSample", new[] { "Worker_WorkerId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Tours", new[] { "WorkSampleId" });
            DropIndex("dbo.Observations", new[] { "TourId" });
            DropIndex("dbo.Observations", new[] { "ActivityId" });
            DropIndex("dbo.Observations", new[] { "ResponsabilityId" });
            DropIndex("dbo.Observations", new[] { "WorkerId" });
            DropIndex("dbo.Workers", new[] { "OrganizationId" });
            DropIndex("dbo.Workers", new[] { "ResponsabilityId" });
            DropIndex("dbo.Responsabilities", new[] { "OrganizationId" });
            DropIndex("dbo.Responsabilities", new[] { "AreaId" });
            DropIndex("dbo.Areas", new[] { "OrganizationId" });
            DropIndex("dbo.WorkSamples", new[] { "OrganizationId" });
            DropIndex("dbo.WorkSamples", new[] { "AreaId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "EProductivityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "EProductivityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "EProductivityUser_Id" });
            DropIndex("dbo.Users", new[] { "OrganizationId" });
            DropIndex("dbo.Activities", new[] { "OrganizationId" });
            DropIndex("dbo.ActivityResponsabilities", new[] { "ResponsabilityId" });
            DropIndex("dbo.ActivityResponsabilities", new[] { "ActivityId" });
            DropTable("dbo.WorkerWorkSample");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tours");
            DropTable("dbo.Observations");
            DropTable("dbo.Workers");
            DropTable("dbo.Responsabilities");
            DropTable("dbo.Areas");
            DropTable("dbo.WorkSamples");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Organizations");
            DropTable("dbo.Activities");
            DropTable("dbo.ActivityResponsabilities");
        }
    }
}
