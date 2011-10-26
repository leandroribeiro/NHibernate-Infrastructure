using System.Reflection;
using NHibernate.Cfg;

namespace NHibernate.Infrastructure.Repositories.Mod3 {
    public static class Database {
        private static ISessionFactory _sessionFactory;


        //SessionFactory = Fluently.Configure()
        //      .Database(
        //          MsSqlConfiguration.MsSql2008.ShowSql()
        //          .ConnectionString(x => x.FromConnectionStringWithKey("FluentNHibernateConnectionString"))
        //      ).Mappings(m => m.FluentMappings.AddFromAssemblyOf<AreaMap>())
        //      .BuildSessionFactory();

        //    EhWeb = false;

        //    return SessionFactory;

        private static ISessionFactory SessionFactory {
            get {
                if (_sessionFactory == null) {
                    var configuration = new Configuration();

                    configuration.Configure();
                    configuration.AddAssembly(Assembly.GetCallingAssembly());

                    _sessionFactory = configuration.BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        public static ISession OpenSession() {
            return SessionFactory.OpenSession();
        }
    }

}
