using System;

using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using CurrencyExchange.Domain;
using CurrencyExchange.Repositories;
using CurrencyExchange.Interfaces;

namespace ForTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Operators).Assembly);
            try
            {
                new SchemaExport(cfg).Execute(true, true, false);
            }
            catch (NHibernate.HibernateException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Operators op = new Operators() { Login="Test1", Password="wtf", Permissions="all" };
            IOperatorsRepository rep = new OperatorRepository();
            rep.Add(op);

            Operators dbOp = rep.getByLogin("Test1");
            if (dbOp.Password != op.Password)
            {
                throw new Exception("Passwords are not equal");
            }

            Console.ReadKey();
        }
    }
}
