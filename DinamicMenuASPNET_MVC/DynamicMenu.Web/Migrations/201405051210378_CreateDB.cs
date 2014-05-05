namespace DynamicMenu.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbMenu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuFather = c.Int(nullable: false),
                        Name = c.String(),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tbMenu");
        }
    }
}
