namespace Infrastructure.Logging.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogExtendedWithXml : DbMigration
    {
        public override void Up()
        {
            AddColumn("Log.Entries", "Error", c => c.String());
            AddColumn("Log.Entries", "Xml", c => c.String(storeType: "xml"));
        }
        
        public override void Down()
        {
            DropColumn("Log.Entries", "Xml");
            DropColumn("Log.Entries", "Error");
        }
    }
}
