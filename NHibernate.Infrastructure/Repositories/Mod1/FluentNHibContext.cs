using System.Reflection;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;

namespace EBXDashboardsModel.Infra.Repositories {
    /// <summary>
    /// Uses Xml-configuration for setup-config and for mappings is Fluent-NHibernate used.
    /// </summary>
    public class FluentNHibContext
        : NHibContext {
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentNHibContext"/> class.
        /// </summary>
        /// <param name="assemblyWithMappings">The assembly containing the Fluent mappings.</param>
        public FluentNHibContext(Assembly assemblyWithMappings) {
            SessionFactory = CreateSessionFactory(assemblyWithMappings);
        }

        /// <summary>
        /// Creates and returns a session factory.
        /// </summary>
        /// <param name="assemblyWithMappings">The assembly containing the Fluent mappings.</param>
        /// <returns></returns>
        private ISessionFactory CreateSessionFactory(Assembly assemblyWithMappings) {
            var cfg = new Configuration();
            cfg = cfg.Configure();

            return Fluently.Configure(cfg)
                .Mappings(m =>
                          m.FluentMappings.AddFromAssembly(assemblyWithMappings))
                .BuildSessionFactory();

            //return cfg.BuildSessionFactory();
        }
    }

}
