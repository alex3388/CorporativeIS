using System;
using CurrencyExchange.Domain;
using CurrencyExchange.Interfaces;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;

namespace CurrencyExchange.Repositories
{
    public class OperatorRepository : IOperatorsRepository
    {
        /// <summary>
        /// Добавить оператора в базу
        /// </summary>
        /// <param name="op">экземпляр объекта Operators</param>
        public void Add(Operators op)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transacton = session.BeginTransaction())
                {
                    session.Save(op);
                    transacton.Commit();
                }
            }
        }

        /// <summary>
        /// Обновить информацию об операторе
        /// </summary>
        /// <param name="op">экземпляр объекта Operators</param>
        public void Update(Operators op)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transacton = session.BeginTransaction())
                {
                    session.Update(op);
                    transacton.Commit();
                }
            }
        }

        /// <summary>
        /// Получить оператора из базы по его идентификатору
        /// </summary>
        /// <param name="id">номер оператора</param>
        /// <returns></returns>
        public Operators getById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<Operators>(id);
            }
        }

        /// <summary>
        /// Получить оператора из базы по его логину
        /// </summary>
        /// <param name="login">логин</param>
        /// <returns></returns>
        public Operators getByLogin(string login)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Operators op = session
                    .CreateCriteria(typeof(Operators))
                    .Add(Restrictions.Eq("Login", login))
                    .UniqueResult<Operators>();
                return op;
            }
        }
    }

    /// <summary>
    /// Вспомогательный класс для того, чтоб не создавать соединение каждый раз
    /// <remarks>Проверить, мб достаточно одного хелпера для всех объектов</remarks>
    /// </summary>
    class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory sessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    Configuration conf = new Configuration();
                    conf.Configure();
                    conf.AddAssembly(typeof(Operators).Assembly);
                    //conf.AddAssembly(Assembly.GetCallingAssembly());
                    _sessionFactory = conf.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }
    }
}
