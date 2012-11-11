namespace MeAnota.Migrations
{
    using MeAnota.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<MeAnota.Models.MeAnotaContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}
