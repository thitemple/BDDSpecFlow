namespace MeAnota.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTabelasMembership : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            Sql("delete webpages_UsersInRoles");
            Sql("delete webpages_OAuthMembership");
            Sql("delete webpages_Roles");
            Sql("delete webpages_Membership");
            Sql("delete ColaboradoresBloco");
            Sql("delete Nota");
            Sql("delete Bloco");
            Sql("delete Usuario");
        }
    }
}
