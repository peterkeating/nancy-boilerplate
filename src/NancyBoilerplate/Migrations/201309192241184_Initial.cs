namespace NancyBoilerplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable("Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Username = c.String(maxLength: 250),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("Users");
        }
    }
}
