namespace MVC_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pindah : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_M_Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role_Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TB_M_User", "Role_Id", c => c.Int());
            CreateIndex("dbo.TB_M_User", "Role_Id");
            AddForeignKey("dbo.TB_M_User", "Role_Id", "dbo.TB_M_Role", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TB_M_User", "Role_Id", "dbo.TB_M_Role");
            DropIndex("dbo.TB_M_User", new[] { "Role_Id" });
            DropColumn("dbo.TB_M_User", "Role_Id");
            DropTable("dbo.TB_M_Role");
        }
    }
}
