namespace TaQNIN1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaqninMetadatas", "ReviewercheckApproval", c => c.String());
            AddColumn("dbo.TaqninMetadatas", "BackToReviewercheck", c => c.Int(nullable: false));
            AddColumn("dbo.TaqninMetadatas", "ResponseSuspended", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaqninMetadatas", "ResponseSuspended");
            DropColumn("dbo.TaqninMetadatas", "BackToReviewercheck");
            DropColumn("dbo.TaqninMetadatas", "ReviewercheckApproval");
        }
    }
}
