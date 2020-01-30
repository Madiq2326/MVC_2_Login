namespace MVC_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ubah_lagi : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TB_M_User", name: "Role_Id", newName: "Role_id_Id");
            RenameIndex(table: "dbo.TB_M_User", name: "IX_Role_Id", newName: "IX_Role_id_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TB_M_User", name: "IX_Role_id_Id", newName: "IX_Role_Id");
            RenameColumn(table: "dbo.TB_M_User", name: "Role_id_Id", newName: "Role_Id");
        }
    }
}
