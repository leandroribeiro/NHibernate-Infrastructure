using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace NHibernate.Infrastructure.Repositories.Mod6 {
    public class NHibernateHelper {
        private const string ConnectionString = "FluentNHibernateConnectionString";
        //private readonly string _connectionString;
        private ISessionFactory _sessionFactory;

        public ISessionFactory SessionFactory {
            get { return _sessionFactory ?? (_sessionFactory = CreateSessionFactory()); }
        }

        //public NHibernateHelper(string connectionString) {
        //    _connectionString = connectionString;
        //}

        //private ISessionFactory CreateSessionFactory() {
        //    return Fluently.Configure()
        //        .Database(MsSqlConfiguration.MsSql2008.ConnectionString(_connectionString))
        //        .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
        //        .BuildSessionFactory();
        //}

        private ISessionFactory CreateSessionFactory() {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(cnx => cnx.FromConnectionStringWithKey(ConnectionString)))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }
    }
}
