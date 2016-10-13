namespace SnowFur.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxlengthForConventionName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Conventions", "Name", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Conventions", "Name", c => c.String());
        }
    }
}
