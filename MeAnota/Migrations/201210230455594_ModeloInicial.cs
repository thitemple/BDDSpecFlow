namespace MeAnota.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModeloInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Nota",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false),
                        Texto = c.String(),
                        Bloco_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bloco", t => t.Bloco_Id)
                .Index(t => t.Bloco_Id);
            
            CreateTable(
                "dbo.Bloco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Proprietario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Proprietario_Id)
                .Index(t => t.Proprietario_Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ColaboradoresBloco",
                c => new
                    {
                        Bloco_Id = c.Int(nullable: false),
                        Usuario_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Bloco_Id, t.Usuario_Id })
                .ForeignKey("dbo.Bloco", t => t.Bloco_Id, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.Usuario_Id, cascadeDelete: true)
                .Index(t => t.Bloco_Id)
                .Index(t => t.Usuario_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ColaboradoresBloco", new[] { "Usuario_Id" });
            DropIndex("dbo.ColaboradoresBloco", new[] { "Bloco_Id" });
            DropIndex("dbo.Bloco", new[] { "Proprietario_Id" });
            DropIndex("dbo.Nota", new[] { "Bloco_Id" });
            DropForeignKey("dbo.ColaboradoresBloco", "Usuario_Id", "dbo.Usuario");
            DropForeignKey("dbo.ColaboradoresBloco", "Bloco_Id", "dbo.Bloco");
            DropForeignKey("dbo.Bloco", "Proprietario_Id", "dbo.Usuario");
            DropForeignKey("dbo.Nota", "Bloco_Id", "dbo.Bloco");
            DropTable("dbo.ColaboradoresBloco");
            DropTable("dbo.Usuario");
            DropTable("dbo.Bloco");
            DropTable("dbo.Nota");
        }
    }
}
