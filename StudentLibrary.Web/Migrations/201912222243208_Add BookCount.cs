namespace StudentLibrary.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BookCount", c => c.Int(nullable: false, defaultValue:0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BookCount");
        }
    }
}
