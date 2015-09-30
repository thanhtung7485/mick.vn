using System.Collections.Generic;
using EagleDigital.CodeFirst.Models;

namespace EagleDigital.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EagleDigital.CodeFirst.MickContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EagleDigital.CodeFirst.MickContext context)
        {

        }
    }
}
