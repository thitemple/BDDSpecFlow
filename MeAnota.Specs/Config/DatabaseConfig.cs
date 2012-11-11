using TechTalk.SpecFlow;
using WebMatrix.WebData;

namespace MeAnota.Specs.Config
{
    [Binding]
    public class DatabaseConfig
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var dbMigrator = new System.Data.Entity.Migrations.DbMigrator(new MeAnota.Migrations.Configuration());
            dbMigrator.Update();
            WebSecurity.InitializeDatabaseConnection("MeAnotaConnString", "Usuario", "Id", "Email", autoCreateTables: true);
        }

        [BeforeScenario]
        public void ReiniciaBanco()
        {
            var dbMigrator = new System.Data.Entity.Migrations.DbMigrator(new MeAnota.Migrations.Configuration());
            dbMigrator.Update("ModeloInicial");
            dbMigrator.Update();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            var dbMigrator = new System.Data.Entity.Migrations.DbMigrator(new MeAnota.Migrations.Configuration());
            dbMigrator.Update("ModeloInicial");
        }
    }
}
