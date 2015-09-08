namespace Infrastructure.Logging.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialLogDbCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Log.Entries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        LoggedAt = c.DateTime(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("Log.Entries");
        }
    }
}
