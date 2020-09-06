namespace EfficiencyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        UserActionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserActions", t => t.UserActionId, cascadeDelete: true)
                .Index(t => t.UserActionId);
            
            CreateTable(
                "dbo.UserActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayActionsId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DayActions", t => t.DayActionsId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.DayActionsId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.DayActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Actions", "UserActionId", "dbo.UserActions");
            DropForeignKey("dbo.UserActions", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.UserActions", "DayActionsId", "dbo.DayActions");
            DropIndex("dbo.UserActions", new[] { "EmployeeId" });
            DropIndex("dbo.UserActions", new[] { "DayActionsId" });
            DropIndex("dbo.Actions", new[] { "UserActionId" });
            DropTable("dbo.Employees");
            DropTable("dbo.DayActions");
            DropTable("dbo.UserActions");
            DropTable("dbo.Actions");
        }
    }
}
