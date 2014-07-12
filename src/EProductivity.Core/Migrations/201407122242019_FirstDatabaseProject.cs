namespace EProductivity.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstDatabaseProject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Observation",
                c => new
                    {
                        ObservationId = c.Long(nullable: false, identity: true),
                        TourId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ObservationId)
                .ForeignKey("dbo.Tour", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.Tour",
                c => new
                    {
                        TourId = c.Long(nullable: false, identity: true),
                        WorkSampleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.TourId)
                .ForeignKey("dbo.WorkSample", t => t.WorkSampleId, cascadeDelete: true)
                .Index(t => t.WorkSampleId);
            
            CreateTable(
                "dbo.WorkSample",
                c => new
                    {
                        WorkSampleId = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkSampleId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        OrganizationId = c.Int(nullable: false, identity: true),
                        Document = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OrganizationId = c.Int(nullable: false),
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
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.EProductivityUser_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Tour", "WorkSampleId", "dbo.WorkSample");
            DropForeignKey("dbo.WorkSample", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.AspNetUserRoles", "EProductivityUser_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.AspNetUserLogins", "EProductivityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserClaims", "EProductivityUser_Id", "dbo.Users");
            DropForeignKey("dbo.Observation", "TourId", "dbo.Tour");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "EProductivityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "EProductivityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "EProductivityUser_Id" });
            DropIndex("dbo.Users", new[] { "OrganizationId" });
            DropIndex("dbo.WorkSample", new[] { "OrganizationId" });
            DropIndex("dbo.Tour", new[] { "WorkSampleId" });
            DropIndex("dbo.Observation", new[] { "TourId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Organizations");
            DropTable("dbo.WorkSample");
            DropTable("dbo.Tour");
            DropTable("dbo.Observation");
        }
    }
}
