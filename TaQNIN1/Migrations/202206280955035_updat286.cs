namespace TaQNIN1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updat286 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaqninDatas", "CreatedBY", c => c.String());
            AddColumn("dbo.TaqninDatas", "CreatedTime", c => c.String());
            AddColumn("dbo.TaqninDatas", "CreatedDevice", c => c.String());
            AddColumn("dbo.TaqninDatas", "Updated", c => c.String());
            AddColumn("dbo.TaqninDatas", "UpdatedTime", c => c.String());
            AddColumn("dbo.TaqninDatas", "UpdatedDevice", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaqninDatas", "UpdatedDevice");
            DropColumn("dbo.TaqninDatas", "UpdatedTime");
            DropColumn("dbo.TaqninDatas", "Updated");
            DropColumn("dbo.TaqninDatas", "CreatedDevice");
            DropColumn("dbo.TaqninDatas", "CreatedTime");
            DropColumn("dbo.TaqninDatas", "CreatedBY");
        }
    }
}
