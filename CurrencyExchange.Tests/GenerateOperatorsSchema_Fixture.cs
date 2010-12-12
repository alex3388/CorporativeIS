using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyExchange.Domain;

using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace CurrencyExchange.Tests
{
    [TestFixture]
    class GenerateOperatorsSchema_Fixture
    {
        [Test]
        public void Can_generate_schema()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Operators).Assembly);

            new SchemaExport(cfg).Execute(false, true, false);
        }
    }
}
