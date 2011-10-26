using System;
using NHibernate;
using NHibernate.Cfg;

namespace EBXDashboardsModel.Infra.Repositories {
    /// <summary>
    /// Uses Xml-configuration for setup-config and for mappings is Hbm-files used.
    /// </summary>
    public class HbmNHibContext : NHibContext {
        /// <summary>
        /// Initializes a new instance of the <see cref="HbmNHibContext"/> class.
        /// Since you do not provide an assembly that contains the mappings,
        /// you have to provide these via configuration of mapping-resources.
        /// </summary>
        public HbmNHibContext()
            : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibContext"/> class.
        /// </summary>
        /// <param name="assemblyName">The Name of the Assembly containing the mappings.</param>
        public HbmNHibContext(string assemblyName) {
            SessionFactory = CreateSessionFactory(assemblyName);
        }

        /// <summary>
        /// Creates and returns a session factory.
        /// </summary>
        /// <param name="assemblyName">Optional Name of the assembly that contains the mappings.</param>
        /// <returns></returns>
        private ISessionFactory CreateSessionFactory(string assemblyName) {
            var cfg = new Configuration();
            cfg = cfg.Configure();

            if (!String.IsNullOrEmpty(assemblyName))
                cfg.AddAssembly(assemblyName);

            return cfg.BuildSessionFactory();
        }
    }

}
